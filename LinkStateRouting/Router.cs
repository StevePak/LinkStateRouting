using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkStateRouting
{
    class Router
    {
        public int routerId { get; set; }
        public string networkName { get; set; }
        public int networkCost { get; set; }

        public List<Router> Connections { get; set; }

        public Dictionary<int, RoutingInfo> RoutingTable { get; set; }

        public void originatePackage()
        {

        }

        public void receivePackage(LSP pack)
        {

        }

        public class RoutingInfo
        {
            public int RouterId { get; set; }
            public int Cost { get; set; }
            public int Link { get; set; }
        }
    }
}
