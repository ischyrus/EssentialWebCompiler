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
        public static CompilationResult Compile(string configurationFilePath)
        {
            return Program.Run(configurationFilePath);
        }
    }
}
