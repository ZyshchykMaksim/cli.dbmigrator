using CommandLine;

namespace CLI.DbMigrator.Options
{
    public class BaseOptions
    {
        [Option('c', "connection", Required = true, HelpText = "The database connection string.")]
        public virtual string ConnectionString { get; set; }
    }
}
