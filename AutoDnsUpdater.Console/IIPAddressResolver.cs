using System.Net;
using System.Threading.Tasks;

namespace AutoDnsUpdater.Console
{
    /// <summary>
    /// Resolves the IP address of the current machine.
    /// </summary>
    public interface IIPAddressResolver
    {
        /// <summary>
        /// Resolves the public facing IP address of the current machine.
        /// </summary>
        /// <returns>The public facing IP address of the current machine.</returns>
        Task<IPAddress> ResolvePublicIpAddress();
    }
}