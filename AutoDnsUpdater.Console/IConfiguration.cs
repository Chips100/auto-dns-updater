namespace AutoDnsUpdater.Console
{
    /// <summary>
    /// Provides access to configuration values.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets a configuration value as a string.
        /// </summary>
        /// <param name="key">Key of the configuration value to get.</param>
        /// <returns>The configured value for the specified key.</returns>
        string GetString(string key);
    }
}