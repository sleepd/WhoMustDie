using UnityEngine;
public class PlayerRuningState : PlayerGroundedState
{
    private float dampTime = 0.1f;
    public PlayerRuningState(PlayerController playerController, MovementStateMachine stateMachine) : base(playerController, stateMachine)
    {
    }

    public override void Update()
    {
        base.Update();
        Vector2 moveInput = player.input.Player.Move.ReadValue<Vector2>();
        player.animator.SetFloat("Input", moveInput.magnitude);
        player.animator.SetFloat("InputX", moveInput.x, dampTime, Time.deltaTime);
        player.animator.SetFloat("InputY", moveInput.y, dampTime, Time.deltaTime);
        if (moveInput.magnitude < 0.2f)
        {
            stateMachine.ChangeState(stateMachine.playerIdleState);
        }
    }
}