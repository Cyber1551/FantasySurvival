using Events.CustomEvents;
using Events.UnityEvents;

namespace Events.Listeners
{
    public class VoidListener : BaseGameEventListener<Void, VoidEvent, UnityVoidEvent> { }

}