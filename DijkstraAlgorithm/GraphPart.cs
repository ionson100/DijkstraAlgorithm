using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DijkstraAlgorithm
{
    public partial class Graph
    {
        List<string> _erMsgList = new List<string>();
        partial void InternalGetMinPath(int startNode, int finishNode, params int[] crashNode)
        {
            ForEach(a =>
            {
                a.IsCrash = false;
                a.Role = Role.NotDetermined;
                a.Sorted = 0;
                a.IsWisit = false;
                a.Distance = 0;
                a.PathNodeIdList.Clear();
                a.WisitList.Clear();
            });
            LastErrorList.Clear();
            var dr = new Drunkards(this, startNode, finishNode, crashNode);
            _erMsgList = dr.GetLastErrorList;
            dr.MinPath();
        }

        private class Drunkards
        {
            readonly List<String> _erMesList = new List<string>();
            class ValidHelperClass
            {
                public int Id { get; set; }
                public HashSet<int> Linklist = new HashSet<int>();
            }

         
            private readonly List<Node> _nodes;

            public List<string> GetLastErrorList
            {
                get
                {
                    return _erMesList;
                }
            }

            public Drunkards(IEnumerable<Node> nodes, int startNode, int finishNode, params int[] crachNode)
            {
                _nodes = nodes.ToList();
                ValidateNodes(startNode, finishNode, crachNode);
                if (!_erMesList.Any())
                {
                    _nodes.ForEach(a =>
                    {
                        if (a.Id == startNode)
                        {
                            a.Role = Role.Start;
                        }
                        if (a.Id == finishNode)
                        {
                            a.Role = Role.Finish;
                        }
                        if (crachNode != null && crachNode.Contains(a.Id))
                        {
                            a.IsCrash = true;
                        }
                    });
                }
            }

            internal void MinPath()
            {
                if (_erMesList.Any()) return;

                _nodes.ForEach(a =>
                {
                    foreach (var l in a.LinkList.OrderBy(g => g.Weight))
                    {
                        a.WisitList.Add(_nodes.Find(f => f.Id == l.Ref));
                    }

                });

                var startNode = _nodes.Find(a => a.Role == Role.Start);
                startNode.IsWisit = true;
                startNode.PathNodeIdList.Add(startNode.Id);
                var sorded = 0;
                var run = true;
             var iterationList=new List<Node> {startNode};
               
                while (run)
                {
                    var rr = iterationList.Where(a => a.IsWisit).OrderBy(s => s.Sorted);// _nodes.Where(a => a.IsWisit).OrderBy(s => s.Sorted);
                    if (!rr.Any()) break;
                    foreach (var sn in rr)//visit node
                    {
                        if (sn.Role == Role.Finish)//if finish node
                        {
                            run = false;
                            break;
                        }
                        if (sn.IsCrash) continue; //if node crash
                        foreach (var node in sn.WisitList)// visit all link node 
                        {
                            if (node.IsCrash) continue;
                            var delnode = node.WisitList.FirstOrDefault(a => a.Id == sn.Id);
                            if (delnode != null)//if traffic is not two-sided
                                node.WisitList.Remove(delnode);
                            var st = sn.Distance + sn.LinkList.Find(a => a.Ref == node.Id).Weight;
                            if (node.Distance > st || node.Distance == 0)// if distance less than current
                            {
                                node.Distance = st;
                                node.PathNodeIdList.Clear();
                                node.PathNodeIdList.AddRange(sn.PathNodeIdList);
                                node.PathNodeIdList.Add(node.Id);
                            }
                            
                            node.Sorted = ++sorded;//private operation
                            node.IsWisit = true;
                            iterationList.Add(node);
                        }
                       // iterationList.Find(s=>s.Id=--sn.Id).IsWisit=fa;
                        sn.IsWisit = false;
                    }
                }
            }

            private void ValidateNodes(int startNode, int finishNode, params int[] crachNode)
            {

                if (!_nodes.Any())
                {
                    GetLastErrorList.Add("Empty collection of nodes");
                    return;
                }

                var s = _nodes.Select(a => a.Id).ToArray();
                if (!s.Contains(startNode))
                {
                    GetLastErrorList.Add("Start node does not exist");
                    return;
                }

                if (!s.Contains(finishNode))
                {
                    GetLastErrorList.Add("Finish node does not exist");
                    return;
                }

                if (startNode == finishNode)
                {
                    GetLastErrorList.Add("Start node can not be  node finish.");
                    return;
                }
                if (crachNode != null && crachNode.Contains(startNode))
                {
                    GetLastErrorList.Add("Start node can not be crash  node.");
                    return;
                }

                if (crachNode != null && crachNode.Contains(finishNode))
                {
                    GetLastErrorList.Add("Finish node can not be crash  node.");
                    return;
                }
                var idlist2 = new HashSet<ValidHelperClass>();
                var idlist=new HashSet<int>();
                _nodes.ForEach(a =>
                               {
                                   if(!idlist.Contains(a.Id))
                                   idlist.Add(a.Id);
                                   else
                                   {
                                       GetLastErrorList.Add("double node: " + a.Id);
                                   }
                                   var linkNodelist = new HashSet<int>();
                                   a.LinkList.ToList().ForEach(d =>
                                   {
                                       if (d.Ref == a.Id)
                                       {
                                           GetLastErrorList.Add("node: " + a.Id +
                                                             " link to herself: " + d.Ref);
                                       }
                                       if (linkNodelist.Contains(d.Ref))
                                       {
                                           GetLastErrorList.Add("node: " + a.Id +
                                                             " double link:" + d.Ref);
                                       }
                                       else
                                       {
                                           linkNodelist.Add(d.Ref);
                                       }
                                   });
                                   idlist2.Add(new ValidHelperClass { Id = a.Id, Linklist = linkNodelist });
                });

                idlist2.ToList().ForEach(d => d.Linklist.ToList().ForEach(a =>
                {
                    if (!idlist.Contains(a))
                    {

                        GetLastErrorList.Add("node:" + d.Id +
                                         " zero link:" + a);
                    }
                }));

                if (GetLastErrorList.Any() || crachNode == null) return;
                foreach (var cr in from cr in crachNode let n = _nodes.Select(a => a.Id).ToArray() where !n.Contains(cr) select cr)
                {
                    GetLastErrorList.Add("Crash node does not exist for id node=" + cr);
                }
            }
        }


    }
}
