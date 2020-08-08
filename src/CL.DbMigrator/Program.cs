using System.IO;
using CLI.DbMigrator.Factories;
using CLI.DbMigrator.Factories.Implementation;
using CLI.DbMigrator.Loggers;
using DbUp.Engine.Output;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CLI.DbMigrator
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<App>().Run(args);
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            var config = LoadConfiguration();
            var logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(config)
                .CreateLogger();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(logger: logger, dispose: true));

            services.AddSingleton(config);
            services.AddTransient<App>();
            services.AddTransient<IDbEngineFactory, DbEngineFactory>();

            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return configuration;
        }
    }
}