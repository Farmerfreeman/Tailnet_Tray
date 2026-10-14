using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tailnet_Tray.Models
{
    public class TailscalePeer
    {
        public string hostName { get; set; }
        public string os { get; set; }
        public bool online { get; set; }
        public List<string> ips { get; set; } = new();
    }
}
