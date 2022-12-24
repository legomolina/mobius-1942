using Engine.Core;

namespace _1942.Entities.Enemies
{
    internal abstract class Enemy : Ship
    {
        protected readonly Player player;

        protected Engine.Core.Timer timer = new();

        protected Enemy(GraphicsManager graphics, Player player) : base(graphics)
        {
            this.player = player;
        }
    }
}
