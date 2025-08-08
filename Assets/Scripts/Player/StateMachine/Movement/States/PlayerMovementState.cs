public class PlayerMovementState : PlayerState
{
    protected MovementStateMachine stateMachine;
    public PlayerMovementState(PlayerController playerController, MovementStateMachine stateMachine) : base(playerController)
    {
        this.stateMachine = stateMachine;
    }
}