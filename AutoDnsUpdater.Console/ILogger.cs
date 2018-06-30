namespace AutoDnsUpdater.Console
{
    /// <summary>
    /// Provides a mechanism to log messages.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Writes an entry to the log.
        /// </summary>
        /// <param name="text">Text of the entry to write to the log.</param>
        void Write(string text);
    }
}