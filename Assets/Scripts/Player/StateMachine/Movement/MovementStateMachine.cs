public class MovementStateMachine : PlayerStateMachine
{
    public PlayerIdleState playerIdleState { get; private set; }
    public PlayerRuningState playerRuningState { get; private set; }
    public MovementStateMachine(PlayerController playerController) : base(playerController)
    {
        playerIdleState = new(playerController, this);
        playerRuningState = new(playerController, this);
    }
}