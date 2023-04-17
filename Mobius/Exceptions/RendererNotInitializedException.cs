using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Exceptions
{
    public class RendererNotInitializedException : Exception
    {
        public RendererNotInitializedException() : base("Renderer is not initialized, initialize it first calling `Start()` method.") { }
    }
}
