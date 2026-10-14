using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tailnet_Tray.Models
{
    public class TailscaleStatus
    {
        public string backendState { get; set; }
        public TailscalePeer? self { get; set; }
        public Dictionary<string, TailscalePeer> peers { get; set; } = new();

    }
}
