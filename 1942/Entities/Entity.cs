using Engine.Components;
using Engine.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942.Entities
{
    internal abstract class Entity : GameComponent
    {
        internal Point Center
        {
            get
            {
                return new Point(Position.X + Width / 2, Position.Y + Height / 2);
            }
        }
    }
}
