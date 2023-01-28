using _1942.Core;
using _1942.Entities;
using _1942.Entities.Enemies;
using _1942.Managers;
using Engine.Components;
using Engine.Core.Managers;
using Engine.Core.Math;

namespace _1942.Stages
{
    public class Level1 : Stage
    {
        private readonly EnemyManager enemyManager;
        private readonly GraphicsManager graphics;
        private readonly Player player;

        private Sprite background;

        public Level1(GraphicsManager graphics) 
        {
            this.enemyManager = new EnemyManager();
            this.graphics = graphics;
            this.player = new Player(graphics, enemyManager);
        }

        public override void Initialize()
        {
            player.Position = new Point(graphics.WindowWidth / 2 - player.Width / 2, graphics.WindowHeight - player.Height);

            enemyManager.AddEnemy(new Fighter(graphics, player), 2);
            enemyManager.AddEnemyRelative(new Fighter(graphics, player), 1);
            enemyManager.AddEnemyRelative(new Fighter(graphics, player), 1);
        }

        public override void LoadContent(AssetManager assetManager)
        {
            Texture backgroundTexture = assetManager.LoadTexture("Assets/Maps/test_level.png");
            background = new Sprite(backgroundTexture);
            background.Order = -1;

            enemyManager.LoadContent(assetManager);
            player.LoadContent(assetManager);

            enemyManager.Start();

            Components.Add(background);
            Components.Add(enemyManager);
            Components.Add(player);
        }
    }
}
