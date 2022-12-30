using Engine.Core.Math;
using Mobius.Exceptions;

namespace Engine.Core.Automation.Tracking
{
    public class TrackFactory
    {
        private Track? track = null;

        public TrackFactory() { }

        public TrackFactory Begin(Point position)
        {
            track = new Track();
            track.Waypoints.Add(new Waypoint(position));

            return this;
        }

        public TrackFactory AddWaypoint(Point position)
        {
            if (track == null)
            {
                throw new TrackNotInitializedException("AddWaypoint");
            }

            track.Waypoints.Add(new Waypoint(position));

            return this;
        }

        public TrackFactory AddWaypointRelative(Point position)
        {
            if (track == null)
            {
                throw new TrackNotInitializedException("AddWaypointRelative");
            }

            Waypoint lastWaypoint = track.Waypoints.Last();
            track.Waypoints.Add(new Waypoint(lastWaypoint.Position + position));

            return this;
        }

        public TrackFactory ShootPlayer()
        {
            if (track == null || track.Waypoints.Count == 0)
            {
                throw new TrackNotInitializedException("ShootPlayer");
            }

            track.Waypoints.Last().Shoot = true;

            return this;
        }

        public Track Build(bool loop = false)
        {
            if (track == null)
            {
                throw new TrackNotInitializedException("Build");
            }

            if (track.Waypoints.Count < 2)
            {
                throw new InsufficientWaypointsOnTrackException();
            }

            track.Loop = loop;
            
            return track;
        }
    }
}