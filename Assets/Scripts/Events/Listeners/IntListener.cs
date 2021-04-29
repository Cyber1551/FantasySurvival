using Events.CustomEvents;
using Events.UnityEvents;

namespace Events.Listeners
{
    public class IntListener : BaseGameEventListener<int, IntEvent, UnityIntEvent> { }
}