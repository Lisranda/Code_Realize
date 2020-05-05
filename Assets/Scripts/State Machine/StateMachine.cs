using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
    protected Controller controller;
    protected State currentState;

    public StateMachine (Controller controller) {
        this.controller = controller;
    }

    public void Initialize (State initialState) {
        currentState = initialState;
        currentState.OnEnter ();
    }

    public void ChangeState (State newState) {
        if (currentState != null)
            currentState.OnExit ();

        currentState = newState;

        if (currentState != null)
            currentState.OnEnter ();
    }

    public virtual void Tick () {
        currentState.HandleInput ();
        currentState.LogicUpdate ();
    }

    public virtual void FixedTick () {
        currentState.PhysicsUpdate ();
    }
}
