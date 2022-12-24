using Engine.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Pathfinding
{
    public partial class Waypoint
    {
        public Point Position { get; set; }

        public Waypoint(Point position)
        {
            Position = position;
        }

        public Vector2 GetDirection(Waypoint waypoint)
        {
            return Vector2.FromPoints(Position, waypoint.Position);
        }

        public float GetDistance(Waypoint waypoint)
        {
            return GetDirection(waypoint).Magnitude();
        }
    }
}
