using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly GroundData groundData;
    //protected readonly AttackData attackData;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.groundData = stateMachine.Player.Data.GroundData;
       //this.attackData = stateMachine.Player.Data.AttackData;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void Update()
    {
        HandleInput();  
        Move();
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }
    
    private void Move()
    {
        // Vector3 movementDirection = GetMovementDirection();
        // Rotate(movementDirection);
        // Move(movementDirection);
        UpdateMovement();
        UpdateCameraRotation();
    }
    
    private Vector3 velocity;
    public float gravity = -9.81f;
    
    private void UpdateMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        float movementSpeed = GetMovementSpeed();
        
        Vector3 move = stateMachine.Player.transform.right * moveX + stateMachine.Player.transform.forward * moveZ;
        stateMachine.Player.Controller.Move(move * (movementSpeed * Time.deltaTime));
    
        velocity.y += gravity * Time.deltaTime;
        stateMachine.Player.Controller.Move(velocity * Time.deltaTime);
    }
    
    [Header("Camera Settings")]
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 80f;
    private float verticalLookRotation = 0f;
    private Quaternion initialCameraRotation;
    
    private void UpdateCameraRotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
    
        Quaternion yawRotation = Quaternion.AngleAxis(mouseX, Vector3.up);
        stateMachine.Player.transform.rotation *= yawRotation;
    
        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -maxLookAngle, maxLookAngle);
    
        Quaternion pitchRotation = Quaternion.AngleAxis(verticalLookRotation, Vector3.right);
        stateMachine.MainCameraTransform.localRotation = initialCameraRotation * pitchRotation;
    }

    // private void Move(Vector3 movementDirection)
    // {
    //     float movementSpeed = GetMovementSpeed();
    //     
    //     stateMachine.Player.Controller.Move(
    //         (
    //             (movementDirection * movementSpeed)
    //             + stateMachine.Player.ForceHandler.Movement
    //         )
    //         * Time.deltaTime
    //     );
    // }
    //
    // private Vector3 GetMovementDirection()
    // {
    //     Vector3 forward = stateMachine.MainCameraTransform.forward;
    //     Vector3 right = stateMachine.MainCameraTransform.right;
    //
    //     forward.y = 0;
    //     right.y = 0;
    //
    //     forward.Normalize();
    //     right.Normalize();
    //
    //     return (forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x).normalized;
    // }
    //
    // private void Rotate(Vector3 movementDirection)
    // {
    //     if (movementDirection.sqrMagnitude > 0.001f) // 거의 0이면 회전 안 함
    //     {
    //         Transform playerTransform = stateMachine.Player.transform;
    //         Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
    //         playerTransform.rotation = Quaternion.Slerp(
    //             playerTransform.rotation,
    //             targetRotation,
    //             stateMachine.RotationDamping * Time.deltaTime
    //         );
    //     }
    // }

    private float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }
    
    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {
    }
    
    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {
        Debug.Log("OnRunStarted");
        StartAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }
    
    private void AddInputActionsCallbacks()
    {
    }

    private void RemoveInputActionsCallbacks()
    {
    }

    protected virtual void OnRunCanceled(InputAction.CallbackContext context)
    {
        stateMachine.IsRunning = false;
    }
}
