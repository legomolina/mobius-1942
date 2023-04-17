using Engine.Core;
using Engine.Core.Collisions;
using Engine.Core.Managers;

namespace _1942.Entities.Ships
{
    public abstract class Enemy : Ship
    {
        public bool Initialized { get; protected set; } = false;
        
        protected Player player;

        protected Enemy(GraphicsManager graphics, BatchRenderer renderer, Player player, CollisionsContainer collisionsContainer) : base(graphics, renderer, collisionsContainer)
        {
            this.player = player;

            Active = false;
        }

        public virtual void Initialize()
        {
            Active = true;
            Initialized = true;
        }
    }
}
