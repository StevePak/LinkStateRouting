using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkStateRouting
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Router r0 = new Router(0, "155.246.80");
            Router r1 = new Router(1, "155.246.81");
            Router r2 = new Router(2, "155.246.82");
            Router r3 = new Router(3, "155.246.83");
            Router r4 = new Router(4, "155.246.84");
            Router r5 = new Router(5, "155.246.85");
            Router r6 = new Router(6, "155.246.86");

            r0.Connections.Add(new Router.Connection(r1, 1));
            r0.Connections.Add(new Router.Connection(r2, 1));
            r0.RoutingTable.Add(r0.routerId, new Router.RoutingInfo(r0));
            r0.RoutingTable.Add(r1.routerId, new Router.RoutingInfo(r1));
            r0.RoutingTable.Add(r2.routerId, new Router.RoutingInfo(r2));
            r0.RoutingTable.Add(r3.routerId, new Router.RoutingInfo(r3));
            r0.RoutingTable.Add(r4.routerId, new Router.RoutingInfo(r4));
            r0.RoutingTable.Add(r5.routerId, new Router.RoutingInfo(r5));
            r0.RoutingTable.Add(r6.routerId, new Router.RoutingInfo(r6));

            r1.Connections.Add(new Router.Connection(r0, 1));
            r1.Connections.Add(new Router.Connection(r2, 1));
            r1.Connections.Add(new Router.Connection(r4, 1));
            r1.RoutingTable.Add(r0.routerId, new Router.RoutingInfo(r0));
            r1.RoutingTable.Add(r1.routerId, new Router.RoutingInfo(r1));
            r1.RoutingTable.Add(r2.routerId, new Router.RoutingInfo(r2));
            r1.RoutingTable.Add(r3.routerId, new Router.RoutingInfo(r3));
            r1.RoutingTable.Add(r4.routerId, new Router.RoutingInfo(r4));
            r1.RoutingTable.Add(r5.routerId, new Router.RoutingInfo(r5));
            r1.RoutingTable.Add(r6.routerId, new Router.RoutingInfo(r6));

            r2.Connections.Add(new Router.Connection(r0, 1));
            r2.Connections.Add(new Router.Connection(r1, 1));
            r2.Connections.Add(new Router.Connection(r3, 1));
            r2.Connections.Add(new Router.Connection(r5, 1));
            r2.RoutingTable.Add(r0.routerId, new Router.RoutingInfo(r0));
            r2.RoutingTable.Add(r1.routerId, new Router.RoutingInfo(r1));
            r2.RoutingTable.Add(r2.routerId, new Router.RoutingInfo(r2));
            r2.RoutingTable.Add(r3.routerId, new Router.RoutingInfo(r3));
            r2.RoutingTable.Add(r4.routerId, new Router.RoutingInfo(r4));
            r2.RoutingTable.Add(r5.routerId, new Router.RoutingInfo(r5));
            r2.RoutingTable.Add(r6.routerId, new Router.RoutingInfo(r6));

            
            r3.Connections.Add(new Router.Connection(r2, 1));
            r3.Connections.Add(new Router.Connection(r4, 1));
            r3.Connections.Add(new Router.Connection(r6, 1));
            r3.RoutingTable.Add(r0.routerId, new Router.RoutingInfo(r0));
            r3.RoutingTable.Add(r1.routerId, new Router.RoutingInfo(r1));
            r3.RoutingTable.Add(r2.routerId, new Router.RoutingInfo(r2));
            r3.RoutingTable.Add(r3.routerId, new Router.RoutingInfo(r3));
            r3.RoutingTable.Add(r4.routerId, new Router.RoutingInfo(r4));
            r3.RoutingTable.Add(r5.routerId, new Router.RoutingInfo(r5));
            r3.RoutingTable.Add(r6.routerId, new Router.RoutingInfo(r6));

            r4.Connections.Add(new Router.Connection(r1, 1));
            r4.Connections.Add(new Router.Connection(r3, 1));
            r4.Connections.Add(new Router.Connection(r5, 1));
            r4.RoutingTable.Add(r0.routerId, new Router.RoutingInfo(r0));
            r4.RoutingTable.Add(r1.routerId, new Router.RoutingInfo(r1));
            r4.RoutingTable.Add(r2.routerId, new Router.RoutingInfo(r2));
            r4.RoutingTable.Add(r3.routerId, new Router.RoutingInfo(r3));
            r4.RoutingTable.Add(r4.routerId, new Router.RoutingInfo(r4));
            r4.RoutingTable.Add(r5.routerId, new Router.RoutingInfo(r5));
            r4.RoutingTable.Add(r6.routerId, new Router.RoutingInfo(r6));

            r5.Connections.Add(new Router.Connection(r2, 1));
            r5.Connections.Add(new Router.Connection(r4, 1));
            r5.Connections.Add(new Router.Connection(r6, 1));
            r5.RoutingTable.Add(r0.routerId, new Router.RoutingInfo(r0));
            r5.RoutingTable.Add(r1.routerId, new Router.RoutingInfo(r1));
            r5.RoutingTable.Add(r2.routerId, new Router.RoutingInfo(r2));
            r5.RoutingTable.Add(r3.routerId, new Router.RoutingInfo(r3));
            r5.RoutingTable.Add(r4.routerId, new Router.RoutingInfo(r4));
            r5.RoutingTable.Add(r5.routerId, new Router.RoutingInfo(r5));
            r5.RoutingTable.Add(r6.routerId, new Router.RoutingInfo(r6));

            r6.Connections.Add(new Router.Connection(r3, 1));
            r6.Connections.Add(new Router.Connection(r5, 1));
            r6.RoutingTable.Add(r0.routerId, new Router.RoutingInfo(r0));
            r6.RoutingTable.Add(r1.routerId, new Router.RoutingInfo(r1));
            r6.RoutingTable.Add(r2.routerId, new Router.RoutingInfo(r2));
            r6.RoutingTable.Add(r3.routerId, new Router.RoutingInfo(r3));
            r6.RoutingTable.Add(r4.routerId, new Router.RoutingInfo(r4));
            r6.RoutingTable.Add(r5.routerId, new Router.RoutingInfo(r5));
            r6.RoutingTable.Add(r6.routerId, new Router.RoutingInfo(r6));

            r0.ConstructRoutingTable();
            r1.ConstructRoutingTable();
            r2.ConstructRoutingTable();
            r3.ConstructRoutingTable();
            r4.ConstructRoutingTable();
            r5.ConstructRoutingTable();
            r6.ConstructRoutingTable();

            r1.started = false;

            var m = int.MaxValue;

            Console.WriteLine(m + 1);

            r0.OriginatePackage();
            r1.OriginatePackage();
            r2.OriginatePackage();
            r3.OriginatePackage();
            r4.OriginatePackage();
            r5.OriginatePackage();
            r6.OriginatePackage();
            r0.OriginatePackage();
            r1.OriginatePackage();
            r2.OriginatePackage();
            r3.OriginatePackage();
            r4.OriginatePackage();
            r5.OriginatePackage();
            r6.OriginatePackage();
            r0.OriginatePackage();
            r1.OriginatePackage();
            r2.OriginatePackage();
            r3.OriginatePackage();
            r4.OriginatePackage();
            r5.OriginatePackage();
            r6.OriginatePackage();
            r0.OriginatePackage();
            r1.OriginatePackage();
            r2.OriginatePackage();
            r3.OriginatePackage();
            r4.OriginatePackage();
            r5.OriginatePackage();
            r6.OriginatePackage();
            r0.OriginatePackage();
            r1.OriginatePackage();
            r2.OriginatePackage();
            r3.OriginatePackage();
            r4.OriginatePackage();
            r5.OriginatePackage();
            r6.OriginatePackage();

            r1.started = true;

            r0.printRoutingTable();

            r0.OriginatePackage();
            r1.OriginatePackage();
            r2.OriginatePackage();
            r3.OriginatePackage();
            r4.OriginatePackage();
            r5.OriginatePackage();
            r6.OriginatePackage();
            r0.OriginatePackage();
            r1.OriginatePackage();
            r2.OriginatePackage();
            r3.OriginatePackage();
            r4.OriginatePackage();
            r5.OriginatePackage();
            r6.OriginatePackage();
            r0.OriginatePackage();
            r1.OriginatePackage();
            r2.OriginatePackage();
            r3.OriginatePackage();
            r4.OriginatePackage();
            r5.OriginatePackage();
            r6.OriginatePackage();
            r0.OriginatePackage();
            r1.OriginatePackage();
            r2.OriginatePackage();
            r3.OriginatePackage();
            r4.OriginatePackage();
            r5.OriginatePackage();
            r6.OriginatePackage();
            r0.OriginatePackage();
            r1.OriginatePackage();
            r2.OriginatePackage();
            r3.OriginatePackage();
            r4.OriginatePackage();
            r5.OriginatePackage();
            r6.OriginatePackage();

            r0.printRoutingTable();

            Console.WriteLine("goodbye");
        }
    }
}
