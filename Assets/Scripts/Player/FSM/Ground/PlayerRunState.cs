using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunState : PlayerGroundState
{
    public PlayerRunState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("PlayerRunState Enter: 달려 ! ");
        ResetAllAnimationParameters();
        
        stateMachine.MovementSpeedModifier = groundData.RunSpeedModifier;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }
    
    public override void Update()
    {
        base.Update();

        if (stateMachine.MovementInput != Vector2.zero)
        {
            OnMove();
            return;
        }
        
        // 공격 키 입력 (예: 마우스 왼쪽 클릭) 감지
        // if (Input.GetMouseButtonDown(0)) // 또는 InputManager 활용
        // {
        //     Debug.Log("공격 애니메이션으로 전환");
        //     stateMachine.ChangeState(stateMachine.BaseAttackState);
        //     return;
        // }
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }
    
    protected override void OnRunCanceled(InputAction.CallbackContext context)
    {
        base.OnRunCanceled(context);
        stateMachine.IsRunning = false;

        if (stateMachine.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
        else
        {
            stateMachine.ChangeState(stateMachine.WalkState);
        }
    }
}
