using UnityEngine;
public class PlayerRuningState : PlayerGroundedState
{
    public PlayerRuningState(PlayerController playerController, MovementStateMachine stateMachine) : base(playerController, stateMachine)
    {
    }

    public override void Update()
    {
        base.Update();
        Vector2 moveInput = player.input.Player.Move.ReadValue<Vector2>();
        player.animator.SetFloat("Input", moveInput.magnitude);
        player.animator.SetFloat("InputX", moveInput.x);
        player.animator.SetFloat("InputY", moveInput.y);
        if (moveInput.magnitude < 0.2f)
        {
            stateMachine.ChangeState(stateMachine.playerIdleState);
        }
    }
}