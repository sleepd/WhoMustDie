using System.Collections;
using UnityEngine;

public class PlayerReloadState : PlayerFightingState
{
    float countDown;
    public PlayerReloadState(PlayerController playerController, FightStateMachine stateMachine) : base(playerController, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        countDown = player.weapon.Settings.reloadTime;
        player.weapon.Reload();
        player.animator.SetTrigger("Reload");
        player.gameHUD.ShowDotCrosshair();
        player.ActiveOrbitalCamera();
        player.animator.SetBool("Aiming", false);
    }

    public override void Update()
    {
        base.Update();
        player.RotateToInputDirection();

        countDown -= Time.deltaTime;
        if (countDown <= 0)
        {
            stateMachine.ChangeState(stateMachine.weaponIdleState);
        }
    }
}