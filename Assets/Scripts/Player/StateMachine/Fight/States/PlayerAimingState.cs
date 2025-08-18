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
        player.input.Player.Reload.performed += Reload;
        player.input.Player.Attack.started += Fire;

        player.gameHUD.ShowCrosshair();
        player.ActiveAimingCamera();
        player.animator.SetBool("Aiming", true);
    }



    public override void Exit()
    {
        base.Exit();
        player.input.Player.Reload.performed -= Reload;
    }

    public override void Update()
    {
        base.Update();
        // float currentWeight = player.animator.GetLayerWeight(1);
        // if (currentWeight < 1)
        // {
        //     float newWeight = Mathf.Lerp(currentWeight, 1, Time.deltaTime * blendSpeed);
        //     player.animator.SetLayerWeight(1, newWeight);
        // }
        player.AlignToCameraY();

        if (!player.input.Player.Aim.IsInProgress())
        {
            stateMachine.ChangeState(stateMachine.weaponIdleState);
        }
    }

    void Fire(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.fireState);
    }
}