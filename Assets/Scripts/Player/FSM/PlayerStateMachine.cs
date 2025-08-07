using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public bool IsRunning { get; set; } = false;
    public bool IsSprinting { get; set; } = false;
    
    public bool IsAttacking { get; set; }
    public AttackInfoData CurrentAttackInfo { get; set; }

    public Transform MainCameraTransform { get; set; } 

    public PlayerIdleState IdleState { get; }
    public PlayerWalkState WalkState { get; }
    public PlayerRunState RunState { get; }

    public BaseAttackState BaseAttackState { get; }

    public PlayerStateMachine(Player player)
    {
        Player = player;
        MainCameraTransform = Camera.main.transform;

        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);

        BaseAttackState = new BaseAttackState(this);
    }

    public void TakeDamage()
    {
        // if(currentState == IdleState||
        //     currentState == WalkState||
        //     currentState == RunState)
        // {
        //     Player.Animator.SetTrigger(Player.AnimationData.HitParameterHash);
        // }
    }

    public void Die()
    {
        Player.Animator.SetTrigger(Player.AnimationData.DieParameterHash);
        currentState = null;
    }







}

