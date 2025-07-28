using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    UIManager uIManager;
    Player player;

    List<ItemSlotUI> itemSlots = new List<ItemSlotUI>();

    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] Transform contentRoot;

    [SerializeField] Button equipButton;
    [SerializeField] Button useButton;
    [SerializeField] Button dropButton;

    ItemSlotUI selectedItem;


    public void InitializeUI(GameManager gameManager, UIManager uIManager)
    {
        this.uIManager = uIManager;
        player = gameManager.Player;

        gameObject.SetActive(false);

        equipButton.onClick.AddListener(OnEquipButton);
        useButton.onClick.AddListener(OnUseButton);
        dropButton.onClick.AddListener(OnDropButton);
    }

    public void Open()
    {
        selectedItem = null;
        UpdateButtons(null);
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void AddItemSlot(ItemInstance item)
    {
        GameObject go = Instantiate(itemSlotPrefab, contentRoot);
        ItemSlotUI slot = go.GetComponent<ItemSlotUI>();
        itemSlots.Add(slot);

        slot.Initialize(this);
        slot.SetUI(item);
    }

    public void UpdateItemSlot(ItemInstance item)
    {
        ItemSlotUI slot = itemSlots.First(slot => slot.ItemInstance == item);

        if (slot == null) return;

        slot.SetUI(item);
    }

    public void SelectItem(ItemSlotUI slot)
    {
        selectedItem = slot;
        UpdateButtons(slot.ItemInstance);
    }
    public void UpdateButtons(ItemInstance item)
    {
        if (item == null)
        {
            useButton.interactable = false;
            equipButton.interactable = false;
            dropButton.interactable = false;
            return;
        }

        switch (item.ItemData.type)
        {
            case ItemType.Consumable:
                useButton.interactable = true;
                equipButton.interactable = false;
                break;
            case ItemType.Equipable:
                useButton.interactable = false;
                equipButton.interactable = true;
                break;
        }
        dropButton.interactable = true;
    }



    public void OnUseButton()
    {
        if (selectedItem == null)
            return;

        ItemData itemData = selectedItem.ItemInstance.ItemData;

        for (int i = 0; i < itemData.consumables.Length; i++)
        {
            switch (itemData.consumables[i].type)
            {
                case ConsumableType.Health:
                    player.HealthSystem.TakeDamage(-itemData.consumables[i].value); break;
            }
        }

        if(selectedItem.ItemInstance.Use() == false)
        {
            itemSlots.Remove(selectedItem);
            Destroy(selectedItem.gameObject);
            selectedItem = null;
        }
        else
        {
            selectedItem.SetUI(selectedItem.ItemInstance);
        }
    }

    public void OnEquipButton()
    {
        if(selectedItem == null) return;

        player.EquipItem(selectedItem.ItemInstance);
    }
    public void OnDropButton()
    {
        if (selectedItem == null) return;

        if (selectedItem.ItemInstance.equipped)
            return;

        Drop(selectedItem.ItemInstance);

        itemSlots.Remove(selectedItem);
        player.Inventory.RemoveItem(selectedItem.ItemInstance);
        Destroy(selectedItem.gameObject);
    }

    void Drop(ItemInstance itemInstance)
    {
        ItemData itemData = itemInstance.ItemData;
        Vector3 dropPosition = player.transform.position + player.transform.forward * 1.5f + player.transform.up * 1.5f;

        GameObject go = Instantiate(itemData.dropPrefab, dropPosition, Quaternion.identity);
        Rigidbody rigidbody = go.GetComponent<Rigidbody>();
        rigidbody.AddForce(player.transform.forward * 2, ForceMode.Impulse);

        ItemObject itemObject = go.GetComponent<ItemObject>();
        itemObject.amount = itemInstance.amount;
    }













}
