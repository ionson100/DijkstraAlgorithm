using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DijkstraAlgorithm
{
    [XmlRoot("graph")]
    public partial class Graph : List<Node>
    {
        partial void InternalGetMinPath(int startNode, int finishNode, params int[] crashNode);
        public List<string> LastErrorList { get { return _erMsgList; } }
        /// <summary>
        /// Function of the minimum way
        /// </summary>
        /// <param name="startNode">id start node</param>
        /// <param name="finishNode">id finish node</param>
        /// <param name="crashNodes">nodes do not participate</param>
        /// <returns></returns>
        public Node GetMinPath(int startNode, int finishNode, params int[] crashNodes)
        {
            InternalGetMinPath(startNode, finishNode, crashNodes);
            var nodefinish= this.FirstOrDefault(a => a.Id == finishNode);
            if (nodefinish != null && (nodefinish.Distance == 0 && !nodefinish.PathNodeIdList.Any())) return null;
            return nodefinish;
        }
    }

    [XmlType("node")]
    public class Node
    {
        internal Role Role { get; set; }
        /// <summary>
        /// Node do not participate
        /// </summary>
        public bool IsCrash { get; set; }
        internal int Sorted { get; set; }
        internal bool IsWisit { get; set; }
        internal List<Node> WisitList = new List<Node>();
        /// <summary>
        /// Min path
        /// </summary>
        public List<int> PathNodeIdList = new List<int>();

        /// <summary>
        /// Min Distance
        /// </summary>
        public uint Distance { get; set; }
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlElement(ElementName = "link")]
        public List<Link> LinkList { get; set; }

        public override string ToString()
        {
            if (!PathNodeIdList.Any() && Distance == 0)
            {
                return "has no the way";
            }
            return string.Format("Total weight: {0}   Path: {1}", Distance, string.Join("-", PathNodeIdList));
        }
    }
    [XmlType("link")]
    public class Link
    {
        [XmlAttribute("ref")]
        public int Ref { get; set; }
        [XmlAttribute("weight")]
        public uint Weight { get; set; }
    }
    internal enum Role
    {
        NotDetermined,
        Start,
        Finish
    }
}
