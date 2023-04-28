using Engine.Core.Managers;
using Engine.Core.Math;


namespace Engine.Core.Debug
{
    public class DebugLine : DebugDrawable
    {
        private Point point1;
        private Point point2;
        private Color color;

        public DebugLine(GraphicsManager graphics, Point point1, Point point2, Color color) : base(graphics)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.color = color;
        }

        public override void Render()
        {
            GraphicsManager.Instance.DrawLine(point1.ToSDLPoint(), point2.ToSDLPoint(), color.R, color.G, color.B, color.A);
        }
    }
}
