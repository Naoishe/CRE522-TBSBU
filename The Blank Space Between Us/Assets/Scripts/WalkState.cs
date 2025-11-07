using UnityEngine;

public class WalkState : State
{
    public IdleState idleState;
    public RunState runState;

    public bool playerIsRunning;
    public bool playerIsIdle;
    public override State RunCurrentState()
    {
        if (playerIsRunning)
        {
            return runState;
        }
        if (playerIsIdle)
        {
            return idleState;
        }
        return this;
    }
}
