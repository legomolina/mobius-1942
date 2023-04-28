using Engine.Components;
using Engine.Core;
using Engine.Core.Managers;
using Engine.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace _1942.UI.Components
{
    public class Label : UIComponent
    {
        private readonly Text text;

        private string content;
        private Font font;
        
        public string Content
        {
            get => content;
            set
            {
                text.Content = value;
                content = value;
            }
        }

        public Font Font
        {
            get => font;
            set
            {
                text.Font = value;
                font = value;
            }
        }

        public Label(GraphicsManager graphics, BatchRenderer renderer) : base(graphics, renderer)
        {
            text = new Text(graphics);
        }

        public override void LoadContent(AssetManager assetManager) { }

        public override void Render()
        {
            text.Render();
        }

        public override void Update(GameTime gameTime)
        {
            text.Position = Position;

            text.MeasureString(out int width, out int height);

            Width = width;
            Height = height;
        }
    }
}
