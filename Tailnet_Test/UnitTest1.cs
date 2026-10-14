using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using Tailnet_Library.Services;
using Tailnet_Library.Models;
using Xunit;

namespace Tailnet_Test
{
    public class TailscaleServiceTests
    {
        [Fact]
        public async Task GetStatusAsync_ReturnsDeserializedObject_FromJsonOutput()
        {
            // Arrange: create a temp file containing the expected JSON output
            string json = "{\"backendState\":\"running\",\"self\":{\"hostName\":\"test-host\",\"os\":\"windows\",\"online\":true,\"ips\":[\"1.2.3.4\"]},\"peers\":{}}";
            string temp = Path.GetTempFileName();
            File.WriteAllText(temp, json);

            try
            {
                var svc = new TestableTailscaleService(temp);

                // Act
                TailscaleStatus status = await svc.GetStatusAsync();

                // Assert
                Assert.NotNull(status);
                Assert.Equal("running", status.backendState);
                Assert.NotNull(status.self);
                Assert.Equal("test-host", status.self.hostName);
                Assert.Contains("1.2.3.4", status.self.ips);
            }
            finally
            {
                File.Delete(temp);
            }
        }

        private class TestableTailscaleService : TailscaleService
        {
            private readonly string _filePath;

            public TestableTailscaleService(string filePath)
            {
                _filePath = filePath;
            }

            protected override Process CreateProcess()
            {
                // Use cmd.exe to output the contents of the temp file so the service reads it from stdout
                var psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c type \"{_filePath}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                return new Process { StartInfo = psi };
            }
        }
    }
}
