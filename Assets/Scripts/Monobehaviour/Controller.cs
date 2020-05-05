using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    protected StateMachine stateMachine;
    protected Rigidbody rb;

    protected Pawn pawn;
    public Pawn Pawn => pawn;

    public Animator animator;    

    protected virtual void Awake () {
        pawn = GetComponent<Pawn> ();
        rb = GetComponent<Rigidbody> ();
    }

    protected virtual void Start () {

    }

    protected virtual void Update () {
        if (stateMachine == null) {
            Debug.LogError ("StateMachine is Null");
            return;
        }
        stateMachine.Tick ();
    }

    protected virtual void FixedUpdate () {
        if (stateMachine == null) {
            Debug.LogError ("StateMachine is Null");
            return;
        }
        stateMachine.FixedTick ();
    }

    public abstract void Move (Vector3 movementDirection);
}
