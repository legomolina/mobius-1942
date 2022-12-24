using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Pathfinding
{
    public interface IPathFinding : IUpdatable
    {
        IList<Waypoint> Waypoints { get; }
        Waypoint[] CurrentSegment { get; }
        Waypoint[] NextSegment { get; }

        void InitPath();
        void StopPath();
    }
}
