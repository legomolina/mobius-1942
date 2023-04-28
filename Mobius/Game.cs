﻿using static SDL2.SDL;
using Engine.Core;
using Engine.Exceptions;
using Engine.Core.Managers;

namespace Engine
{
    public abstract class Game : IDisposable
    {
        private readonly GameTime? gameTime;

        private bool isRunning = true;
        private SDL_Event events;

        protected BatchRenderer BatchRenderer { get; private set; }
        protected int FrameRate { get; set; } = 120;
        protected AudioManager Audio { get; private set; }
        protected float FPS { get; private set; }
        protected GraphicsManager Graphics { get; private set; }

        public AssetManager AssetManager { get; private set; }

        public Game()
        {
            Graphics = GraphicsManager.Instance;

            if (!Graphics.Initialized)
            {
                throw new GraphicsNotInitializedException();
            }

            Audio = AudioManager.Instance;

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

                while (SDL_PollEvent(out events) != 0)
                {
                    if (events.type == SDL_EventType.SDL_QUIT)
                    {
                        isRunning = false;
                    }
                }

                Render();

                Update(gameTime);

                gameTime.LateUpdate();

                ulong fpsEnd = SDL_GetPerformanceCounter();
                float elapsed = (fpsEnd - fpsStart) / (float)SDL_GetPerformanceFrequency();
                FPS = 1.0f / elapsed;
            }
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Render();
    }
}
