using Engine.Components;
using Engine.Core;
using Engine.Core.Managers;
using Engine.Core.Math;

namespace _1942.Entities
{
    public abstract class Entity : GameComponent
    {
        public Rectangle Bounds => new Rectangle(Position.X, Position.Y, Width, Height);
        public Point Center
        {
            get
            {
                return new Point(Position.X + Width / 2, Position.Y + Height / 2);
            }
        }
        public int Health { get; protected set; }
        public string Tag { get; protected set; } = "Entity";
    }
}
