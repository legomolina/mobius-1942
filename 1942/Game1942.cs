using _1942.Entities;
using _1942.Entities.Enemies;
using Engine;
using Engine.Components;
using Engine.Core;
using Engine.Core.Math;
using System;

namespace _1942
{
    public class Game1942 : Game
    {
        private readonly InputManager inputManager;
        private readonly Player player;

        private IList<Fighter> fighters;

        private Sprite? background;

        public Game1942() : base()
        {
            Graphics.WindowWidth = 512;
            Graphics.WindowHeight = 800;

            inputManager = InputManager.Instance;
            player = new Player(Graphics);
            player.Position = new Point(Graphics.WindowWidth / 2 - player.Width / 2, Graphics.WindowHeight - player.Height);
            fighters = new List<Fighter>()
            {
                new(Graphics, player),
                new(Graphics, player),
                new(Graphics, player),
                new(Graphics, player),
                new(Graphics, player),
                new(Graphics, player),
                new(Graphics, player),
                new(Graphics, player),
            };
        }

        public override void LoadContent(AssetManager assetManager)
        {
            Texture background = assetManager.LoadTexture("Assets/Maps/test_level.png");
            this.background = new Sprite(background);

            base.LoadContent(assetManager);
            player.LoadContent(assetManager);
            
            foreach(Fighter fighter in fighters)
            { 
                fighter.LoadContent(assetManager);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            inputManager.Update(gameTime);
            player.Update(gameTime);

            foreach (Fighter fighter in fighters)
            {
                if (fighter.Health > 0)
                {
                    fighter.Update(gameTime);
                }
                else
                {
                    fighter.Dispose();
                }
            }
        }

        public override void Render()
        {
            base.Render();

            Graphics!.ClearBackBuffer();

            background!.Render();
            player.Render();

            foreach (Fighter fighter in fighters)
            {
                if (fighter.Health > 0)
                {
                    fighter.Render();
                }
                else
                {
                    fighter.Dispose();
                }
            }

            Graphics.Render();

            // Show FPS in console
            // Console.WriteLine(Math.Truncate(FPS).ToString());
        }
    }
}
