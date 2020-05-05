using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Grounded_Sprinting : State_Grounded
{
    public State_Grounded_Sprinting (Controller_Player controller , StateMachine stateMachine , Rigidbody rb , Camera cam)
        : base (controller , stateMachine , rb , cam) {
        this.controller = controller;
        this.stateMachine = stateMachine;
    }

    public override void OnEnter () {
        base.OnEnter ();
        speed = controller.Pawn.SpeedSprint;
    }

    public override void OnExit () {
        base.OnExit ();
    }

    public override void HandleInput () {
        base.HandleInput ();

        movementDirection = new Vector3 (Input.GetAxisRaw ("Horizontal") , 0f , Input.GetAxisRaw ("Vertical"));
    }

    public override void LogicUpdate () {
        base.LogicUpdate ();
        if (!sprint)
            stateMachine.ChangeState (controller.stateRunning);
    }

    public override void PhysicsUpdate () {
        base.PhysicsUpdate ();

        controller.Move (movementDirection * speed);
        controller.animator.SetFloat ("axisHorizontal" , movementDirection.x);
        controller.animator.SetFloat ("axisVertical" , movementDirection.z * 2);
    }
}
