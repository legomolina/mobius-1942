using Engine.Components;

namespace Engine.Core.Math
{
    [Flags]
    public enum AreaQuadrants
    {
        None = 0,
        TopLeft = 1,
        TopRight = 2,
        BottomLeft = 4,
        BottomRight = 8,
    }

    public class Area
    {
        private readonly Rectangle area;

        public float X => area.X;
        public float Y => area.Y;
        public float Width => area.Width;
        public float Height => area.Height;
        public float Top => area.Y;
        public float Right => area.X + area.Width;
        public float Bottom => area.Y + area.Height;
        public float Left => area.X;
        public Point TopLeft => new(area.X, area.Y);
        public Point TopRight => new(area.X + area.Width, area.Y);
        public Point BottomLeft => new(area.X, area.Y + area.Height);
        public Point BottomRight => new(area.X + area.Width, area.Y + area.Y + area.Height);

        public Area(GameComponent component, int padding)
        {
            area = new Rectangle(
                component.Position.X - padding,
                component.Position.Y - padding,
                component.Width + padding * 2,
                component.Height + padding * 2
            );
        }

        public Area(Rectangle area)
        {
            this.area = area;
        }

        public Area(int x, int y, int width, int height)
        {
            area = new Rectangle(x, y, width, height);
        }

        public bool IsPointInside(Point point)
        {
            bool x = point.X >= Left && point.X <= Right;
            bool y = point.Y >= Top && point.Y <= Bottom;

            return x && y;
        }

        public Point GetRandomPoint()
        {
            return GetRandomPoint(AreaQuadrants.TopLeft | AreaQuadrants.TopRight | AreaQuadrants.BottomLeft | AreaQuadrants.BottomRight);
        }

        public Point GetRandomPoint(AreaQuadrants quadrants)
        {
            if (quadrants == AreaQuadrants.None)
            {
                return Point.Zero;
            }

            AreaQuadrants quadrant = GetRandomQuadrant(quadrants);
            Rectangle quadrantBounds = GetBoundsOfQuadrant(quadrant);

            return GetRandomPointFromRectangle(quadrantBounds);
        }

        public Rectangle ToRectangle()
        {
            return area;
        }

        private AreaQuadrants GetRandomQuadrant(AreaQuadrants quadrants)
        {
            AreaQuadrants[] selectedQuadrants = Enum.GetValues(typeof(AreaQuadrants))
                .Cast<AreaQuadrants>()
                .Where(q => (quadrants & q) == q)
                .ToArray();

            return selectedQuadrants[new Random().Next(selectedQuadrants.Length)];
        }

        private Rectangle GetBoundsOfQuadrant(AreaQuadrants quadrant)
        {
            return quadrant switch
            {
                AreaQuadrants.TopLeft => new Rectangle(area.X, area.Y, area.Width / 2, area.Height / 2),
                AreaQuadrants.TopRight => new Rectangle(area.X + area.Width / 2, area.Y, area.Width / 2, area.Height / 2),
                AreaQuadrants.BottomLeft => new Rectangle(area.X, area.Y + area.Height / 2, area.Width / 2, area.Height / 2),
                AreaQuadrants.BottomRight => new Rectangle(area.X + area.Width / 2, area.Y + area.Height / 2, area.Width / 2, area.Height / 2),
                _ => area,
            };
        }

        private Point GetRandomPointFromRectangle(Rectangle rectangle)
        {
            Random random = new Random();
            int x = random.Next((int)rectangle.X, (int)(rectangle.X + rectangle.Width));
            int y = random.Next((int)rectangle.Y, (int)(rectangle.Y + rectangle.Height));

            return new Point(x, y);
        }
    }
}
