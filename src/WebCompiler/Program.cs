using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WebCompiler
{
    /// <summary>
    /// Given a compilerconfig.json configuration file, compiles each
    /// included stylesheet with specified options.
    /// </summary>
    public class Program
    {
        private static WebCompilerResult _result;

        /// <summary>
        /// Run the compiler with the provided configuration file and capture results.
        /// </summary>
        /// <param name="configurationFilePath"></param>
        /// <returns></returns>
        public static WebCompilerResult Run(string configurationFilePath)
        {
            Main(configurationFilePath);

            return _result;
        }

        private static int FailedCompilationAttempts = 0;

        /// <summary>
        /// Entry-point for compilation
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int Main(params string[] args)
        {
            _result = new WebCompilerResult();
            string configPath = args.Length > 0 ? args[0] : "compilerconfig.json";

            var configs = GetConfigs(configPath);

            if (configs == null)
            {
                Log("No configurations matched.", true);
                return 1;
            }

            ConfigFileProcessor processor = new ConfigFileProcessor();
            EventHookups(processor);

            bool problemEncountered = false;

            IEnumerable<CompilerResult> results = new CompilerResult[]{};
            try
            {
                results = processor.Process(configPath, configs, true, FailedCompilationAttempts > 0);
            }
            catch (Exception e)
            {
                problemEncountered = true;
                if (FailedCompilationAttempts > 0)
                    throw;
            }

            var errorResults = results.Where(r => r.HasErrors).ToArray();

            foreach (var result in errorResults)
            {
                problemEncountered = true;
                foreach (var error in result.Errors)
                    Log(error.Message, true);
            }

            //If issues were encountered with compilation, re-initialize the Node dependencies and try one more time
            if (problemEncountered)
            {
                FailedCompilationAttempts++;
                return Main(args);
            }
            FailedCompilationAttempts = 0;

            return errorResults.Any() ? 1 : 0;
        }

        private static void EventHookups(ConfigFileProcessor processor)
        {
            // For console colors, see http://stackoverflow.com/questions/23975735/what-is-this-u001b9-syntax-of-choosing-what-color-text-appears-on-console

            processor.BeforeProcess += (s, e) => { Log($"Processing {e.Config.InputFile}"); if (e.ContainsChanges) FileHelpers.RemoveReadonlyFlagFromFile(e.Config.GetAbsoluteOutputFile()); };
            processor.AfterProcess += (s, e) => { Log($"  Compiled"); };
            processor.BeforeWritingSourceMap += (s, e) => { if (e.ContainsChanges) FileHelpers.RemoveReadonlyFlagFromFile(e.ResultFile); };
            processor.AfterWritingSourceMap += (s, e) => { Log("  Source-mapped"); };
            processor.ConfigProcessed += (s, e) => { Log("\t"); };

            FileMinifier.BeforeWritingMinFile += (s, e) => { FileHelpers.RemoveReadonlyFlagFromFile(e.ResultFile); };
            FileMinifier.AfterWritingMinFile += (s, e) => { Log("  Minified"); };
            FileMinifier.BeforeWritingGzipFile += (s, e) => { FileHelpers.RemoveReadonlyFlagFromFile(e.ResultFile); };
            FileMinifier.AfterWritingGzipFile += (s, e) => { Log("  32mGZipped"); };
        }

        private static IEnumerable<Config> GetConfigs(string configPath)
        {
            var configs = ConfigHandler.GetConfigs(configPath);

            var enumerable = configs as Config[] ?? configs.ToArray();
            if (!enumerable.Any())
                return null;

            return enumerable;
        }

        /// <summary>
        /// Log the messages to the console for standalone execution and a result DTO in case its being run programmatically
        /// </summary>
        /// <param name="message"></param>
        /// <param name="isError"></param>
        private static void Log(string message, bool isError = false)
        {
            _result.LogEvent(message, isError);
            Console.WriteLine(message);
        }
    }
}
