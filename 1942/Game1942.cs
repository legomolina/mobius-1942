using _1942.Entities;
using _1942.Entities.Enemies;
using _1942.Managers;
using Engine;
using Engine.Components;
using Engine.Core;
using Engine.Core.Math;
using System;

namespace _1942
{
    public class Game1942 : Game
    {
        private readonly EnemyManager enemyManager;
        private readonly InputManager inputManager;
        private readonly Player player;

        private Sprite? background;

        public Game1942() : base()
        {
            Graphics.WindowWidth = 512;
            Graphics.WindowHeight = 800;

            enemyManager = new EnemyManager();
            inputManager = InputManager.Instance;
            player = new Player(Graphics);
            player.Position = new Point(Graphics.WindowWidth / 2 - player.Width / 2, Graphics.WindowHeight - player.Height);

            enemyManager.AddEnemy(new Fighter(Graphics, player), 7);
            enemyManager.AddEnemyRelative(new Fighter(Graphics, player), 2);
            enemyManager.AddEnemyRelative(new Fighter(Graphics, player), 5);
        }

        public override void LoadContent(AssetManager assetManager)
        {
            Texture background = assetManager.LoadTexture("Assets/Maps/test_level.png");
            this.background = new Sprite(background);

            enemyManager.LoadContent(assetManager);
            enemyManager.Initialize();

            base.LoadContent(assetManager);
            player.LoadContent(assetManager);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            inputManager.Update(gameTime);
            player.Update(gameTime);
            enemyManager.Update(gameTime);
        }

        public override void Render()
        {
            base.Render();

            Graphics!.ClearBackBuffer();

            background!.Render();
            player.Render();
            enemyManager.Render();

            Graphics.Render();

            // Show FPS in console
            // Console.WriteLine(Math.Truncate(FPS).ToString());
        }
    }
}
