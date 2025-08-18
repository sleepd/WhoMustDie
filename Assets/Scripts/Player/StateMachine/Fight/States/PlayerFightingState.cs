using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFightingState : PlayerState
{
    protected FightStateMachine stateMachine;
    public PlayerFightingState(PlayerController playerController, FightStateMachine stateMachine) : base(playerController)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    protected void Reload(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.reloadState);
    }

}