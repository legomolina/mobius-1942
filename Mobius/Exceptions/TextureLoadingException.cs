using static SDL2.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Exceptions
{
    public class TextureLoadingException : Exception
    {
        public TextureLoadingException(string path) : base($"Error loading texture {path}. {SDL_GetError()}") { }
    }
}
