using System;
using System.Collections.Generic;
using System.Text;
using CLI.DbMigrator.Engines.Implementation;
using DbUp.Engine.Output;
using Microsoft.Extensions.Logging;

namespace CLI.DbMigrator.Loggers
{
    /// <inheritdoc cref="IUpgradeLog"/>
    public class DbEngineLogger<T> : IUpgradeLog where T : class
    {
        private readonly ILogger<T> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbEngineLogger<T>"/> class.
        /// </summary>
        public DbEngineLogger(ILogger<T> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public void WriteInformation(string format, params object[] args)
        {
            _logger.LogInformation(format, args);
        }

        /// <inheritdoc />
        public void WriteError(string format, params object[] args)
        {
            _logger.LogError(format, args);
        }

        /// <inheritdoc />
        public void WriteWarning(string format, params object[] args)
        {
            _logger.LogWarning(format, args);
        }
    }
}
