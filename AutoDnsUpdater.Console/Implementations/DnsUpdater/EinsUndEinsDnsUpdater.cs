using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutoDnsUpdater.Console.Implementations.DnsUpdater
{
    /// <summary>
    /// Updates DNS entries in the 1&1 hosting configuration by calling
    /// the Web endpoint for managing a domain's DNS entries.
    /// </summary>
    public sealed class EinsUndEinsDnsUpdater : IDnsUpdater
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        /// <summary>
        /// Creates an EinsUndEinsDnsUpdater.
        /// </summary>
        /// <param name="configuration">Configuration source.</param>
        /// <param name="logger">Logger for writing log messages.</param>
        public EinsUndEinsDnsUpdater(IConfiguration configuration, ILogger logger)
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
                var httpContent = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "__sendingdata", "1" },
                    { "record.forWwwSubdomain", "true" },
                    { "record.value", ipAddress.ToString() },
                    { "record.ttl", _configuration.GetString("EinsUndEinsDnsEntryTtl") }
                });

                // Set headers for request.
                httpContent.Headers.Add("Cookie", _configuration.GetString("EinsUndEinsCookie"));
                client.DefaultRequestHeaders.UserAgent.ParseAdd(_configuration.GetString("EinsUndEinsUserAgent"));

                // Send request.
                _logger.Write("Trying to change DNS entry by 1&1 Web App.");
                var response = await client.PostAsync(_configuration.GetString("EinsUndEinsUpdateDnsEntryUrl"), httpContent);

                _logger.Write(await GetLogTextForResponse(response));
                response.EnsureSuccessStatusCode();
            }
        }

        private async Task<string> GetLogTextForResponse(HttpResponseMessage response) =>
            $"Response from 1&1 Web App: {response.StatusCode} - {response.ReasonPhrase}" +
                // Only include full response body if unsuccessful.
                (response.IsSuccessStatusCode ? string.Empty : Environment.NewLine + await response.Content.ReadAsStringAsync());
    }
}