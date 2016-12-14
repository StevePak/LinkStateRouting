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

        public List<Connection> Connections { get; set; }

        public Dictionary<int, RoutingInfo> RoutingTable { get; set; }

        public class RoutingInfo
        {
            public int RouterId { get; set; }
            public int Cost { get; set; }
            public int Link { get; set; }
        }

        public class Connection
        {
            public Router Router;
            public int RecentSequence;
            public int TickCount;
        }

        public void OriginatePackage()
        {

        }

        public void ReceivePackage(LSP pack)
        {
            pack.TTL--;
            Connection c = Connections.Where(x => x.Router.routerId == pack.SourceId).Single();
            if (pack.TTL == 0 || c.RecentSequence >= pack.SequenceId)
            {
                return;
            }
        }

        public void ConstructRoutingTable()
        {
           
        }

    }
}
