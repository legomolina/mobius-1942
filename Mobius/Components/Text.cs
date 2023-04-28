using static SDL2.SDL_ttf;
using Engine.Core;
using Engine.Core.Managers;
using Engine.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Components
{
    public class Text : IDrawable
    {
        private readonly GraphicsManager graphics;

        public bool Active { get; set; }

        public string Content { get; set; }

        public Font Font { get; set; }

        public int Order { get; set; }

        public Point Position { get; set; }

        public Text(GraphicsManager graphics)
        {
            this.graphics = graphics;
        }

        public void LoadContent(AssetManager assetManager) { }

        public void MeasureString(out int width, out int height)
        {
            TTF_SizeText(Font.Pointer, Content, out width, out height);
        }

        public void Render()
        {
            if (Font != null)
            {
                graphics.DrawText(this, Position.ToSDLPoint());
            }
        }
    }
}
