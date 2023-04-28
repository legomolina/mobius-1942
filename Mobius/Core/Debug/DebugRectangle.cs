using Engine.Core.Managers;
using Engine.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Debug
{
    public class DebugRectangle : DebugDrawable
    {
        private Rectangle rectangle;
        private Color color;

        public DebugRectangle(GraphicsManager graphics, Rectangle rectangle, Color color) : base(graphics)
        {
            this.rectangle = rectangle;
            this.color = color;
        }

        public override void Render()
        {
            graphics.DrawRectangle(rectangle.ToSDLRect(), color.R, color.G, color.B, color.A);
        }
    }
}
