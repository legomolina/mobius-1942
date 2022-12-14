using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Exceptions
{
    public class TextureFileExtensionException : Exception
    {
        public TextureFileExtensionException(string extension) : base($"Unavailable loader for extension {extension}.") { }
    }
}
