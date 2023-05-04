using static SDL2.SDL;
using Engine.Core;
using Engine.Exceptions;
using Engine.Core.Managers;

namespace Engine
{
    public abstract class Game : IDisposable
    {
        private readonly GameTime? gameTime;

        private bool isRunning = true;

        protected AudioManager Audio { get; private set; }
        protected BatchRenderer BatchRenderer { get; private set; }
        protected float FPS { get; private set; }
        protected GraphicsManager Graphics { get; private set; }
        protected InputManager InputManager { get; private set; }

        public AssetManager AssetManager { get; private set; }

        public Game()
        {
            Graphics = GraphicsManager.Instance;

            if (!Graphics.Initialized)
            {
                throw new GraphicsNotInitializedException();
            }

            Audio = AudioManager.Instance;
            InputManager = InputManager.Instance;

            if (!Audio.Initialized)
            {
                throw new AudioNotInitializedException();
            }

            AssetManager = new AssetManager(Graphics!, Audio!);
            BatchRenderer = new BatchRenderer(Graphics);
            gameTime = new GameTime();
        }

        public void Dispose()
        {
            Audio.Dispose();
            Graphics.Dispose();

            SDL_Quit();
            isRunning = false;
        }

        public virtual void Initialize() { }

        public virtual void LoadContent(AssetManager assetManager) { }

        public void Run()
        {
            Initialize();

            LoadContent(AssetManager);

            while (isRunning)
            {
                ulong fpsStart = SDL_GetPerformanceCounter();

                gameTime!.Update();

                while (SDL_PollEvent(out SDL_Event ev) != 0)
                {
                    if (ev.type == SDL_EventType.SDL_QUIT)
                    {
                        isRunning = false;
                    }

                    InputManager.HandleInputEvents(ev);
                }

                Render();

                Update(gameTime);

                gameTime.LateUpdate();

                ulong fpsEnd = SDL_GetPerformanceCounter();
                float elapsed = (fpsEnd - fpsStart) / (float)SDL_GetPerformanceFrequency();
                FPS = 1.0f / elapsed;
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            InputManager.Update(gameTime);
        }

        public abstract void Render();
    }
}
