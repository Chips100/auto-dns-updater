using System;

namespace AutoDnsUpdater.Console.Implementations.Logger
{
    /// <summary>
    /// Extension methods in the context of logging.
    /// </summary>
    public static class LoggerExtensions
    {
        private static readonly string NewLine = Environment.NewLine;
        private static readonly string Separator = "-------------------" + NewLine;

        /// <summary>
        /// Formats the specified raw log message as a log entry.
        /// </summary>
        /// <param name="text">Raw log message.</param>
        /// <returns>The formatted text for the log entry.</returns>
        public static string FormatAsLogEntry(this string text) =>
            $"{Separator}{ DateTime.Now }{NewLine}{ text }{NewLine}";
    }
}