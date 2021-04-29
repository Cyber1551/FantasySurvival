
using System;
using UnityEngine;

namespace Inventory.Items
{
    public struct ItemSlot
    {
        public InventoryItem item;
        [Min(1)] public int quantity;

        public ItemSlot(InventoryItem _item, int _quantity)
        {
            item = _item;
            quantity = _quantity;
        }

        public bool IsEmpty()
        {
            return item == null;
        }
        public void Clear()
        {
            item = null;
            quantity = 0;
        }
        public int SpaceRemaining()
        {
            return item.MaxStack - quantity;
        }
        public static bool operator ==(ItemSlot a, ItemSlot b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(ItemSlot a, ItemSlot b)
        {
            return !a.Equals(b);
        }
    }
}