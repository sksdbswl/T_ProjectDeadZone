using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerGroundState
{
    public PlayerWalkState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        ResetAllAnimationParameters();
        
        Debug.Log("PlayerWalkState Enter : 걸어 제발 ");
        stateMachine.MovementSpeedModifier = groundData.WalkSpeedModifier;
        StartAnimation(stateMachine.Player.AnimationData.WalkParameterHash);
        
        base.Enter();

        
    }

    public override void Update()
    {
        if (stateMachine.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return; // 상태 전환 후 더 이상 실행하지 않음
        }

        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.WalkParameterHash);
    }

    // protected override void OnRunStarted(InputAction.CallbackContext context)
    // {
    //     base.OnRunStarted(context);
    //     stateMachine.ChangeState(stateMachine.RunState);
    // }
}
