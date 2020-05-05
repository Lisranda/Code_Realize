using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Player : Controller
{
    public State_Grounded_Running stateRunning;
    public State_Grounded_Sprinting stateSprinting;
    
    Camera cam;

    protected override void Awake () {
        base.Awake ();
        cam = GetComponentInChildren<Camera> ();
        stateMachine = new SM_Player (this);
        stateRunning = new State_Grounded_Running (this , stateMachine , rb , cam);
        stateSprinting = new State_Grounded_Sprinting (this , stateMachine , rb , cam);
        animator = GetComponentInChildren<Animator> ();
    }

    protected override void Start () {
        base.Start ();
        stateMachine.Initialize (stateRunning);
    }

    public override void Move (Vector3 velocity) {
        velocity = transform.TransformDirection (velocity);
        rb.MovePosition (rb.position + velocity * Time.fixedDeltaTime);        
    }

}
