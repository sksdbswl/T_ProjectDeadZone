using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    { }

    public override void Enter()
    {
        Debug.Log("Player Idle State");
        
        base.Enter();
        //stateMachine.MovementSpeedModifier = 0f;
        StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
        
        //base.HandleInput();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
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
        if (Input.GetMouseButtonDown(0)) // 또는 InputManager 활용
        {
            Debug.Log("공격 애니메이션으로 전환");
            stateMachine.ChangeState(stateMachine.BaseAttackState);
            return;
        }
    }
}
