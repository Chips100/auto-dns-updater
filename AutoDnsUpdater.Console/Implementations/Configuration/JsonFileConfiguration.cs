using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutoDnsUpdater.Console.Implementations.Configuration
{
    /// <summary>
    /// Provides access to configuration values stored in a JSON-File.
    /// </summary>
    public sealed class JsonFileConfiguration : IConfiguration
    {
        private readonly IDictionary<string, object> _configuration;

        /// <summary>
        /// Creates a JsonFileConfiguration.
        /// </summary>
        /// <param name="jsonFile">Full path to the JSON file that holds the configuration values.</param>
        public JsonFileConfiguration(string jsonFile)
        {
            _configuration = Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary<string, object>>(File.ReadAllText(jsonFile));
        }

        /// <summary>
        /// Gets a configuration value as a string.
        /// </summary>
        /// <param name="key">Key of the configuration value to get.</param>
        /// <returns>The configured value for the specified key.</returns>
        public string GetString(string key)
        {
            return _configuration[key] as string;
        }

        /// <summary>
        /// Gets a configuration value as an array of Int32.
        /// </summary>
        /// <param name="key">Key of the configuration value to get.</param>
        /// <returns>The configured values for the specified key.</returns>
        public int[] GetInt32Array(string key)
        {
            return ((JArray)_configuration[key]).ToObject<int[]>();
        }
    }
}