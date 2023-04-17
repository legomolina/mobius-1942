using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Exceptions
{
    public class RendererAlreadyInitializedException : Exception
    {
        public RendererAlreadyInitializedException() : base("Renderer is already initialized") { }
    }
}
