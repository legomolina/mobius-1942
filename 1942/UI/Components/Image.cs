using Engine.Components;
using Engine.Core;
using Engine.Core.Managers;
using Engine.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _1942.UI.Components
{
    public class Image : UIComponent
    {
        private readonly string filename;

        private Texture texture;

        public Rectangle ClipRect { get; set; }
        public Rectangle RenderRect => new Rectangle(Position.X, Position.Y, Width, Height);

        public Image(GraphicsManager graphics, BatchRenderer renderer, string filename) : base(graphics, renderer)
        {
            this.filename = filename;
        }

        public override void LoadContent(AssetManager assetManager)
        {
            texture = assetManager.LoadTexture(filename);
            
            Width = texture.Width;
            Height = texture.Height;

            ClipRect = new Rectangle(0, 0, Width, Height);
        }

        public override void Render()
        {
            texture.Render(RenderRect, ClipRect);
        }
    }
}
