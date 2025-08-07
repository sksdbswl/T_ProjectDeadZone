using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    public Vector2 MoveInput { get; private set; } // 이동 입력값을 담을 프로퍼티
    public PlayerInputActions.PlayerActions PlayerActions { get; private set; }
    public CinemachineInputProvider cinemachineInputProvider { get; private set; }

    public bool IsRunPressed { get; private set; } // Shift 상태 저장
    
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        PlayerActions = playerInputActions.Player;
    }
    
    
    private void OnEnable()
    {
        playerInputActions.Enable();

        PlayerActions.Movement.performed += OnMovementPerformed;
        PlayerActions.Movement.canceled += OnMovementCanceled;
        PlayerActions.Run.performed += OnRunPerformed;
        PlayerActions.Run.canceled += OnRunCanceled;
    }

    private void OnDisable()
    {
        PlayerActions.Movement.performed -= OnMovementPerformed;
        PlayerActions.Movement.canceled -= OnMovementCanceled;
        PlayerActions.Run.performed -= OnRunPerformed;
        PlayerActions.Run.canceled -= OnRunCanceled;

        playerInputActions.Disable();
    }

    private void OnRunPerformed(InputAction.CallbackContext context)
    {
        IsRunPressed = true;
    }

    private void OnRunCanceled(InputAction.CallbackContext context)
    {
        IsRunPressed = false;
    }

    // private void OnEnable()
    // {
    //     playerInputActions.Enable();
    //
    //     PlayerActions.Movement.performed += OnMovementPerformed;
    //     PlayerActions.Movement.canceled += OnMovementCanceled;
    // }
    //
    // private void OnDisable()
    // {
    //     PlayerActions.Movement.performed -= OnMovementPerformed;
    //     PlayerActions.Movement.canceled -= OnMovementCanceled;
    //
    //     playerInputActions.Disable();
    // }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        MoveInput = Vector2.zero;
    }
}



// public class PlayerInput : MonoBehaviour
// {
//     public PlayerInputActions InputActions { get; private set; }
//     public PlayerInputActions.PlayerActions PlayerActions { get; private set; }
//     public PlayerInputActions.UIActions UIActions { get; private set; }
//     
//     public CinemachineInputProvider cinemachineInputProvider {  get; private set; }
//     
//     private void Awake()
//     {
//         InputActions = new PlayerInputActions();
//         PlayerActions = InputActions.Player;
//         
//         cinemachineInputProvider = FindObjectOfType<CinemachineInputProvider>();
//         
//         // PlayerActions.Movement.performed += ctx => InputActions = ctx.ReadValue<Vector2>();
//         // PlayerActions.Movement.canceled += ctx => InputActions = Vector2.zero;
//         
//
//         InputActions.Player.Movement.performed += ctx =>
//         {
//             InputActions = ctx.ReadValue<Vector2>();
//         };
//
//         InputActions.Player.Movement.canceled += ctx =>
//         {
//             InputActions = Vector2.zero;
//         };
//     }
//     
//     private void OnEnable()
//     {
//         InputActions.Enable();
//     }
//
//     private void OnDisable()
//     {
//         InputActions.Disable();
//     }
// }
