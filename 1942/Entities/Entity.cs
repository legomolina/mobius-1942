using Engine.Components;
using Engine.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942.Entities
{
    public abstract class Entity : GameComponent
    {
        public Point Center
        {
            get
            {
                return new Point(Position.X + Width / 2, Position.Y + Height / 2);
            }
        }

        public int Health { get; protected set; }

        public virtual Rectangle GetArea()
        {
            return new Rectangle(Position.X, Position.Y, Width, Height);
        }
    }
}
