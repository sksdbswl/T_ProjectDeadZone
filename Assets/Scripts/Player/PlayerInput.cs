using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputActions InputActions { get; private set; }
    public PlayerInputActions.PlayerActions PlayerActions { get; private set; }
    public PlayerInputActions.UIActions UIActions { get; private set; }

    // GameManager gameManager;
    // UIManager uiManager;

    public CinemachineInputProvider cinemachineInputProvider {  get; private set; }
    
    private void Awake()
    {
        InputActions = new PlayerInputActions();
        PlayerActions = InputActions.Player;

        //UIActions = InputActions.UI;
        //UIActions.Inventory.performed += OnInventory;
        //cinemachineInputProvider = FindObjectOfType<CinemachineInputProvider>();
    }

    private void Start()
    {
        //gameManager = GameManager.Instance;
        //uiManager = gameManager.UIManager;
    }

    private void Update()
    {
        //Debug.Log($"InputActions.Player.Movement.ReadValue<Vector2>():: {InputActions.Player.Movement.ReadValue<Vector2>()}");
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }

    // private void OnInventory(InputAction.CallbackContext context)
    // {
    //     bool active = uiManager.OpenInvetoryUI();
    //     if (active)
    //     {
    //         PlayerActions.Disable();
    //         cinemachineInputProvider.enabled = false;
    //         Cursor.lockState = CursorLockMode.None;
    //     }
    //     else
    //     {
    //         PlayerActions.Enable();
    //         cinemachineInputProvider.enabled = true;
    //         Cursor.lockState = CursorLockMode.Locked;
    //     }
    // }

}
