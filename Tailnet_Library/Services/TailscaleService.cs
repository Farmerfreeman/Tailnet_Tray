using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tailnet_Library.Services.Interfaces;
using Tailnet_Library.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Tailnet_Library.Services
{
    public class TailscaleService : ITailscaleService
    {
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public TailscaleService() { }



        public async Task<TailscaleStatus> GetStatusAsync()
        {
            string output;
            //run tailscale status --json command and capture the output
            Process process = CreateProcess();
            process.Start();

            
            try
            {
                output = await process.StandardOutput.ReadToEndAsync();
                await process.WaitForExitAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to execute tailscale command.", ex);
            }

            // Deserialize the JSON output into a TailscaleStatus object

            TailscaleStatus status = JsonSerializer.Deserialize<TailscaleStatus>(output, _jsonOptions);

            if (status == null)
            {
                throw new InvalidOperationException("Failed to deserialize tailscale status.");
            }

            return status;
        }

        // Extract process creation so tests can override and return a test process
        protected virtual Process CreateProcess()
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "tailscale",
                Arguments = "status --json",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            return new Process
            {
                StartInfo = psi
            };
        }
    }
}
