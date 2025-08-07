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
        Vector3 movementDirection = GetMovementDirection();
        Rotate(movementDirection);
        Move(movementDirection);
    }

    private void Move(Vector3 movementDirection)
    {
        float movementSpeed = GetMovementSpeed();
        
        stateMachine.Player.Controller.Move(
            (
                (movementDirection * movementSpeed)
                + stateMachine.Player.ForceHandler.Movement
            )
            * Time.deltaTime
        );
    }
    
    // private void Move(Vector3 movementDirection)
    // {
    //     Debug.Log($"movementDirection:: 움직여라 !@");
    //     Debug.Log($"movementDirection: {movementDirection}, speed: {GetMovementSpeed()}, grounded: {stateMachine.Player.Controller.isGrounded}");
    //     
    //     float movementSpeed = GetMovementSpeed();
    //
    //     stateMachine.Player.Controller.Move(
    //         movementDirection * (movementSpeed * Time.deltaTime)
    //     );
    // }
    
    private Vector3 GetMovementDirection()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return (forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x).normalized;
    }

    private void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Transform playerTransform = stateMachine.Player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
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

    protected void ForceMove()
    {
        // stateMachine.Player.Controller.Move(
        //     (
        //         stateMachine.Player.ForceHandler.Movement
        //     )
        //     * Time.deltaTime
        // );
    }

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
