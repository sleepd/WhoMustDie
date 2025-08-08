public class PlayerFightingState : PlayerState
{
    protected FightStateMachine stateMachine;
    public PlayerFightingState(PlayerController playerController, FightStateMachine stateMachine) : base(playerController)
    {
        this.stateMachine = stateMachine;
    }
}