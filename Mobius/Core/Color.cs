using static SDL2.SDL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    public struct Color
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public static Color Red => new(255, 0, 0);
        public static Color Green => new(0, 255, 0);
        public static Color Blue => new(0, 0, 255);
        public static Color Yellow => new(255, 255, 0);
        public static Color White => new(255, 255, 255);
        public static Color Black => new(0, 0, 0);
        public static Color Transparent => new(0, 0, 0, 0);

        public Color() : this(0, 0, 0, 255) { }

        public Color(byte r, byte g, byte b, byte a = 255)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        internal SDL_Color ToSDLColor()
        {
            return new SDL_Color()
            {
                r = R,
                g = G,
                b = B,
                a = A,
            };
        }
    }
}
