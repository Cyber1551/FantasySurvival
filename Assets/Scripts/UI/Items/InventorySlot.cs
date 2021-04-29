using Inventory.Items;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Inventory;

namespace UI.Items
{
    public class InventorySlot : ItemSlotUI, IDropHandler
    {
        [SerializeField] private BaseInventory inventory = null;
        [SerializeField] private TextMeshProUGUI itemQuantityText = null;

        public override HotbarItem SlotItem
        {
            get { return ItemSlot.item; }
            set { }
        }

        public ItemSlot ItemSlot => inventory.ItemContainer.GetSlotByIndex(SlotIndex);

        public override void OnDrop(PointerEventData eventData)
        {
            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
            if (itemDragHandler == null) return;
            if ((itemDragHandler.ItemSlotUI as InventorySlot) != null)
            {
                inventory.ItemContainer.SwapItem(itemDragHandler.ItemSlotUI.SlotIndex, SlotIndex);
            }
        }
        public override void UpdateSlotUI()
        {
            if (ItemSlot.item == null)
            {
                SetEnabled(false);
                return;
            }

            SetEnabled(true);

            itemIconImage.sprite = ItemSlot.item.Icon;
            itemQuantityText.text = ItemSlot.quantity > 1 ? ItemSlot.quantity.ToString() : "";
        }
        protected override void SetEnabled(bool enable)
        {
            base.SetEnabled(enable);
            itemQuantityText.enabled = enable;
        }
    }
}