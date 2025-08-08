using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponIdleState : PlayerFightingState
{
    private float blendSpeed = 20f;
    public PlayerWeaponIdleState(PlayerController playerController, FightStateMachine stateMachine) : base(playerController, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.input.Player.Aim.started += Aim;
        // player.SwitchCamera();
    }

    private void Aim(InputAction.CallbackContext context)
    {
        Transform camTransform = player.orbitalCamera.transform;
        Vector3 camForward = camTransform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        if (camForward.sqrMagnitude > 0.001f)
        {
            player.RotateToDirection(camForward);
        }

        stateMachine.ChangeState(stateMachine.aimingState);
    }

    public override void Update()
    {
        base.Update();
        player.RotateToInputDirection();

        float currentWeight = player.animator.GetLayerWeight(1);
        if (currentWeight > 0)
        {
            float newWeight = Mathf.Lerp(currentWeight, 0, Time.deltaTime * blendSpeed);
            if (currentWeight < 0.01f) currentWeight = 0;
            player.animator.SetLayerWeight(1, newWeight);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.input.Player.Aim.started -= Aim;
    }
}