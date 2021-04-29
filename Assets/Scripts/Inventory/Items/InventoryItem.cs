using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Items
{
    public abstract class InventoryItem : HotbarItem
    {
        [Header("Item Data")]
        [SerializeField] [Min(1)] private int maxStack = 99;

        public override string ColoredName
        {
            get
            {
                return Name;
            }
        }
        public int MaxStack => maxStack;

    }
}
