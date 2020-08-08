using System;
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
            var dbDeploy = DeployChanges
                .To
                .SqlDatabase(Options.ConnectionString)
                .LogTo(new DbEngineLogger<MigrationDbEngine>(_logger))
                .JournalToSqlTable("dbo", "SchemaVersions")
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
