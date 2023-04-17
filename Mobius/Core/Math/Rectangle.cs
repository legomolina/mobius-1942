using static SDL2.SDL;

using System;
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

        public bool Contains(Point point)
        {
            return point.X >= X && point.X <= X + Width && point.Y >= Y && point.Y <= Y + Height;
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

        internal SDL_Rect ToSDLRect()
        {
            return new SDL_Rect()
            {
                x = (int)X,
                y = (int)Y,
                w = (int)Width,
                h = (int)Height,
            };
        }
    }
}
