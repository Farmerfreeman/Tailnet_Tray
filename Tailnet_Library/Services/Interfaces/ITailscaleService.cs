using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tailnet_Library.Models;

namespace Tailnet_Library.Services.Interfaces
{
    public interface ITailscaleService
    {
        /// <summary>
        /// Provides the current status of Tailscale in a serialized JSON format.
        /// </summary>
        /// <returns>
        /// Serialized JSON string representing the Tailscale status.
        /// </returns>
        public Task<TailscaleStatus> GetStatusAsync();
    }
}
