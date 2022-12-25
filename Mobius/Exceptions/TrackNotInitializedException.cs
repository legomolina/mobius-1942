using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobius.Exceptions
{
    public class TrackNotInitializedException : Exception
    {
        public TrackNotInitializedException(string method) : base($"Method begin must be called before `{method}`.") { }
    }
}