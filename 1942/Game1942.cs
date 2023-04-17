using _1942.Entities;
using _1942.Managers;
using _1942.Stages;
using Engine;
using Engine.Core;
using Engine.Core.Managers;

namespace _1942
{
    public class Game1942 : Game
    {
        private readonly InputManager inputManager;
        private readonly StageManager stageManager;

        public Game1942() : base()
        {
            Graphics.WindowWidth = 512;
            Graphics.WindowHeight = 800;

            stageManager = new StageManager();

            Level1 level1 = new Level1(Graphics, BatchRenderer);
            stageManager.PushStage(level1);
            
            inputManager = InputManager.Instance;
        }

        public override void Initialize()
        {
            stageManager.PeekStage().Initialize();
        }

        public override void LoadContent(AssetManager assetManager)
        {
            base.LoadContent(assetManager);
            stageManager.PeekStage().LoadContent(assetManager);
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update(gameTime);
            stageManager.PeekStage().Update(gameTime);
        }

        public override void Render()
        {
            BatchRenderer.Start();

            stageManager.PeekStage().Render();

            BatchRenderer.End();

            // Show FPS in console
            // Console.WriteLine(Math.Truncate(FPS).ToString());
        }
    }
}
