using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tailnet_Library.Models
{
    public class TailscalePeer
    {
        public string HostName { get; set; }
        public string OS { get; set; }
        public bool Online { get; set; }
        public List<string> IPs  { get; set; } = new();
    }
}
