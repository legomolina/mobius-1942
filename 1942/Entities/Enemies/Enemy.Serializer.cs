using Engine.Core;
using Engine.Serializing;
using org.matheval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace _1942.Entities.Enemies
{
    [XmlRoot("Enemy")]
    public partial class Enemy2
    {
        [XmlElement("Path")]
        public Path Path { get; set; }
    }

    public enum WaypointTypes
    {
        [XmlEnum("point")]
        Point,
        [XmlEnum("arc")]
        Arc
    }

    [Serializable()]
    public class Path
    {
        [XmlArray("Waypoints")]
        [XmlArrayItem("Waypoint", typeof(Waypoint))]
        public Waypoint[] Waypoints { get; set; }
    }

    [Serializable()]
    public class Waypoint
    {
        [XmlAttribute("x")]
        public string x;

        [XmlAttribute("y")]
        public string y;

        [XmlAttribute("type")]
        public WaypointTypes Type { get; set; }

        [XmlIgnore]
        public int X => SerializerHelper.Eval<int>(x.Replace("$", ""));

        [XmlIgnore]
        public int Y => SerializerHelper.Eval<int>(y.Replace("$", ""));

        [XmlAttribute("radius")]
        public int Radius { get; set; }

        [XmlAttribute("minDeg")]
        public int MinDegree { get; set; }

        [XmlAttribute("maxDeg")]
        public int MaxDegree { get; set; }

        [XmlAttribute("precision")]
        public int Precision { get; set; }
    }
}
