using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Math
{
    public struct Point
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
    }
}
