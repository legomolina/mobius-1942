using Engine.Core.Math;
using System;

namespace Engine.Core.Automation.Tracking
{
    public class Waypoint
    {
        public Point Position { get; set; }

        public Waypoint()
        {
            Position = Point.Zero;
        }

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