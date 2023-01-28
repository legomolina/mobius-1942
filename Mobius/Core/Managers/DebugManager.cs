using static SDL2.SDL;
using Engine.Core.Math;
using Engine.Core.Automation.Tracking;

namespace Engine.Core.Managers
{
    public static class DebugManager
    {
        public static void DrawLine(Point point1, Point point2)
        {
            DrawLine(point1, point2, Color.Red);
        }

        public static void DrawLine(Point point1, Point point2, Color color)
        {
            SDL_Point p1 = new SDL_Point()
            {
                x = (int)point1.X,
                y = (int)point1.Y
            };
            SDL_Point p2 = new SDL_Point()
            {
                x = (int)point2.X,
                y = (int)point2.Y,
            };

            GraphicsManager.Instance.DrawLine(p1, p2, color.R, color.G, color.B, color.A);
        }

        public static void DrawRectangle(Rectangle bounds)
        {
            DrawRectangle(bounds, Color.Red);
        }

        public static void DrawRectangle(Rectangle bounds, Color color)
        {
            SDL_Rect rect = new SDL_Rect()
            {
                x = (int)bounds.X,
                y = (int)bounds.Y,
                w = (int)bounds.Width,
                h = (int)bounds.Height
            };

            GraphicsManager.Instance.DrawRectangle(rect, color.R, color.G, color.B, color.A);
        }

        public static void DrawTrack(Track track)
        {
            for (int i = 0; i < track.Waypoints.Count - 1; i++)
            {
                Waypoint current = track.Waypoints[i];
                Waypoint next = track.Waypoints[i + 1];

#if DEBUG
                DrawLine(current.Position, next.Position);
#endif
            }
        }
    }
}
