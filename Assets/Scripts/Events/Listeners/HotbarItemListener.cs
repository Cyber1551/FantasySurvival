using Events.CustomEvents;
using Events.UnityEvents;
using Inventory.Items;

namespace Events.Listeners
{
    public class HotbarItemListener : BaseGameEventListener<HotbarItem, HotbarItemEvent, UnityHotbarItemEvent> { }

}