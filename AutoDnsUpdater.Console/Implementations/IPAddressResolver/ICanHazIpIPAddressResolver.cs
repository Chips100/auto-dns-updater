using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutoDnsUpdater.Console.Implementations.IPAddressResolver
{
    /// <summary>
    /// Resolves the IP address of the current machine by calling the ICanHazIp WebService.
    /// </summary>
    public sealed class ICanHazIpIPAddressResolver : IIPAddressResolver
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        /// <summary>
        /// Creates an ICanHazIpIPAddressResolver.
        /// </summary>
        /// <param name="configuration">Configuration source.</param>
        /// <param name="logger">Logger for writing log messages.</param>
        public ICanHazIpIPAddressResolver(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Resolves the public facing IP address of the current machine.
        /// </summary>
        /// <returns>The public facing IP address of the current machine.</returns>
        public async Task<IPAddress> ResolvePublicIpAddress()
        {
            using (var client = new HttpClient())
            {
                _logger.Write("Trying to get IP from ICanHazIp WebService.");
                var response = await client.GetStringAsync(_configuration.GetString("ICanHazIpUrl"));

                _logger.Write($"Response from ICanHazIp WebService: {response}");
                return IPAddress.Parse(response.Trim(' ', '\n', '\r'));
            }
        }
    }
}