using static SDL2.SDL;
using static SDL2.SDL_ttf;
using Engine.Core;
using Engine.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Components
{
    public class Font : IDisposable
    {
        public const int DEFAULT_FONT_SIZE = 12;

        private readonly GraphicsManager graphicsManager;
        private readonly string filename;
        private readonly Dictionary<int, IntPtr> sizes = new();

        private int size = DEFAULT_FONT_SIZE;

        internal IntPtr Pointer => sizes[size];

        public Color Color { get; set; }

        public int Size
        {
            get => size;
            set
            {
                if (!sizes.ContainsKey(value) || sizes[value] == IntPtr.Zero)
                {
                    sizes.Add(value, graphicsManager.LoadFont(filename, value));
                }

                size = value;
            }
        }

        public Font(GraphicsManager graphicsManager, IntPtr defaultFontSize, string filename)
        {
            this.graphicsManager = graphicsManager;
            this.filename = filename;
            sizes.Add(DEFAULT_FONT_SIZE, defaultFontSize);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
