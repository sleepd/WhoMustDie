public class PlayerStateMachine : StateMachine
{
    protected PlayerController playerController;
    protected PlayerStateMachine(PlayerController playerController)
    {
        this.playerController = playerController;
    }
}