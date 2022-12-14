using static SDL2.SDL_mixer;

namespace Engine.Exceptions
{
    public class SoundEffectCreatingException : Exception
    {
        public SoundEffectCreatingException(string path) : base($"Error creating sounde effect {path}. {Mix_GetError()}") { }
    }
}
