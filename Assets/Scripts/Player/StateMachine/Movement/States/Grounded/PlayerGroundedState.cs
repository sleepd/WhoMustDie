public class PlayerGroundedState : PlayerMovementState
{
    public PlayerGroundedState(PlayerController playerController, MovementStateMachine stateMachine) : base(playerController, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        player.velocity.y = -1f;
    }
}