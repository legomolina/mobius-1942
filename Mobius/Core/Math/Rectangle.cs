﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Math
{
    public struct Rectangle
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public Rectangle() : this(0, 0, 0, 0)
        {

        }

        public Rectangle(float x, float y, float w, float h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }

        public bool Contains(Rectangle rect) 
        {
            return (X <= rect.X) &&
                ((rect.X + rect.Width) <= (X + Width)) &&
                (Y <= rect.Y) &&
                ((rect.Y + rect.Height) <= (Y + Height));
        }

        public bool Intersects(Rectangle rect)
        {
            return (rect.X < X + Width) &&
                (X < (rect.X + rect.Width)) &&
                (rect.Y < Y + Height) &&
                (Y < rect.Y + rect.Height);
        }

        public override string ToString()
        {
            return $"x: {X}, y: {Y}, w: {Width}, h: {Height}";
        }
    }
}
