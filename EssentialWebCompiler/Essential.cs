using System.Collections.Generic;
using WebCompiler;

namespace EssentialWebCompiler
{
    /// <summary>
    /// Library for run-time compilation of stylesheets using WebCompiler.
    /// </summary>
    public static class Essential
    {
        /// <summary>
        /// Run compilation(s) for the provided compilerconfig.json file.
        /// </summary>
        /// <param name="configurationFilePath"></param>
        /// <returns></returns>
        public static EssentialResult Compile(string configurationFilePath)
        {
            return new EssentialResult(Program.Run(configurationFilePath));
        }
    }
}
