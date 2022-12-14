using static SDL2.SDL;
using Engine.Core;
using Engine.Exceptions;

namespace Engine
{
    public abstract class Game : IDisposable
    {
        private readonly AssetManager assetManager;
        private readonly GameTime? gameTime;

        private bool isRunning = true;
        private SDL_Event events;

        protected int FrameRate { get; set; } = 120;
        protected GraphicsManager Graphics { get; private set; }

        public Game()
        {
            Graphics = GraphicsManager.Instance;

            if (!Graphics.Initialized)
            {
                throw new GraphicsNotInitializedException();
            }

            assetManager = new AssetManager(Graphics!);
            gameTime = new GameTime();
        }

        public void Dispose()
        {
            Graphics.Dispose();
        }

        public virtual void LoadContent(AssetManager assetManager) { }

        public void Run()
        {
            LoadContent(assetManager);

            while (isRunning)
            {
                gameTime!.Update();

                while (SDL_PollEvent(out events) != 0)
                {
                    if (events.type == SDL_EventType.SDL_QUIT)
                    {
                        isRunning = false;
                    }
                }

                Update(gameTime);

                gameTime.LateUpdate();
            }
        }

        public virtual void Update(GameTime gameTime) { }
    }
}
