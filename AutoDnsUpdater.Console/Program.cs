using AutoDnsUpdater.Console.Implementations.Configuration;
using AutoDnsUpdater.Console.Implementations.DnsUpdater;
using AutoDnsUpdater.Console.Implementations.IPAddressResolver;
using AutoDnsUpdater.Console.Implementations.Logger;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AutoDnsUpdater.Console
{
    class Program
    {
        // Poor man's dependency inversion:
        private static readonly IConfiguration _configuration = new JsonFileConfiguration("config.json");
        private static readonly ILogger _logger = new CompositeLogger(new ILogger[] { new FileBasedLogger(_configuration), new ConsoleLogger() });
        private static readonly IIPAddressResolver _ipAddressResolver = new ICanHazIpIPAddressResolver(_configuration, _logger);
        private static readonly IDnsUpdater _dnsUpdater = new EinsUndEinsDnsUpdater(_configuration, _logger);

        static void Main(string[] args)
        {
            MainAsync().Wait();

            if (args.Contains("keepopen"))
            {
                System.Console.WriteLine(Environment.NewLine + "Press enter to exit...");
                System.Console.ReadLine();
            }
        }

        static async Task MainAsync()
        {
            var ipAddress = await _ipAddressResolver.ResolvePublicIpAddress();
            await _dnsUpdater.UpdateDnsEntry(ipAddress);
        }
    }
}