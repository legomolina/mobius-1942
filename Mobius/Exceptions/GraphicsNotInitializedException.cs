﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Exceptions
{
    public class GraphicsNotInitializedException : Exception
    {
        public GraphicsNotInitializedException() : base("An error has occurred while initializing graphics") { }
    }
}
