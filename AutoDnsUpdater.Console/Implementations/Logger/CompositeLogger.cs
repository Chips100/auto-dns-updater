using System;
using System.Collections.Generic;

namespace AutoDnsUpdater.Console.Implementations.Logger
{
    /// <summary>
    /// Decorator that delegates each call to multiple implementations of ILogger.
    /// </summary>
    public sealed class CompositeLogger : ILogger
    {
        private readonly IEnumerable<ILogger> _loggers;

        /// <summary>
        /// Creates a CompositeLogger.
        /// </summary>
        /// <param name="loggers">Sequence with loggers that should be delegated to.</param>
        public CompositeLogger(IEnumerable<ILogger> loggers)
        {
            _loggers = loggers ?? throw new ArgumentNullException(nameof(loggers));
        }

        /// <summary>
        /// Writes an entry to the log.
        /// </summary>
        /// <param name="text">Text of the entry to write to the log.</param>
        public void Write(string text)
        {
            foreach (var logger in _loggers) logger.Write(text);
        }
    }
}