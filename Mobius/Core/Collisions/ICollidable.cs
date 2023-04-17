using Engine.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Collisions
{
    public interface ICollidable : IUpdatable
    {
        bool Active { get; }
        Rectangle Bounds { get; }

        void OnCollision(ICollidable other);
    }
}
