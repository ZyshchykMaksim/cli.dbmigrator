using System;
using CLI.DbMigrator.Loggers;
using CLI.DbMigrator.Options;
using DbUp;
using Microsoft.Extensions.Logging;

namespace CLI.DbMigrator.Engines.Implementation
{
    /// <inheritdoc cref="IDbEngine"/>
    public class TryConnectDbEngine : IDbEngine
    {
        private readonly ILogger<TryConnectDbEngine> _logger;

        private ConnectOptions Options { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TryConnectDbEngine"/> class.
        /// </summary>
        public TryConnectDbEngine(ILogger<TryConnectDbEngine> logger)
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
                case ConnectOptions connectOptions:
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
                .LogTo(new DbEngineLogger<TryConnectDbEngine>(_logger))
                .WithScriptsFromFileSystem(String.Empty)
                .Build();

            var isTryConnectResult = dbDeploy.TryConnect(out var errorMessage);

            if (!isTryConnectResult)
            {
                _logger.LogError(errorMessage);
            }
            else
            {
                _logger.LogInformation("The connect to database operation was successful.");
            }
        }
    }
}
