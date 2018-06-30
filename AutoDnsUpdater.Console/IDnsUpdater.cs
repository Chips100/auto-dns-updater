using System.Net;
using System.Threading.Tasks;

namespace AutoDnsUpdater.Console
{
    /// <summary>
    /// Updates the DNS entry to point to the specified IP address.
    /// </summary>
    public interface IDnsUpdater
    {
        /// <summary>
        /// Updates the DNS entry to point to the specified IP address.
        /// </summary>
        /// <param name="ip">IP address that the DNS entry should point to.</param>
        /// <returns>A task that will complete when the DNS entry has been updated.</returns>
        Task UpdateDnsEntry(IPAddress ip);
    }
}