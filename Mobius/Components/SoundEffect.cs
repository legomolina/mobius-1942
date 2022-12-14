using static SDL2.SDL_mixer;
using Engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Components
{
    public class SoundEffect : IDisposable
    {
        private readonly IntPtr soundEffect;

        public SoundEffect(IntPtr soundEffect)
        {
            this.soundEffect = soundEffect;

            // TODO volume by channels
        }

        public void SetVolume(int volume)
        {
            // TODO volume by channels/instance
            // All pointers share same volume
            Mix_VolumeChunk(soundEffect, volume);
        }

        public void Play(int times = 1, int channel = -1)
        {
            Mix_PlayChannel(channel, soundEffect, times - 1);
        }

        public void Dispose()
        {
            Mix_FreeChunk(soundEffect);
        }
    }
}
