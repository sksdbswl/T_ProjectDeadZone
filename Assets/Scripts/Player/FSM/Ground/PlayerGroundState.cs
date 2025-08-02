using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundState : PlayerBaseState
{
   public PlayerGroundState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
        AddInputActionsCallbacks(); // ← 이게 있어야 이벤트 등록됨
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
        RemoveInputActionsCallbacks(); // ← 나갈 때 제거
    }

    private void AddInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Run.started += OnRunStarted;
        stateMachine.Player.Input.PlayerActions.Run.canceled += OnRunCanceled;
    }

    private void RemoveInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Run.started -= OnRunStarted;
        stateMachine.Player.Input.PlayerActions.Run.canceled -= OnRunCanceled;
    }
    
    public override void Update()
    {
        base.Update();
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if (stateMachine.MovementInput == Vector2.zero)
            return;
    
        stateMachine.ChangeState(stateMachine.IdleState);
        stateMachine.IsRunning = false;
    
        base.OnMovementCanceled(context);
    }
    
    protected override void OnRunStarted(InputAction.CallbackContext context)
    {
        base.OnRunStarted(context);
        stateMachine.IsRunning = true;
    }
    
    protected virtual void OnRunCanceled(InputAction.CallbackContext context)
    {
        stateMachine.IsRunning = false;
    }
    
    protected virtual void OnMove()
    {
        //stateMachine.ChangeState(stateMachine.WalkState);
        
        if (!stateMachine.IsRunning)
            stateMachine.ChangeState(stateMachine.WalkState);
        else
            stateMachine.ChangeState(stateMachine.RunState);
    }
    
    protected void ResetAllAnimationParameters()
    {
        var animator = stateMachine.Player.Animator;
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
        StopAnimation(stateMachine.Player.AnimationData.WalkParameterHash);
        StopAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }

    // protected override void OnAttackStarted(InputAction.CallbackContext context)
    // {
    //     if (stateMachine.Player.CurrentWeapon == null)
    //         return;
    //
    //     AttackInfoData attackInfoData = attackData.BaseAttackInfo;
    //     if (attackInfoData == null)
    //         return;
    //
    //     ChangeAttackState(attackInfoData);
    // }
    //
    // protected override void OnSkillStarted(InputAction.CallbackContext context)
    // {
    //     if (stateMachine.Player.CurrentWeapon == null)
    //         return;
    //
    //     AttackInfoData attackInfoData = attackData.GetSkillInfo(0);
    //     if(attackInfoData == null) return;
    //
    //     ChangeAttackState(attackInfoData);
    // }
}
