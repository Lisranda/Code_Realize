using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State_Grounded : State
{
    public State_Grounded (Controller_Player controller , StateMachine stateMachine , Rigidbody rb , Camera cam) 
        : base(controller , stateMachine) {
        this.controller = controller;
        this.stateMachine = stateMachine;
        this.rb = rb;
        this.cam = cam;
    }

    protected Vector3 movementDirection;
    protected bool sprint;
    protected float speed;

    Rigidbody rb;
    Camera cam;
    Quaternion targetRotation = Quaternion.identity;

    public override void OnEnter () {
        base.OnEnter ();
    }

    public override void OnExit () {
        base.OnExit ();
    }

    public override void HandleInput () {
        base.HandleInput ();

        Plane normalPlane = new Plane (Vector3.up , rb.position);
        Ray ray = cam.ScreenPointToRay (Input.mousePosition);
        if (normalPlane.Raycast (ray , out float hitDistance)) {
            Vector3 hitPoint = ray.GetPoint (hitDistance);
            targetRotation = Quaternion.LookRotation (hitPoint - rb.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
        }

        sprint = Input.GetButton ("Sprint");
    }

    public override void LogicUpdate () {
        base.LogicUpdate ();
    }

    public override void PhysicsUpdate () {
        base.PhysicsUpdate ();
        rb.rotation = Quaternion.Slerp (rb.rotation , targetRotation , 7f * Time.fixedDeltaTime);        
    }
}
