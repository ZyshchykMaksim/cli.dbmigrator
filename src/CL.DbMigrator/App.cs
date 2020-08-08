using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CLI.DbMigrator.Factories;
using CLI.DbMigrator.Options;
using CommandLine;
using CommandLine.Text;

namespace CLI.DbMigrator
{
    public sealed class App
    {
        private readonly IDbEngineFactory _dbEngineFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App(IDbEngineFactory dbEngineFactory)
        {
            _dbEngineFactory = dbEngineFactory;
        }

        public void Run(string[] args)
        {
            var types = LoadVerbs();
            var parser = new Parser(with => with.HelpWriter = null);
            var parserResult = parser.ParseArguments(args, types);

            parserResult
                .WithParsed<BaseOptions>(Run)
                .WithNotParsed(errs => DisplayHelp(parserResult, errs));
        }

        private void Run<T>(T obj) where T : BaseOptions
        {
            try
            {
                var dbEngine = _dbEngineFactory.Create(obj);

                dbEngine.Execute();
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private static void DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs)
        {
            var helpText = HelpText.AutoBuild(result, h =>
            {
                h.AddDashesToOption = true;
                h.AdditionalNewLineAfterOption = true;

                return HelpText.DefaultParsingErrorsHandler(result, h);
            }, e => e, verbsIndex: true);

            Console.WriteLine(helpText);
        }

        /// <summary>
        /// Gets all verb options.
        /// </summary>
        /// <returns></returns>
        private static Type[] LoadVerbs()
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetCustomAttribute<VerbAttribute>() != null)
                .ToArray();
        }

        private static void DisplayError(Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
    }
}
