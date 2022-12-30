using Engine.Core.Debug;
using System;

namespace Engine.Core.Automation.Tracking
{
    public class Track
    {
        private readonly IList<Waypoint> waypoints;

        private int currentWaypointIndex = 0;

        public bool Loop { get; set; }
        public Waypoint CurrentWaypoint => waypoints[currentWaypointIndex];
        public Waypoint NextWaypoint
        {
            get
            {
                try 
                {
                    return waypoints[currentWaypointIndex + 1];
                }
                catch (ArgumentOutOfRangeException e)
                {
                    if (Loop) {
                        return waypoints[0];
                    }

                    throw e;
                }
            }
        }
        public IList<Waypoint> Waypoints => waypoints;

        public Track()
        {
            waypoints = new List<Waypoint>();
        }

        public Track(IList<Waypoint> waypoints, int currentIndex = 0)
        {
            this.currentWaypointIndex = currentIndex;
            this.waypoints = waypoints;
        }

        public void MoveNextWaypoint()
        {
            if (!Loop && currentWaypointIndex >= waypoints.Count)
            {
                throw new IndexOutOfRangeException();
            }

            currentWaypointIndex++;
        }

        public void MovePreviousWaypoint()
        {
            if (currentWaypointIndex == 0)
            {
                throw new IndexOutOfRangeException();
            }

            currentWaypointIndex--;
        }

        public bool IsLastWaypoint()
        {
            return currentWaypointIndex >= waypoints.Count - 1;
        }
    }
}