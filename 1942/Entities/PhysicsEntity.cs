using Engine.Core;
using Engine.Core.Collisions;
using Engine.Core.Managers;
using Engine.Core.Math;

namespace _1942.Entities
{
    public abstract class PhysicsEntity : Entity, ICollidable
    {
        protected readonly BatchRenderer renderer;

        protected bool IsColliding { get; set; }

        public PhysicsEntity(BatchRenderer renderer)
        {
            this.renderer = renderer;
        }

        public abstract void OnCollision(ICollidable other);

        public override void Render()
        {
#if DEBUG
            renderer.Render(DebugManager.DrawText(new Point(Position.X + Width + 10, Position.Y - 5), Color.Blue, $"({Math.Truncate(Position.X)}, {Math.Truncate(Position.Y)})"));
            renderer.Render(DebugManager.DrawText(new Point(Position.X + Width + 10, Position.Y - 5 + 15), Color.Green, $"{Math.Truncate(Rotation)} deg"));
            renderer.Render(DebugManager.DrawText(new Point(Position.X + Width + 10, Position.Y - 5 + 30), Color.Yellow, $"{Scale}"));
            renderer.Render(DebugManager.DrawRectangle(Bounds, Color.Red));
#endif
        }
    }
}
