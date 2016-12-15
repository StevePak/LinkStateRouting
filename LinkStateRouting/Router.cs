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

        public List<Connection> Connections { get; set; }

        public List<RoutingInfo> RoutingTable { get; set; }

        public List<LSP> LSDB { get; set; }

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
                    item.Cost = Int32.MaxValue;
                }
            }

            List<LSP.RouterCost> list = new List<LSP.RouterCost>();
            foreach (var item in Connections)
            {
                list.Add(new LSP.RouterCost(item.Router.routerId, item.Cost));
            }
            LSP lsp = new LSP(routerId, ++sequenceId, list);
            replaceLSDB(lsp);
            foreach (var item in Connections)
            {
                item.Router.ReceivePackage(lsp);
            }
        }

        public void ReceivePackage(LSP pack)
        {
            if (!started)
            {
                return;
            }

            pack.TTL--;
            LSP lsp = LSDB.Where(x => x.SourceId == pack.SourceId).SingleOrDefault();
            bool newer = replaceLSDB(pack);
            if (pack.TTL > 0 && newer)
            {
                foreach (var item in pack.ReachableNetwork)
                {
                    if (lsp == null || lsp.ReachableNetwork.Where(x => x.RouterId == item.RouterId).SingleOrDefault().TotalCost != item.TotalCost)
                    {
                        ConstructRoutingTable();
                        continue;
                    }
                }
                foreach (var item in Connections)
                {
                    if (item.Router.routerId != pack.SourceId)
                        item.Router.ReceivePackage(pack);
                    else
                        item.TickCount++;
                }
            }
        }

        private bool replaceLSDB(LSP newLSP)
        {
            LSP oldLSP = LSDB.Where(x => x.SourceId == newLSP.SourceId).SingleOrDefault();
            if (oldLSP != null)
            {
                LSDB.Add(newLSP);
                return true;
            }
            else
            {
                if (oldLSP.SequenceId >= newLSP.SequenceId)
                {
                    LSDB.Remove(oldLSP);
                    LSDB.Add(newLSP);
                    return true;
                }
                else
                    return false;
            }

        }

        public void ConstructRoutingTable()
        {
           
        }

    }
}
