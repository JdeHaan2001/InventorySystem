using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HotbarSlot : ItemSlotUI, IDropHandler
{
    [SerializeField] private Inventory inventory = null;
    [SerializeField] private TextMeshProUGUI itemQuantityText = null;
    [SerializeField] private PlayerStats playerStats = null;

    private Item slotItem = null;

    public override Item SlotItem
    {
        get { return slotItem; }
        set { slotItem = value; UpdateSlotUI(); }
    }

    public bool AddItem(Item itemToAdd)
    {
        if (SlotItem != null) return false;

        SlotItem = itemToAdd;

        return true;
    }

    public void UseSlot(int index)
    {
        if (index != SlotIndex) return;

        if (slotItem is HealthItem)
        {
            HealthItem item = slotItem as HealthItem;
            playerStats.Heal(item.HealAmount);

            var itemToRemove = new ItemSlot((InventoryItem)slotItem, 1);
            inventory.ItemContainer.RemoveItem(itemToRemove); //This line of code can kill itself, I hate it
            UpdateSlotUI();
        }
        
    }

    public override void OnDrop(PointerEventData eventData)
    {
        ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

        if (itemDragHandler == null) return;

        InventorySlot inventorySlot = itemDragHandler.ItemSlotUI as InventorySlot;
        if (inventorySlot != null)
        {
            SlotItem = inventorySlot.ItemSlot.item;
            return;
        }

        HotbarSlot hotbarSlot = itemDragHandler.ItemSlotUI as HotbarSlot;
        if (hotbarSlot != null)
        {
            Item oldItem = SlotItem;
            SlotItem = hotbarSlot.SlotItem;
            hotbarSlot.SlotItem = oldItem;
            return;
        }
    }

    public override void UpdateSlotUI()
    {
        if (SlotItem == null)
        {
            EnableSlotUI(false);
            return;
        }

        itemIcon.sprite = SlotItem.Icon;

        EnableSlotUI(true);

        SetItemQuantityUI();
    }

    private void SetItemQuantityUI()
    {
        if (SlotItem is InventoryItem inventoryItem)
        {
            if (inventory.ItemContainer.HasItem(inventoryItem))
            {
                int quantityCount = inventory.ItemContainer.GetTotalQuantity(inventoryItem);
                itemQuantityText.text = quantityCount > 1 ? quantityCount.ToString() : "";
            }
            else
            {
                SlotItem = null;
            }
        }
        else
        {
            itemQuantityText.enabled = false;
        }
    }

    protected override void EnableSlotUI(bool enable)
    {
        base.EnableSlotUI(enable);
        itemQuantityText.enabled = enable;
    }
}
