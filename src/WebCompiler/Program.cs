using System;
using System.Collections.Generic;
using System.Linq;

namespace WebCompiler
{
    public class Program
    {
        public static int Main(params string[] args)
        {
            string configPath = args.Length > 0 ? args[0] : "compilerconfig.json";

            var configs = GetConfigs(configPath);

            if (configs == null)
            {
                Console.WriteLine("\x1B[33mNo configurations matched");
                return 0;
            }

            ConfigFileProcessor processor = new ConfigFileProcessor();
            EventHookups(processor);

            var results = processor.Process(configPath, configs, true);
            var errorResults = results.Where(r => r.HasErrors);

            var compilerResults = errorResults as CompilerResult[] ?? errorResults.ToArray();
            foreach (var result in compilerResults)
                foreach (var error in result.Errors)
                {
                    Console.Write("\x1B[31m" + error.Message);
                }

            return compilerResults.Any() ? 1 : 0;
        }

        private static void EventHookups(ConfigFileProcessor processor)
        {
            // For console colors, see http://stackoverflow.com/questions/23975735/what-is-this-u001b9-syntax-of-choosing-what-color-text-appears-on-console

            processor.BeforeProcess += (s, e) => { Console.WriteLine($"Processing \x1B[36m{e.Config.InputFile}"); if (e.ContainsChanges) FileHelpers.RemoveReadonlyFlagFromFile(e.Config.GetAbsoluteOutputFile()); };
            processor.AfterProcess += (s, e) => { Console.WriteLine($"  \x1B[32mCompiled"); };
            processor.BeforeWritingSourceMap += (s, e) => { if (e.ContainsChanges) FileHelpers.RemoveReadonlyFlagFromFile(e.ResultFile); };
            processor.AfterWritingSourceMap += (s, e) => { Console.WriteLine($"  \x1B[32mSourcemap"); };
            processor.ConfigProcessed += (s, e) => { Console.WriteLine("\t"); };

            FileMinifier.BeforeWritingMinFile += (s, e) => { FileHelpers.RemoveReadonlyFlagFromFile(e.ResultFile); };
            FileMinifier.AfterWritingMinFile += (s, e) => { Console.WriteLine($"  \x1B[32mMinified"); };
            FileMinifier.BeforeWritingGzipFile += (s, e) => { FileHelpers.RemoveReadonlyFlagFromFile(e.ResultFile); };
            FileMinifier.AfterWritingGzipFile += (s, e) => { Console.WriteLine($"  \x1B[32mGZipped"); };
        }

        private static IEnumerable<Config> GetConfigs(string configPath)
        {
            var configs = ConfigHandler.GetConfigs(configPath);

            if (configs == null || !configs.Any())
                return null;

            return configs;
        }
    }
}
