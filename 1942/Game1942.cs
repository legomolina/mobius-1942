using _1942.Entities;
using Engine;
using Engine.Components;
using Engine.Core;
using Engine.Core.Math;

namespace _1942
{
    internal class Game1942 : Game
    {
        private readonly InputManager inputManager;
        private readonly Player player;
        private readonly Enemy enemy;

        private Sprite? background;

        internal Game1942() : base()
        {
            Graphics.WindowWidth = 512;
            Graphics.WindowHeight = 800;

            inputManager = InputManager.Instance;
            player = new Player(Graphics);
            enemy = new Enemy(Graphics, player);
        }

        public override void LoadContent(AssetManager assetManager)
        {
            Texture background = assetManager.LoadTexture("Assets/Maps/test_level.png");
            this.background = new Sprite(background);

            base.LoadContent(assetManager);
            player.LoadContent(assetManager);
            enemy.LoadContent(assetManager);

            player.Position = new Point(Graphics.WindowWidth / 2 - player.Width / 2, Graphics.WindowHeight - player.Height);
            enemy.Position = new Point(Graphics.WindowWidth / 2 - enemy.Width / 2, 360);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            inputManager.Update(gameTime);

            Graphics!.ClearBackBuffer();

            background!.Render();
            player.Update(gameTime);
            player.Render();
            enemy.Update(gameTime);
            enemy.Render();

            Graphics.Render();
        }
    }
}
