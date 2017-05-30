using System.Collections.Generic;
using System.Text;

namespace WebCompiler
{
    /// <summary>
    /// Results of stylesheet compilation attempt
    /// </summary>
    public class WebCompilerResult
    {
        /// <summary>
        /// Errors encountered while attempting to compile the stylesheet
        /// </summary>
        public List<string> Errors { get; } = new List<string>();

        private readonly StringBuilder _log = new StringBuilder();

        /// <summary>
        /// Log an event
        /// </summary>
        internal void LogEvent(string message, bool isError = false)
        {
            _log.AppendLine(message);
            if (isError)
                Errors.Add(message);
        }

        /// <summary>
        /// Logged compilation events
        /// </summary>
        public string Log => _log.ToString();
    }
}
