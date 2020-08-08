using CommandLine;

namespace CLI.DbMigrator.Options
{
    [Verb("try-connect", HelpText = "Check the database connection.")]
    public sealed class ConnectOptions : BaseOptions
    {
    }
}
