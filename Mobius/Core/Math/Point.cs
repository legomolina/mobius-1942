using static SDL2.SDL;

namespace Engine.Core.Math
{
    public struct Point : ICloneable<Point>
    {
        public float X { get; set; }
        public float Y { get; set; }
        public static Point Zero { get => new(0, 0); }

        public Point() : this(0, 0) { }

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2 ToVector()
        {
            return new Vector2(X, Y);
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(b.X - a.X, b.Y - b.X);
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Point a, Point b)
        {
            return a.X != b.X || a.Y != b.Y;
        }

        public Point Clone()
        {
            return new Point(X, Y);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException();
            }

            return X == ((Point)obj).X && Y == ((Point)obj).Y;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        internal SDL_Point ToSDLPoint()
        {
            return new SDL_Point()
            {
                x = (int)X,
                y = (int)Y,
            };
        }
    }
}
