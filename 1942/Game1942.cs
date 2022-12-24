using _1942.Entities;
using _1942.Entities.Enemies;
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

        private Fighter fighter;
        private Sprite? background;

        internal Game1942() : base()
        {
            Graphics.WindowWidth = 512;
            Graphics.WindowHeight = 800;

            inputManager = InputManager.Instance;
            player = new Player(Graphics);
            fighter = new(Graphics, player);
        }

        public override void LoadContent(AssetManager assetManager)
        {
            Texture background = assetManager.LoadTexture("Assets/Maps/test_level.png");
            this.background = new Sprite(background);

            base.LoadContent(assetManager);
            player.LoadContent(assetManager);
            fighter.LoadContent(assetManager);

            player.Position = new Point(Graphics.WindowWidth / 2 - player.Width / 2, Graphics.WindowHeight - player.Height);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            inputManager.Update(gameTime);
            player.Update(gameTime);

            if (fighter.Health > 0)
            {
                fighter.Update(gameTime);
            } 
            else
            {
                fighter.Dispose();
            }
        }

        public override void Render()
        {
            base.Render();

            Graphics!.ClearBackBuffer();

            background!.Render();
            player.Render();

            if (fighter.Health > 0)
            {
                fighter.Render();
            }

            Graphics.Render();
        }
    }
}
