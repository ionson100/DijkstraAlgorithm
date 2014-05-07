using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using DijkstraAlgorithm;
using NUnit.Framework.Constraints;

namespace graph
{
    class Program
    {
        static void Main(string[] args)
        {
            var nodes = (Graph)GetNodes(args);
            for (int i = 100; i < 2000000; i++)
            {
                nodes.Add(new Node { Id = i, PathNodeIdList = new List<int>(), LinkList = new List<Link>() });
            }
            while (true)
            {

                Console.WriteLine("{0}Select start node ?", Environment.NewLine + Environment.NewLine);
                int startN = SelectNumber(nodes);
                Console.WriteLine("Select finish node ?");
                int finishN = SelectNumber(nodes);

                Console.WriteLine("Select crash nodes (gap) ?");
                int[] crashN = SelectCrashNumbers();

                if (nodes != null)
                {
                    var resnode = nodes.GetMinPath(startN, finishN, crashN);
                    if (resnode == null)
                        Console.Write("Errors:{0}{1}", Environment.NewLine, string.Join("Errorrs:" + Environment.NewLine, nodes.LastErrorList));
                    else
                        Console.WriteLine(resnode.ToString());
                }
                Console.WriteLine("{0}_____________________________________________", Environment.NewLine);
                Console.ReadKey();
            }







        }

        static IEnumerable<Node> GetNodes(params string[] arg)
        {

            var serializer = new XmlSerializer(typeof(Graph));

            if (arg.Length == 0)
            {
                return (Graph)serializer.Deserialize(new StringReader(Properties.Resources.test1));
            }
            return (Graph)serializer.Deserialize(new XmlTextReader(arg[0]));
        }

        public static IEnumerable<Node> GetNodes(string str)
        {

            var serializer = new XmlSerializer(typeof(Graph));

            return (Graph)serializer.Deserialize(new StringReader(str));

        }

        static int SelectNumber(IEnumerable<Node> nodes)
        {
            int number;


            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out number))
                {
                    if (nodes.FirstOrDefault(a => a.Id == number) != null)
                    {
                        break;
                    }
                    Console.WriteLine("No such node id=" + number);

                }
                else
                {
                    Console.WriteLine("This not number");
                }

            }
            return number;
        }

        static int[] SelectCrashNumbers()
        {
            var number = new List<int>();
            while (true)
            {
                var readLine = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(readLine))
                    return number.ToArray();
                var dd = readLine.Split(" ".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                bool isVa = true;
                foreach (var s in dd)
                {
                    int i;
                    if (int.TryParse(s, out i))
                    {
                        number.Add(i);
                    }
                    else
                    {
                        number.Clear();
                        Console.WriteLine("This ({0}) not number", s);
                        Console.WriteLine("Select crash node  on again (gap)?");
                        isVa = false;
                        break;
                    }
                }
                if (isVa) { break; }
            }
            return number.ToArray();
        }
    }



}
