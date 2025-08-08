using UnityEngine;
public class PlayerState : IState
{
    protected PlayerController player;
    public PlayerState(PlayerController playerController)
    {
        player = playerController;
    }
    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {

    }
}