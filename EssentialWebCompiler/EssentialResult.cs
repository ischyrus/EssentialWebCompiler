using System.Collections.Generic;
using WebCompiler;

namespace EssentialWebCompiler
{
    /// <summary>
    /// Results of WebCompiler compilation attempt.
    /// </summary>
    public class EssentialResult
    {
        public EssentialResult(WebCompilerResult webCompilerResult)
        {
            Errors = webCompilerResult.Errors;
            Log = webCompilerResult.Log;
        }

        /// <summary>
        /// Errors encountered while attempting to compile the stylesheet
        /// </summary>
        public List<string> Errors { get; }

        /// <summary>
        /// Logged compilation events
        /// </summary>
        public string Log { get; }
    }
}
