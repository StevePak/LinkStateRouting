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

        public Dictionary<int, RoutingInfo> RoutingTable { get; set; }

        public Dictionary<int, LSP> LSDB { get; set; }

        public class RoutingInfo
        {
            public Router RouterId { get; set; }
            public int Cost { get; set; }
            public int Link { get; set; }

            public RoutingInfo(Router id)
            {
                RouterId = id;
                Cost = int.MaxValue;
                Link = -1;
            }
        }

        public class Connection
        {
            private int cost;
            public Router Router { get; set; }
            public int Cost { get { return Maxed ? int.MaxValue : cost; } set { cost = value; } }
            public int TickCount { get; set; }
            public bool Maxed { get; set; }

            public Connection(Router r, int c)
            {
                Router = r;
                Cost = c;
                TickCount = 0;
                Maxed = false;
            }
        }

        public Router(int id, string name)
        {
            routerId = id;
            networkName = name;
            sequenceId = 1;
            started = true;
            Connections = new HashSet<Connection>();
            RoutingTable = new Dictionary<int, RoutingInfo>();
            LSDB = new Dictionary<int, LSP>();
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
                    item.Maxed = true;
                }
            }

            Dictionary<int, int> links = new Dictionary<int, int>();
            foreach (var item in Connections)
            {
                links.Add(item.Router.routerId, item.Cost);
            }
            LSP lsp = new LSP(routerId, ++sequenceId, links);
            replaceLSDB(lsp);
            ConstructRoutingTable();
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

            foreach (var item in Connections)
            {
                if (item.Router.routerId == newLSP.SourceId)
                {
                    item.TickCount = 0;
                    item.Maxed = false;
                }
            }
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
                    item.Router.ReceivePackage(newLSP);
                }
            }
        }

        private void replaceLSDB(LSP newLSP)
        {
            LSDB[newLSP.SourceId] = newLSP;
        }

        public void ConstructRoutingTable()
        {

            if (this.routerId == 0)
                ;

            foreach (var item in RoutingTable.Values)
            {
                item.Cost = int.MaxValue;
                item.Link = -1;
            }

            RoutingTable[this.routerId].Cost = 0;
            
            PriorityQueue<Router, int> PQ = new PriorityQueue<Router, int>();
            List<int> connectingIDs = this.Connections.Select(x => x.Router.routerId).ToList();

            

            foreach (var item in RoutingTable)
            {
                PQ.Enqueue(item.Value.RouterId, int.MaxValue);
            }
            
            PQ.ChangePriority(this, 0);

            while (PQ.Count > 0)
            {
                Router r = PQ.Dequeue();

                foreach (var item in r.Connections)
                {
                    int c = RoutingTable[r.routerId].Cost + item.Cost;
                    if (c < RoutingTable[item.Router.routerId].Cost && c > 0)
                    {
                        RoutingTable[item.Router.routerId].Cost = c;
                        if(connectingIDs.Contains(item.Router.routerId))
                        {
                            RoutingTable[item.Router.routerId].Link = item.Router.routerId;
                        }
                        else
                        {
                            int id = r.routerId;
                            while (!connectingIDs.Contains(id))
                            {
                                id = RoutingTable[id].Link;
                            }
                            RoutingTable[item.Router.routerId].Link = id;
                        }
                        PQ.ChangePriority(item.Router, c);
                    }
                }
            }
        }

        public void addConnection(Router r, int c)
        {
            Connections.Add(new Connection(r, c));
        }

        public void printRoutingTable()
        {

            Console.WriteLine("\nRouting Table for Router ID " + routerId);
            foreach (var item in RoutingTable)
            {
                if(!(item.Value.RouterId.routerId == this.routerId))
                    Console.WriteLine($"{item.Value.RouterId.networkName}, {item.Value.Cost}, {item.Value.Link}");
            }
        }
    }
}
