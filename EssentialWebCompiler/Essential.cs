using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompiler;

namespace EssentialWebCompiler
{
    public static class Essential
    {
        public static bool Compile(string configurationFilePath)
        {
            return Program.Main(configurationFilePath) == 0;
        }
    }
}
