using UnityEngine;

public class IdleState : State
{
    public WalkState walkState;
    public RunState runState;

    public bool playerIsRunning;
    public bool playerIsWalking;
    public override State RunCurrentState()
    {
        if (playerIsRunning)
        {
            return runState;
        }
        if(playerIsWalking)
        {
            return walkState;
        }
        return this;
    }
}
