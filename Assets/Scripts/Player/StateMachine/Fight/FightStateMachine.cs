public class FightStateMachine : PlayerStateMachine
{
    public PlayerWeaponIdleState weaponIdleState  { get; private set; }
    public PlayerAimingState aimingState  { get; private set; }
    public PlayerFireState fireState  { get; private set; }
    public FightStateMachine(PlayerController playerController) : base(playerController)
    {
        weaponIdleState = new(playerController, this);
        aimingState = new(playerController, this);
        fireState = new(playerController, this);
    }
}