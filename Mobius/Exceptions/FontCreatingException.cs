using static SDL2.SDL_ttf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Exceptions
{
    public class FontCreatingException : Exception
    {
        public FontCreatingException(string path) : base($"Error creating font {path}. {TTF_GetError()}") { }
    }
}
