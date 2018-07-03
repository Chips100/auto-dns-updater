using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutoDnsUpdater.Console.Implementations.DnsUpdater
{
    /// <summary>
    /// Updates DNS entries in the FreeDNS configuration by calling
    /// the Web endpoint for managing a domain's DNS entries.
    /// </summary>
    public sealed class FreeDNSDnsUpdater : IDnsUpdater
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        /// <summary>
        /// Creates a FreeDNSDnsUpdater.
        /// </summary>
        /// <param name="configuration">Configuration source.</param>
        /// <param name="logger">Logger for writing log messages.</param>
        public FreeDNSDnsUpdater(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Updates the DNS entry to point to the specified IP address.
        /// </summary>
        /// <param name="ip">IP address that the DNS entry should point to.</param>
        /// <returns>A task that will complete when the DNS entry has been updated.</returns>
        public async Task UpdateDnsEntry(IPAddress ipAddress)
        {
            if (ipAddress == null) throw new ArgumentNullException(nameof(ipAddress));

            using (var client = new HttpClient())
            {
                // Set HTTP POST body content.
                var httpContent = new FormUrlEncodedContent(
                    GetPostBodyValues(_configuration.GetInt32Array("FreeDNSUpdateDnsEntryIds"), ipAddress));

                // Set headers for request.
                httpContent.Headers.Add("Cookie", _configuration.GetString("FreeDNSCookie"));

                // Send request.
                _logger.Write("Trying to change DNS entry by FreeDNS Web App.");
                var response = await client.PostAsync(_configuration.GetString("FreeDNSUpdateDnsEntryUrl"), httpContent);
                
                _logger.Write(await GetLogTextForResponse(response));
                response.EnsureSuccessStatusCode();
            }
        }

        private IEnumerable<KeyValuePair<string, string>> GetPostBodyValues(int[] dnsEntryIds, IPAddress ipAddress)
        {
            yield return bodyValue("action", "execute");
            yield return bodyValue("type", "A");
            yield return bodyValue("dst", ipAddress.ToString());

            //Add ID values as Array in Form Content.
            foreach (var id in dnsEntryIds)
            {
                yield return bodyValue("dataset[]", id.ToString());
            }

            // Shortcut for creating KeyValuePairs.
            KeyValuePair<string, string> bodyValue(string key, string value) 
                => new KeyValuePair<string, string>(key, value);
        }

        private async Task<string> GetLogTextForResponse(HttpResponseMessage response) =>
            $"Response from FreeDNS Web App: {response.StatusCode} - {response.ReasonPhrase}" +
                // Only include full response body if unsuccessful.
                (response.IsSuccessStatusCode ? string.Empty : Environment.NewLine + await response.Content.ReadAsStringAsync());
    }
}