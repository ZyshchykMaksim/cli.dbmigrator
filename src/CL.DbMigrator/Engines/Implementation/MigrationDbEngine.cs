using System;
using System.IO;
using CLI.DbMigrator.Loggers;
using CLI.DbMigrator.Options;
using DbUp;
using DbUp.ScriptProviders;
using Microsoft.Extensions.Logging;

namespace CLI.DbMigrator.Engines.Implementation
{
    /// <inheritdoc cref="IDbEngine"/>
    public class MigrationDbEngine : IDbEngine
    {
        private const string SQL_SCHEMA = "dbo";
        private const string JOURNAL_TABLE = "SchemaVersions";

        private readonly ILogger<MigrationDbEngine> _logger;

        private MigrationOptions Options { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationDbEngine"/> class.
        /// </summary>
        public MigrationDbEngine(ILogger<MigrationDbEngine> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public void SetOptions<T>(T options) where T : BaseOptions
        {
            switch (options)
            {
                case null:
                    throw new ArgumentNullException(nameof(options));
                case MigrationOptions connectOptions:
                    Options = connectOptions;
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        /// <inheritdoc />
        public void Execute()
        {
            if (!Directory.Exists(Options.Directory))
            {
                throw new DirectoryNotFoundException($"The directory {Options.Directory} is not exist.");
            }
            
            var dbDeploy = DeployChanges
                .To
                .SqlDatabase(Options.ConnectionString)
                .LogTo(new DbEngineLogger<MigrationDbEngine>(_logger))
                .JournalToSqlTable(SQL_SCHEMA, JOURNAL_TABLE)
                .WithScriptsFromFileSystem(Options.Directory, new FileSystemScriptOptions
                {
                    IncludeSubDirectories = true
                })
                .Build();

            var result = dbDeploy.PerformUpgrade();

            if (!result.Successful)
            {
                _logger.LogError(result.Error.ToString());
            }
            else
            {
                _logger.LogInformation("The script migration operation for the database was successful.");
            }
        }
    }
}
