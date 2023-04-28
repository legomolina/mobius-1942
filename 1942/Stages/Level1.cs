using _1942.Core;
using _1942.Entities;
using _1942.Entities.Ships;
using _1942.Managers;
using Engine.Core;
using Engine.Core.Managers;
using Engine.Core.Math;

namespace _1942.Stages
{
    public class Level1 : Stage
    {
        private readonly Background background;
        private readonly EnemyManager enemyManager;
        private readonly Player player;

        public Level1(GraphicsManager graphics, BatchRenderer renderer) : base(graphics, renderer)
        {
            this.background = new Background("Assets/Maps/map.png", graphics, renderer);
            this.enemyManager = new EnemyManager(renderer, CollisionsContainer);
            this.player = new Player(graphics, renderer, CollisionsContainer);
        }

        public override void Initialize()
        {
            player.Position = new Point(graphics.WindowWidth / 2 - player.Width / 2, graphics.WindowHeight - player.Height);

            CollisionsContainer.Insert(player);

            enemyManager.Start();
        }

        public override void LoadContent(AssetManager assetManager)
        {
            enemyManager.AddEnemy(new Fighter(graphics, renderer, player, CollisionsContainer), 2);
            enemyManager.AddEnemyRelative(new Fighter(graphics, renderer, player, CollisionsContainer), 1);
            enemyManager.AddEnemyRelative(new Fighter(graphics, renderer, player, CollisionsContainer), 1);

            background.LoadContent(assetManager);
            enemyManager.LoadContent(assetManager);
            player.LoadContent(assetManager);
        }

        public override void Render()
        {
            background.Render();
            enemyManager.Render();
            player.Render();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            background.Update(gameTime);
            enemyManager.Update(gameTime);
            player.Update(gameTime);
        }
    }
}
