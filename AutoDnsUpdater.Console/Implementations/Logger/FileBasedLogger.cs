using System;
using System.IO;
using System.Text;

namespace AutoDnsUpdater.Console.Implementations.Logger
{
    /// <summary>
    /// Writes log entries to a file.
    /// </summary>
    public sealed class FileBasedLogger : ILogger
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Creates a FileBasedLogger.
        /// </summary>
        /// <param name="configuration">Configuration source.</param>
        public FileBasedLogger(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Writes an entry to the log.
        /// </summary>
        /// <param name="text">Text of the entry to write to the log.</param>
        public void Write(string text)
        {
            File.AppendAllText(_configuration.GetString("LogFile"), text.FormatAsLogEntry(), Encoding.UTF8);
        }
    }
}