using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;
    InventoryUI inventoryUI;

    public InventoryUI InventoryUI { get { return inventoryUI; } }

    public void InitializeUI(GameManager gameManager)
    {
        this.gameManager = gameManager;
        inventoryUI = transform.GetComponentInChildren<InventoryUI>();
        inventoryUI.InitializeUI(gameManager, this);
    }

    public bool OpenInvetoryUI()
    {
        inventoryUI.Open();
        return inventoryUI.gameObject.activeSelf;
    }
}
