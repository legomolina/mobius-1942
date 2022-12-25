using Engine.Core;
using Engine.Core.Automation.Tracking;

namespace Mobius.Core.Automation.Tracking
{
    public interface ITrackFollower : IUpdatable
    {
        public Track Track { get; set; }

        public void StartFollow();
    }
}