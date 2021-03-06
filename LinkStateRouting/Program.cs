﻿using System;
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

            Console.WriteLine("Welcome to the Link State Routing Simulation. The simulated routers have been ");
            Console.WriteLine("generated by the infile.dat, and have been initialized. However, each router's");
            Console.WriteLine("routing table has not been initialized yet. Continue once if you would like to");
            Console.WriteLine("generate them.");
            Console.WriteLine("\nBelow are some options:");
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
                int id;
                switch (inputSplit[0])
                {
                    case "C":
                        inputContinue(routers);
                        break;
                    case "P":
                        if (inputSplit.Length != 2 || !int.TryParse(inputSplit[1], out id))
                        {
                            goto default;
                        }
                        inputPrint(routers, id);
                        break;
                    case "S":
                        if (inputSplit.Length != 2 || !int.TryParse(inputSplit[1], out id))
                        {
                            goto default;
                        }
                        inputStop(routers, id);
                        break;
                    case "T":
                        if (inputSplit.Length != 2 || !int.TryParse(inputSplit[1], out id))
                        {
                            goto default;
                        }
                        inputStart(routers, id);
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
