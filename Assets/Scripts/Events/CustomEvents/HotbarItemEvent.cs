using Inventory.Items;
using UnityEngine;

namespace Events.CustomEvents
{
    [CreateAssetMenu(fileName = "New Hotbar Item Event", menuName = "Game Events/Hotbar Item Event")]
    public class HotbarItemEvent : BaseGameEvent<HotbarItem> { }

}