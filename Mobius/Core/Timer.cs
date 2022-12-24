using static SDL2.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    public class Timer
    {
        private float startTime;

        public Timer()
        {

        }

        public void Start()
        {
            startTime = SDL_GetTicks();
        }

        public float ElapsedTime()
        {
            return SDL_GetTicks() - startTime;
        }

        public void Reset()
        {
            startTime = SDL_GetTicks();
        }
    }
}
