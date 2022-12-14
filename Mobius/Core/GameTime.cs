using static SDL2.SDL;

namespace Engine.Core
{
    /// <summary>
    /// <see cref="https://gamedev.stackexchange.com/questions/151877/handling-variable-frame-rate-in-sdl2"/>
    /// </summary>
    public class GameTime
    {
        private const uint MIN_FPS_DELTA_TIME = 1000 / 6;

        private uint now = SDL_GetTicks();
        private uint lastStep = SDL_GetTicks();

        public float CurrentTime { get => now; }
        public float DeltaTime { get; private set; } = 0;
        public float LastTime { get => lastStep; }

        internal void Update()
        {
            now = SDL_GetTicks();

            if (lastStep < now)
            {
                DeltaTime = now - lastStep;

                if (DeltaTime > MIN_FPS_DELTA_TIME)
                {
                    DeltaTime = MIN_FPS_DELTA_TIME;
                }
            } else
            {
                SDL_Delay(1);
            }
        }

        internal void LateUpdate()
        {
            lastStep = now;
        }
    }
}
