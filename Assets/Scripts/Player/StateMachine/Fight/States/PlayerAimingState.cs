using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimingState : PlayerFightingState
{
    private float blendSpeed = 20f;
    public PlayerAimingState(PlayerController playerController, FightStateMachine stateMachine) : base(playerController, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.input.Player.Aim.canceled += StopAim;
    }

    private void StopAim(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.weaponIdleState);
    }

    public override void Exit()
    {
        base.Exit();
        player.input.Player.Aim.canceled -= StopAim;
    }

    public override void Update()
    {
        base.Update();
        float currentWeight = player.animator.GetLayerWeight(1);
        if (currentWeight < 1)
        {
            float newWeight = Mathf.Lerp(currentWeight, 1, Time.deltaTime * blendSpeed);
            player.animator.SetLayerWeight(1, newWeight);
        }
        
    }
}