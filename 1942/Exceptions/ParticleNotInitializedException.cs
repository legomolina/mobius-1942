using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942.Exceptions
{
    public class ParticleNotInitializedException : Exception
    {
        public ParticleNotInitializedException(string method) : base($"Method begin must be called before `{method}`.") { }
    }
}
