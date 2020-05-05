using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State {
    protected Controller_Player controller;
    protected StateMachine stateMachine;

    public State (Controller_Player controller , StateMachine stateMachine) {
        this.controller = controller;
        this.stateMachine = stateMachine;
    }

    public virtual void OnEnter () {

    }

    public virtual void OnExit () {

    }

    public virtual void HandleInput () {

    }

    public virtual void LogicUpdate () {

    }

    public virtual void PhysicsUpdate () {

    }
}
