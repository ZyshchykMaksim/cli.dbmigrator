using CLI.DbMigrator.Options;

namespace CLI.DbMigrator.Engines
{
    public interface IDbEngine
    {
        void SetOptions<T>(T options) where T : BaseOptions;

        void Execute();
    }
}
