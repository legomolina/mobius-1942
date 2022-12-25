using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobius.Exceptions
{
    public class InsufficientWaypointsOnTrackException : Exception
    {
        public InsufficientWaypointsOnTrackException() : base("Track must have at least two waypoints.") { }
    }
}