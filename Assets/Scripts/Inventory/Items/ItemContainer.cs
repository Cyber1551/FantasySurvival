using System;

namespace Inventory.Items
{
    [Serializable]
    public class ItemContainer : IItemContainer
    {
        private ItemSlot[] itemSlots = new ItemSlot[0];

        public Action OnItemUpdated = delegate { };

        public ItemContainer(int size) => itemSlots = new ItemSlot[size];
        public ItemSlot GetSlotByIndex(int index) => itemSlots[index];
        public ItemSlot AddItem(ItemSlot itemSlot)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (!itemSlots[i].IsEmpty())
                {
                    if (itemSlots[i].item == itemSlot.item)
                    {
                        int slotRemainingSpace = itemSlots[i].SpaceRemaining();
                        if (itemSlot.quantity <= slotRemainingSpace)
                        {
                            itemSlots[i].quantity += itemSlot.quantity;
                            itemSlot.quantity = 0;
                            OnItemUpdated.Invoke();
                            return itemSlot;
                        }
                        else if (slotRemainingSpace > 0)
                        {
                            itemSlots[i].quantity += slotRemainingSpace;
                            itemSlot.quantity -= slotRemainingSpace;
                        }
                    }
                }
            }
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].IsEmpty())
                {
                    if (itemSlot.quantity <= itemSlot.item.MaxStack)
                    {
                        itemSlots[i] = itemSlot;
                        itemSlot.quantity = 0;
                        OnItemUpdated.Invoke();
                        return itemSlot;
                    }
                    else
                    {
                        itemSlots[i] = new ItemSlot(itemSlot.item, itemSlot.item.MaxStack);
                        itemSlot.quantity -= itemSlot.item.MaxStack;
                    }
                }
            }
            OnItemUpdated.Invoke();
            return itemSlot;
        }

        public int GetTotalQuantity(InventoryItem item)
        {
            int totalCount = 0;

            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.IsEmpty()) continue;
                if (itemSlot.item != item) continue;
                totalCount += itemSlot.quantity;
            }
            return totalCount;
        }

        public bool HasItem(InventoryItem item)
        {
            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.IsEmpty()) continue;
                if (itemSlot.item != item) continue;
                return true;
            }
            return false;
        }

        public void RemoveItem(ItemSlot itemSlot)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (!itemSlots[i].IsEmpty())
                {
                    if (itemSlots[i].item == itemSlot.item)
                    {
                        if (itemSlots[i].quantity < itemSlot.quantity)
                        {
                            itemSlot.quantity -= itemSlots[i].quantity;
                            itemSlots[i].Clear();
                        }
                        else
                        {
                            itemSlots[i].quantity -= itemSlot.quantity;
                            if (itemSlots[i].quantity == 0)
                            {
                                itemSlots[i].Clear();
                                OnItemUpdated.Invoke();
                                return;
                            }
                        }
                    }
                }
            }
        }

        public void RemoveItemAt(int index)
        {
            if (index < 0 || index > itemSlots.Length - 1) return;
            itemSlots[index].Clear();
            OnItemUpdated.Invoke();
        }

        public void SwapItem(int indexOne, int indexTwo)
        {
            ItemSlot firstSlot = itemSlots[indexOne];
            ItemSlot secondSlot = itemSlots[indexTwo];

            if (firstSlot == secondSlot) return;
            if (!secondSlot.IsEmpty())
            {
                if (firstSlot.item == secondSlot.item)
                {
                    int secondSlotRemainingSpace = secondSlot.SpaceRemaining();
                    if (firstSlot.quantity <= secondSlotRemainingSpace)
                    {
                        itemSlots[indexTwo].quantity += firstSlot.quantity;
                        itemSlots[indexOne].Clear();
                        OnItemUpdated.Invoke();
                        return;
                    }
                }
            }

            itemSlots[indexOne] = secondSlot;
            itemSlots[indexTwo] = firstSlot;
            OnItemUpdated.Invoke();
        }
    }
}