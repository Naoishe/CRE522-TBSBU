using UnityEngine;

public class RunState : State
{
    public WalkState walkState;
    public IdleState idleState;

    public bool playerIsIdle;
    public bool playerIsWalking;
    public override State RunCurrentState()
    {
        
        if (playerIsIdle)
        {
            return idleState;
        }
        if (playerIsWalking)
        {
            return walkState;
        }
        return this;
    }
}
