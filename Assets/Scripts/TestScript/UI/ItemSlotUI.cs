using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour, IPointerClickHandler
{
    InventoryUI inventoryUI;
    ItemInstance itemInstance;
    public ItemInstance ItemInstance { get { return itemInstance; } }

    // [SerializeField] private TextMeshProUGUI nameText;
    // [SerializeField] private TextMeshProUGUI amountText;

    public void Initialize(InventoryUI inventoryUI)
    {
        this.inventoryUI = inventoryUI;
    }

    public void SetUI(ItemInstance itemInstance)
    {
        this.itemInstance = itemInstance;
        // nameText.text = ItemInstance.ItemData.name;
        // amountText.text = ItemInstance.amount.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        inventoryUI.SelectItem(this);
    }
}
