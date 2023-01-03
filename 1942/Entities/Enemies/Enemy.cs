using Engine.Core;

namespace _1942.Entities.Enemies
{
    public abstract class Enemy : Ship
    {
        protected readonly Player player;

        public bool Initialized = false;

        protected Enemy(GraphicsManager graphics, Player player) : base(graphics)
        {
            Active = false;
            this.player = player;
        }

        public virtual void Initialize()
        {
            Active = true;
            Initialized= true;
        }
    }
}
