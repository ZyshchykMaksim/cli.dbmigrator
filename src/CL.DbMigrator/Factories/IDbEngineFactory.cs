using System;
using System.Collections.Generic;
using System.Text;
using CLI.DbMigrator.Engines;
using CLI.DbMigrator.Options;
using DbUp.Engine;

namespace CLI.DbMigrator.Factories
{
    public interface IDbEngineFactory
    {
        IDbEngine Create<T>(T options) where T : BaseOptions;
    }
}
