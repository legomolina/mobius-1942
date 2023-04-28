using Engine.Core.Managers;
using Engine.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Debug
{
    public class DebugText : DebugDrawable
    {
        private Color color;
        private string text;
        private Point position;

        public DebugText(GraphicsManager graphics, Color color, string text, Point position) : base(graphics)
        {
            this.color = color;
            this.text = text;
            this.position = position;
        }

        public override void Render()
        {
            graphics.DrawText(12, color.ToSDLColor(), text, position.ToSDLPoint());
        }
    }
}
