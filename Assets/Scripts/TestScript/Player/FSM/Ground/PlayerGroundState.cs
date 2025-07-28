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
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if(!stateMachine.Player.Controller.isGrounded
            && stateMachine.Player.Controller.velocity.y < Physics.gravity.y * Time.fixedDeltaTime)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if (stateMachine.MovementInput == Vector2.zero)
            return;

        stateMachine.ChangeState(stateMachine.IdleState);
        stateMachine.IsRunning = false;

        base.OnMovementCanceled(context);
    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        // base.OnJumpStarted(context);
        stateMachine.ChangeState(stateMachine.JumpState);
    }

    protected override void OnRunStarted(InputAction.CallbackContext context)
    {
        base.OnRunStarted(context);
        stateMachine.IsRunning = true;
    }

    protected override void OnAttackStarted(InputAction.CallbackContext context)
    {
        if (stateMachine.Player.CurrentWeapon == null)
            return;

        AttackInfoData attackInfoData = attackData.BaseAttackInfo;
        if (attackInfoData == null)
            return;

        ChangeAttackState(attackInfoData);
    }

    protected override void OnSkillStarted(InputAction.CallbackContext context)
    {
        if (stateMachine.Player.CurrentWeapon == null)
            return;

        AttackInfoData attackInfoData = attackData.GetSkillInfo(0);
        if(attackInfoData == null) return;

        ChangeAttackState(attackInfoData);
    }


    protected virtual void OnMove()
    {
        if (!stateMachine.IsRunning)
            stateMachine.ChangeState(stateMachine.WalkState);
        else
            stateMachine.ChangeState(stateMachine.RunState);
    }


}
