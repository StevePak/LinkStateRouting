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
        public int sequenceId { get; set; }
        public bool started { get; set; }

        public HashSet<Connection> Connections { get; set; }

        public List<RoutingInfo> RoutingTable { get; set; }

        public Dictionary<int, LSP> LSDB { get; set; }

        public class RoutingInfo
        {
            public int RouterId { get; set; }
            public int Cost { get; set; }
            public int Link { get; set; }

            public RoutingInfo(int id, int cost, int link)
            {
                RouterId = id;
                Cost = cost;
                Link = link;
            }
        }

        public class Connection
        {
            public Router Router { get; set; }
            public int Cost { get; set; }
            public int TickCount { get; set; }

            public Connection(Router r, int c)
            {
                Router = r;
                Cost = c;
                TickCount = 0;
            }
        }

        public void OriginatePackage()
        {
            if (!started)
            {
                return;
            }

            foreach (var item in Connections)
            {
                item.TickCount++;
                if (item.TickCount > 2)
                {
                    item.Cost = int.MaxValue;
                }
            }

            Dictionary<int, int> links = new Dictionary<int, int>();
            foreach (var item in Connections)
            {
                links.Add(item.Router.routerId, item.Cost);
            }
            LSP lsp = new LSP(routerId, ++sequenceId, links);
            replaceLSDB(lsp);
            foreach (var item in Connections)
            {
                item.Router.ReceivePackage(lsp);
            }
        }

        public void ReceivePackage(LSP newLSP)
        {
            if (!started)
            {
                return;
            }

            newLSP.TTL--;
            LSP oldLSP;
            LSDB.TryGetValue(newLSP.SourceId, out oldLSP);
            if (newLSP.TTL > 0 && newLSP.Newer(oldLSP))
            {
                replaceLSDB(newLSP);
                if (newLSP.ConnectivityChanged(oldLSP))
                {
                    ConstructRoutingTable();
                }
                foreach (var item in Connections)
                {
                    if (item.Router.routerId != this.routerId)
                        item.Router.ReceivePackage(newLSP);
                    else
                        item.TickCount++;
                }
            }
        }

        private void replaceLSDB(LSP newLSP)
        {
            LSDB[newLSP.SourceId] = newLSP;
        }

        public void ConstructRoutingTable()
        {

        }

        public void addConnection(Router r, int c)
        {
            Connections.Add(new Connection(r, c));
        }

    }
}
