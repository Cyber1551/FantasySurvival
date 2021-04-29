using Events.CustomEvents;
using Inventory.Items;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Items/Inventory")]
    public class BaseInventory : ScriptableObject
    {
        [SerializeField] private VoidEvent onInventoryItemsUpdated = null;
        public ItemContainer ItemContainer { get; } = new ItemContainer(20);

        public void OnEnable()
        {
            ItemContainer.OnItemUpdated += onInventoryItemsUpdated.Raise;
        }
        public void OnDisable()
        {
            ItemContainer.OnItemUpdated -= onInventoryItemsUpdated.Raise;
        }
    }
}