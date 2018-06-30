namespace AutoDnsUpdater.Console.Implementations.Logger
{
    /// <summary>
    /// Writes log entries to the console.
    /// </summary>
    public sealed class ConsoleLogger : ILogger
    {
        /// <summary>
        /// Writes an entry to the log.
        /// </summary>
        /// <param name="text">Text of the entry to write to the log.</param>
        public void Write(string text)
        {
            System.Console.WriteLine(text.FormatAsLogEntry());
        }
    }
}