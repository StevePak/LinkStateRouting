using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinkStateRouting
{
    class Program
    {
        static void Main(string[] args)
        {

            string filename = "infile.dat";

            Dictionary<int, Router> routers = getRoutersFromFile(filename);



            //Router r0 = new Router(0, "155.246.80");
            //Router r1 = new Router(1, "155.246.81");
            //Router r2 = new Router(2, "155.246.82");
            //Router r3 = new Router(3, "155.246.83");
            //Router r4 = new Router(4, "155.246.84");
            //Router r5 = new Router(5, "155.246.85");
            //Router r6 = new Router(6, "155.246.86");

            Console.WriteLine("Welcome to the Link State Routing Simulation. Below are some options:");
            Console.WriteLine("C - Continue. Originates a package from each router in input file.");
            Console.WriteLine("P {ID} - Print. Prints the routing table of router with id == {ID}.");
            Console.WriteLine("S {ID} - Shut Down Router. Stops the router with id == {ID}.");
            Console.WriteLine("T {ID} - Start Router. Starts the router with id == {ID}.");
            Console.WriteLine("Q - Quit. This will exit the program.");

            bool quit = false;

            while (!quit)
            {
                Console.WriteLine("\nWhat would you like to do?");
                Console.Write(">>> ");
                string input = Console.ReadLine();
                string[] inputSplit = input.Split(' ');
                switch (inputSplit[0])
                {
                    case "C":
                        inputContinue(routers);
                        break;
                    case "P":
                        if (inputSplit.Length != 2)
                        {
                            goto default;
                        }
                        inputPrint(routers, int.Parse(inputSplit[1]));
                        break;
                    case "S":
                        if (inputSplit.Length != 2)
                        {
                            goto default;
                        }
                        inputStop(routers, int.Parse(inputSplit[1]));
                        break;
                    case "T":
                        if (inputSplit.Length != 2)
                        {
                            goto default;
                        }
                        inputStart(routers, int.Parse(inputSplit[1]));
                        break;
                    case "Q":
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. List of valid options below:");
                        Console.WriteLine("C - Continue. Originates a package from each router in input file.");
                        Console.WriteLine("P {ID} - Print. Prints the routing table of router with id == {ID}.");
                        Console.WriteLine("S {ID} - Shut Down Router. Stops the router with id == {ID}.");
                        Console.WriteLine("T {ID} - Start Router. Starts the router with id == {ID}.");
                        Console.WriteLine("Q - Quit. This will exit the program.");
                        break;
                }
            }

            //r0.Connections.Add(new Router.Connection(r1, 1));
            //r0.Connections.Add(new Router.Connection(r2, 1));
            //r0.RoutingTable.Add(r0.routerId, new Router.RoutingInfo(r0));
            //r0.RoutingTable.Add(r1.routerId, new Router.RoutingInfo(r1));
            //r0.RoutingTable.Add(r2.routerId, new Router.RoutingInfo(r2));
            //r0.RoutingTable.Add(r3.routerId, new Router.RoutingInfo(r3));
            //r0.RoutingTable.Add(r4.routerId, new Router.RoutingInfo(r4));
            //r0.RoutingTable.Add(r5.routerId, new Router.RoutingInfo(r5));
            //r0.RoutingTable.Add(r6.routerId, new Router.RoutingInfo(r6));

            //r1.Connections.Add(new Router.Connection(r0, 1));
            //r1.Connections.Add(new Router.Connection(r2, 1));
            //r1.Connections.Add(new Router.Connection(r4, 1));
            //r1.RoutingTable.Add(r0.routerId, new Router.RoutingInfo(r0));
            //r1.RoutingTable.Add(r1.routerId, new Router.RoutingInfo(r1));
            //r1.RoutingTable.Add(r2.routerId, new Router.RoutingInfo(r2));
            //r1.RoutingTable.Add(r3.routerId, new Router.RoutingInfo(r3));
            //r1.RoutingTable.Add(r4.routerId, new Router.RoutingInfo(r4));
            //r1.RoutingTable.Add(r5.routerId, new Router.RoutingInfo(r5));
            //r1.RoutingTable.Add(r6.routerId, new Router.RoutingInfo(r6));

            //r2.Connections.Add(new Router.Connection(r0, 1));
            //r2.Connections.Add(new Router.Connection(r1, 1));
            //r2.Connections.Add(new Router.Connection(r3, 1));
            //r2.Connections.Add(new Router.Connection(r5, 1));
            //r2.RoutingTable.Add(r0.routerId, new Router.RoutingInfo(r0));
            //r2.RoutingTable.Add(r1.routerId, new Router.RoutingInfo(r1));
            //r2.RoutingTable.Add(r2.routerId, new Router.RoutingInfo(r2));
            //r2.RoutingTable.Add(r3.routerId, new Router.RoutingInfo(r3));
            //r2.RoutingTable.Add(r4.routerId, new Router.RoutingInfo(r4));
            //r2.RoutingTable.Add(r5.routerId, new Router.RoutingInfo(r5));
            //r2.RoutingTable.Add(r6.routerId, new Router.RoutingInfo(r6));

            
            //r3.Connections.Add(new Router.Connection(r2, 1));
            //r3.Connections.Add(new Router.Connection(r4, 1));
            //r3.Connections.Add(new Router.Connection(r6, 1));
            //r3.RoutingTable.Add(r0.routerId, new Router.RoutingInfo(r0));
            //r3.RoutingTable.Add(r1.routerId, new Router.RoutingInfo(r1));
            //r3.RoutingTable.Add(r2.routerId, new Router.RoutingInfo(r2));
            //r3.RoutingTable.Add(r3.routerId, new Router.RoutingInfo(r3));
            //r3.RoutingTable.Add(r4.routerId, new Router.RoutingInfo(r4));
            //r3.RoutingTable.Add(r5.routerId, new Router.RoutingInfo(r5));
            //r3.RoutingTable.Add(r6.routerId, new Router.RoutingInfo(r6));

            //r4.Connections.Add(new Router.Connection(r1, 1));
            //r4.Connections.Add(new Router.Connection(r3, 1));
            //r4.Connections.Add(new Router.Connection(r5, 1));
            //r4.RoutingTable.Add(r0.routerId, new Router.RoutingInfo(r0));
            //r4.RoutingTable.Add(r1.routerId, new Router.RoutingInfo(r1));
            //r4.RoutingTable.Add(r2.routerId, new Router.RoutingInfo(r2));
            //r4.RoutingTable.Add(r3.routerId, new Router.RoutingInfo(r3));
            //r4.RoutingTable.Add(r4.routerId, new Router.RoutingInfo(r4));
            //r4.RoutingTable.Add(r5.routerId, new Router.RoutingInfo(r5));
            //r4.RoutingTable.Add(r6.routerId, new Router.RoutingInfo(r6));

            //r5.Connections.Add(new Router.Connection(r2, 1));
            //r5.Connections.Add(new Router.Connection(r4, 1));
            //r5.Connections.Add(new Router.Connection(r6, 1));
            //r5.RoutingTable.Add(r0.routerId, new Router.RoutingInfo(r0));
            //r5.RoutingTable.Add(r1.routerId, new Router.RoutingInfo(r1));
            //r5.RoutingTable.Add(r2.routerId, new Router.RoutingInfo(r2));
            //r5.RoutingTable.Add(r3.routerId, new Router.RoutingInfo(r3));
            //r5.RoutingTable.Add(r4.routerId, new Router.RoutingInfo(r4));
            //r5.RoutingTable.Add(r5.routerId, new Router.RoutingInfo(r5));
            //r5.RoutingTable.Add(r6.routerId, new Router.RoutingInfo(r6));

            //r6.Connections.Add(new Router.Connection(r3, 1));
            //r6.Connections.Add(new Router.Connection(r5, 1));
            //r6.RoutingTable.Add(r0.routerId, new Router.RoutingInfo(r0));
            //r6.RoutingTable.Add(r1.routerId, new Router.RoutingInfo(r1));
            //r6.RoutingTable.Add(r2.routerId, new Router.RoutingInfo(r2));
            //r6.RoutingTable.Add(r3.routerId, new Router.RoutingInfo(r3));
            //r6.RoutingTable.Add(r4.routerId, new Router.RoutingInfo(r4));
            //r6.RoutingTable.Add(r5.routerId, new Router.RoutingInfo(r5));
            //r6.RoutingTable.Add(r6.routerId, new Router.RoutingInfo(r6));

            //r0.ConstructRoutingTable();
            //r1.ConstructRoutingTable();
            //r2.ConstructRoutingTable();
            //r3.ConstructRoutingTable();
            //r4.ConstructRoutingTable();
            //r5.ConstructRoutingTable();
            //r6.ConstructRoutingTable();

            //r1.started = false;

            //var m = int.MaxValue;

            //Console.WriteLine(m + 1);

            //r0.OriginatePackage();
            //r1.OriginatePackage();
            //r2.OriginatePackage();
            //r3.OriginatePackage();
            //r4.OriginatePackage();
            //r5.OriginatePackage();
            //r6.OriginatePackage();
            //r0.OriginatePackage();
            //r1.OriginatePackage();
            //r2.OriginatePackage();
            //r3.OriginatePackage();
            //r4.OriginatePackage();
            //r5.OriginatePackage();
            //r6.OriginatePackage();
            //r0.OriginatePackage();
            //r1.OriginatePackage();
            //r2.OriginatePackage();
            //r3.OriginatePackage();
            //r4.OriginatePackage();
            //r5.OriginatePackage();
            //r6.OriginatePackage();
            //r0.OriginatePackage();
            //r1.OriginatePackage();
            //r2.OriginatePackage();
            //r3.OriginatePackage();
            //r4.OriginatePackage();
            //r5.OriginatePackage();
            //r6.OriginatePackage();
            //r0.OriginatePackage();
            //r1.OriginatePackage();
            //r2.OriginatePackage();
            //r3.OriginatePackage();
            //r4.OriginatePackage();
            //r5.OriginatePackage();
            //r6.OriginatePackage();

            //r1.started = true;

            //r0.printRoutingTable();

            //r0.OriginatePackage();
            //r1.OriginatePackage();
            //r2.OriginatePackage();
            //r3.OriginatePackage();
            //r4.OriginatePackage();
            //r5.OriginatePackage();
            //r6.OriginatePackage();
            //r0.OriginatePackage();
            //r1.OriginatePackage();
            //r2.OriginatePackage();
            //r3.OriginatePackage();
            //r4.OriginatePackage();
            //r5.OriginatePackage();
            //r6.OriginatePackage();
            //r0.OriginatePackage();
            //r1.OriginatePackage();
            //r2.OriginatePackage();
            //r3.OriginatePackage();
            //r4.OriginatePackage();
            //r5.OriginatePackage();
            //r6.OriginatePackage();
            //r0.OriginatePackage();
            //r1.OriginatePackage();
            //r2.OriginatePackage();
            //r3.OriginatePackage();
            //r4.OriginatePackage();
            //r5.OriginatePackage();
            //r6.OriginatePackage();
            //r0.OriginatePackage();
            //r1.OriginatePackage();
            //r2.OriginatePackage();
            //r3.OriginatePackage();
            //r4.OriginatePackage();
            //r5.OriginatePackage();
            //r6.OriginatePackage();

            //r0.printRoutingTable();

            Console.WriteLine("goodbye");
        }


        public static Dictionary<int, Router> getRoutersFromFile(string filename)
        {
            string[] lines = System.IO.File.ReadAllLines(filename);

            Dictionary<int, Router> routers = new Dictionary<int, Router>();
            

            foreach (string line in lines)
            {
                string lineStripped = Regex.Replace(line, @"\s+", " ");
                if (!char.IsWhiteSpace(lineStripped, 0))
                {
                    string[] lineSplit = lineStripped.Split(' ');

                    routers.Add(int.Parse(lineSplit[0]), new Router(int.Parse(lineSplit[0]), lineSplit[1]));
                }
            }

            int id = -1;
            foreach (string line in lines)
            {
                string lineStripped = Regex.Replace(line, @"\s+", " ");
                if (!char.IsWhiteSpace(lineStripped, 0))
                {
                    string[] lineSplit = lineStripped.Split(' ');
                    id = int.Parse(lineSplit[0]);
                }
                else
                {
                    string[] lineSplit = lineStripped.Split(' ');
                    List<string> lineCleaned = lineSplit.ToList();
                    lineCleaned.RemoveAll(x => String.IsNullOrEmpty(x));
                    routers[id].addConnection(routers[int.Parse(lineCleaned[0])], lineCleaned.Count == 2 ? int.Parse(lineCleaned[1]) : 1);
                }
            }

            foreach (var i in routers.Values)
            {
                foreach (var j in routers.Values)
                {
                    i.RoutingTable.Add(j.routerId, new Router.RoutingInfo(j));
                }
            }

            return routers;
        }

        public static void inputContinue(Dictionary<int, Router> routers)
        {
            foreach (var router in routers.Values)
            {
                router.OriginatePackage();
            }
        }

        public static void inputStop(Dictionary<int, Router> routers, int id)
        {
            if (!isValidRouterId(routers, id))
            {
                Console.WriteLine($"Router {id} does not exist.");
                return;
            }
            if (!routers[id].started)
            {
                Console.WriteLine($"Router {id} has already been stopped.");
            }
            else
            {
                routers[id].started = false;
                Console.WriteLine($"Router {id} has been stopped.");
            }
        }

        public static void inputStart(Dictionary<int, Router> routers, int id)
        {
            if (!isValidRouterId(routers, id))
            {
                Console.WriteLine($"Router {id} does not exist.");
                return;
            }
            if (routers[id].started)
            {
                Console.WriteLine($"Router {id} has already been started.");
            }
            else
            {
                routers[id].started = true;
                Console.WriteLine($"Router {id} has been started.");
            }
        }

        public static void inputPrint(Dictionary<int, Router> routers, int id)
        {
            if(!isValidRouterId(routers, id))
            {
                Console.WriteLine($"Router {id} does not exist.");
                return;
            }
            routers[id].printRoutingTable();
        }

        public static bool isValidRouterId(Dictionary<int, Router> routers, int id)
        {
            Router r;
            return routers.TryGetValue(id, out r);
        }

    }
}
