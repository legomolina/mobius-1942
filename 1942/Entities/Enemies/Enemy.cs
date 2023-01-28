using Engine.Core;
using Engine.Core.Managers;
using System.Transactions;

namespace _1942.Entities.Enemies
{
    public abstract class Enemy : Ship
    {
        protected readonly Player player;
        protected bool isColliding = false;

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

        public override void Render()
        {
            base.Render();

            if (!Active)
            {
                return;
            }

#if DEBUG
            DebugManager.DrawRectangle(Bounds, isColliding ? Color.Red : Color.Green);
#endif
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!Active)
            {
                return;
            }

            if (Bounds.Intersects(player.Bounds))
            {
                isColliding = true;

                player.Destroy();
                Destroy();
            }
        }
    }
}
