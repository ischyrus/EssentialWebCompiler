using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace EssentialWebCompilerTests
{
    [TestClass]
    public class InitializationRecoveryTests
    {
        /// <summary>
        /// Deleting the node.exe from the temp folder where the node dependencies are stored should
        /// result in reinitialization and recovery
        /// </summary>
        [TestMethod]
        public void CompilerCanRecoverFromNodeExeBeingDeleted()
        {
            var result = EssentialWebCompiler.Essential.Compile("compilerconfig.json");
            Assert.AreEqual(0, result.Errors.Count);

            File.Delete(Path.Combine(WebCompiler.CompilerService.TempFolderPath, "node.exe"));

            result = EssentialWebCompiler.Essential.Compile("compilerconfig.json");
            Assert.AreEqual(0, result.Errors.Count);
        }

        /// <summary>
        /// Deleting necessary node_modules contents from the temp folder where the node dependencies are stored should
        /// result in reinitialization and recovery
        /// </summary>
        [TestMethod]
        public void CompilerCanRecoverFromNodeModulesContentsBeingDeleted()
        {
            var result = EssentialWebCompiler.Essential.Compile("compilerconfig.json");
            Assert.AreEqual(0, result.Errors.Count);

            Directory.Delete(Path.Combine(WebCompiler.CompilerService.TempFolderPath, "node_modules/less"), true);

            result = EssentialWebCompiler.Essential.Compile("compilerconfig.json");
            Assert.AreEqual(0, result.Errors.Count);
        }
    }
}
