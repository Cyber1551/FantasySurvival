using Inventory.Items;
using System;
using UnityEngine.Events;
namespace Events.UnityEvents
{
    [Serializable] public class UnityHotbarItemEvent : UnityEvent<HotbarItem> { }
}