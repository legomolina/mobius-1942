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

        private MainMenu mainMenu;
        private Level1 level1;

        public Game1942() : base()
        {
            Graphics.WindowWidth = 512;
            Graphics.WindowHeight = 800;

            inputManager = InputManager.Instance;
            stageManager = new StageManager(this);

            mainMenu = new MainMenu(Graphics, BatchRenderer);
            level1 = new Level1(Graphics, BatchRenderer);

            stageManager.PushStage(mainMenu);

            mainMenu.NewGame += (s, e) => 
            {
                stageManager.PushStage(level1);
            };
            mainMenu.QuitGame += (s, e) =>
            {
                Dispose();
            };
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
