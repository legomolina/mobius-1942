using static SDL2.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Exceptions
{
    public class TextureCreatingException : Exception
    {
        public TextureCreatingException(string path) : base($"Error creating texture {path}. {SDL_GetError()}") { }
    }
}
