using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using CLI.DbMigrator.Engines;
using CLI.DbMigrator.Engines.Implementation;
using CLI.DbMigrator.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CLI.DbMigrator.Factories.Implementation
{
    /// <inheritdoc cref="IDbEngineFactory"/>
    public class DbEngineFactory : IDbEngineFactory
    {
        public readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbEngineFactory"/> class.
        /// </summary>
        public DbEngineFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public IDbEngine Create<T>(T options) where T : BaseOptions
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            IDbEngine dbEngine;
            var loggerFactory = _serviceProvider.GetRequiredService<ILoggerFactory>();

            switch (options)
            {
                case ConnectOptions c:
                    var tryConnectLogger = loggerFactory.CreateLogger<TryConnectDbEngine>();

                    dbEngine = new TryConnectDbEngine(tryConnectLogger);
                    dbEngine.SetOptions(c);
                    break;
                case MigrationOptions m:
                    var migrationLogger = loggerFactory.CreateLogger<MigrationDbEngine>();

                    dbEngine = new MigrationDbEngine(migrationLogger);
                    dbEngine.SetOptions(m);
                    break;

                default:
                    throw new NotImplementedException();
            }

            return dbEngine;
        }
    }
}
