using static SDL2.SDL;
using Engine.Core.Math;
using Engine.Core.Automation.Tracking;
using Engine.Core.Debug;

namespace Engine.Core.Managers
{
    public static class DebugManager
    {
        public static DebugDrawable DrawLine(Point point1, Point point2)
        {
            return DrawLine(point1, point2, Color.Red);
        }

        public static DebugDrawable DrawLine(Point point1, Point point2, Color color)
        {
            return new DebugLine(GraphicsManager.Instance, point1, point2, color);
        }

        public static DebugDrawable DrawRectangle(Rectangle bounds)
        {
            return DrawRectangle(bounds, Color.Red);
        }

        public static DebugDrawable DrawRectangle(Rectangle bounds, Color color)
        {
            return new DebugRectangle(GraphicsManager.Instance, bounds, color);
        }

        /*public static DebugDrawable DrawTrack(Track track)
        {
            for (int i = 0; i < track.Waypoints.Count - 1; i++)
            {
                Waypoint current = track.Waypoints[i];
                Waypoint next = track.Waypoints[i + 1];

                DrawLine(current.Position, next.Position);
            }
        }*/

        public static DebugDrawable DrawText(Point position, Color color, string text)
        {
            return new DebugText(GraphicsManager.Instance, color, text, position);
        }
    }
}
