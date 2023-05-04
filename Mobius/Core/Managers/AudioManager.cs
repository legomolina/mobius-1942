using static SDL2.SDL;
using static SDL2.SDL_mixer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Exceptions;

namespace Engine.Core.Managers
{
    public class AudioManager : IDisposable
    {
        private static AudioManager? instance;

        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AudioManager();
                }

                return instance;
            }
        }

        public bool Initialized { get; private set; } = false;

        private AudioManager()
        {
            Initialized = Init();
        }

        private bool Init()
        {
            if (SDL_Init(SDL_INIT_AUDIO) < 0)
            {
                Console.WriteLine($"SDL Audio initialization error: {SDL_GetError()}");
                return false;
            }

            if (Mix_OpenAudio(44100, MIX_DEFAULT_FORMAT, 2, 2048) < 0)
            {
                Console.WriteLine($"SDL_mixer initialization error: {Mix_GetError()}");
                return false;
            }

            return true;
        }

        internal IntPtr LoadSoundEffect(string file)
        {
            IntPtr sound = Mix_LoadWAV(file);

            if (sound == IntPtr.Zero)
            {
                throw new SoundEffectCreatingException(file);
            }

            return sound;
        }

        public void Dispose()
        {
            Initialized = false;
            Mix_Quit();
        }
    }
}
