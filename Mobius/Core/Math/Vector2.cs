namespace Engine.Core.Math
{
    public struct Vector2 : ICloneable
    {
        public static Vector2 Zero => new(0, 0);

        public float X { get; set; }
        public float Y { get; set; }

        public Vector2(float x = 0, float y = 0)
        {
            X = x;
            Y = y;
        }

        public static Vector2 FromPoints(Point a, Point b)
        {
            return new Vector2(b.X - a.X, b.Y - a.Y);
        }

        public float MagnitudeSqrt()
        {
            return X * X + Y * Y;
        }

        public float Magnitude()
        {
            return (float)System.Math.Sqrt(MagnitudeSqrt());
        }

        public Vector2 Normalized()
        {
            float magnitude = Magnitude();

            if (magnitude > 0)
            {
                return new Vector2(X / magnitude, Y / magnitude);
            }

            return Zero;
        }

        public Vector2 Rotate(float angle)
        {
            float radAngle = (float)(angle * System.Math.PI / 180.0f);
            float x = (float)(X * System.Math.Cos(radAngle) - Y * System.Math.Sin(radAngle));
            float y = (float)(X * System.Math.Sin(radAngle) + Y * System.Math.Cos(radAngle));

            return new Vector2(x, y);
        }

        public float AngleBetween(Vector2 vec)
        {
            double sin = X * vec.Y - vec.X * Y;
            double cos = X * vec.X - Y * vec.Y;

            return (float)(System.Math.Atan2(sin, cos) * (180 / System.Math.PI));
        }

        public Point ToPoint()
        {
            return new Point(X, Y);
        }

        public Vector2 Clone()
        {
            return new Vector2(X, Y);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public override string ToString()
        {
            return $"{X:0.00},{Y:0.00}";
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator *(Vector2 a, float factor)
        {
            return new Vector2(a.X * factor, a.Y * factor);
        }

        public static Vector2 operator /(Vector2 a, float factor)
        {
            return new Vector2(a.X / factor, a.Y / factor);
        }

        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return !a.Equals(b);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException();
            }

            return X == ((Vector2)obj).X && Y == ((Vector2)obj).Y;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
