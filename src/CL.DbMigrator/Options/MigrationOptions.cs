using CommandLine;

namespace CLI.DbMigrator.Options
{
    [Verb("migration", true, HelpText = "Adds changes to the database.")]
    public sealed class MigrationOptions : BaseOptions
    {
        [Option('d', "directory", Required = true, HelpText = "The path to directory where SQL scripts are stored.")]
        public string Directory { get; set; }


        [Option('t', "transaction", Required = false, Default = true, HelpText = "Allows to roll back changes if an error occurs.")]
        public bool WithTransaction { get; set; }
    }
}
