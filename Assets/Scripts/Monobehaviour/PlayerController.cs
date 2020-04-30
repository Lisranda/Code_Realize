using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : PawnController {
    Camera cam;
    PawnPlayer pawnPlayer;
    Rigidbody rb;
    Animator animator;
    CapsuleCollider collide;

    [Header ("Smooth Times")]
    [SerializeField]
    [Range (0 , 1)]
    float locomotionSmooth = .1f;

    Quaternion targetRotation = Quaternion.identity;

    Vector3 movementDirection;

    float Speed {
        get {
            if (isSprinting)
                return pawnPlayer.SpeedSprint;
            if (movementDirection.z < 0)
                return pawnPlayer.SpeedWalk;
            return pawnPlayer.SpeedRun;
        }
    }
    float rollingSpeed = 0f;

    bool isGrounded = true;
    bool isSprinting = false;
    bool jump = false;

    bool isRolling = false;
    bool roll = false;

    void Awake () {
        cam = GetComponentInChildren<Camera> ();
        pawnPlayer = GetComponent<PawnPlayer> ();
        rb = GetComponent<Rigidbody> ();
        animator = GetComponentInChildren<Animator> ();
        collide = GetComponent<CapsuleCollider> ();
    }

    void Update () {
        Grounded ();
        FaceMouseInput ();
        MovementInput ();
        JumpInput ();
        RollInput ();
    }

    void FixedUpdate () {
        FaceMousePhysics ();
        MovementPhysics ();
        JumpPhysics ();
        RollPhysics ();
    }

    void FaceMouseInput () {
        if (!isGrounded || isRolling)
            return;
        Plane normalPlane = new Plane (Vector3.up , rb.position);
        Ray ray = cam.ScreenPointToRay (Input.mousePosition);
        if (normalPlane.Raycast (ray , out float hitDistance)) {
            Vector3 hitPoint = ray.GetPoint (hitDistance);
            targetRotation = Quaternion.LookRotation (hitPoint - rb.position);
            targetRotation.x = 0;
            targetRotation.z = 0;            
        }
    }

    void FaceMousePhysics () {
        rb.rotation = Quaternion.Slerp (rb.rotation , targetRotation , 7f * Time.fixedDeltaTime);
    }

    void MovementInput () {
        if (!isGrounded || isRolling)
            return;
        movementDirection = new Vector3 (Input.GetAxisRaw ("Horizontal") , 0f , Input.GetAxisRaw ("Vertical"));
        animator.SetFloat ("axisHorizontal" , movementDirection.x , locomotionSmooth , Time.deltaTime);
        animator.SetFloat ("axisVertical" , movementDirection.z , locomotionSmooth , Time.deltaTime);

        isSprinting = false;
        if (Input.GetButton ("Sprint") && movementDirection.z == 1 && movementDirection.x == 0) {
            isSprinting = true;
            animator.SetFloat ("axisVertical" , movementDirection.z * 2f , locomotionSmooth , Time.deltaTime);
        }
    }

    void MovementPhysics () {
        Vector3 velocity = transform.TransformDirection (movementDirection) * Speed;
        rb.MovePosition (rb.position + velocity * Time.fixedDeltaTime);
    }

    void JumpInput () {
        if (!isGrounded || isRolling)
            return;
        if (Input.GetButtonDown ("Jump")) {
            jump = true;            
        }
    }

    void JumpPhysics () {
        if (!jump)
            return;
        rb.AddForce (Vector3.up * pawnPlayer.JumpStrength , ForceMode.Impulse);
        animator.SetTrigger ("jump");
        jump = false;
    }

    void RollInput () {
        if (!isGrounded || isRolling)
            return;
        if (Input.GetButtonDown ("Roll")) {
            roll = true;
            isRolling = true;
            rollingSpeed = pawnPlayer.SpeedRoll;
        }
    }

    void RollPhysics () {
        if (!roll && !isRolling)
            return;
        if (movementDirection == Vector3.zero)
            movementDirection = Vector3.forward;
        Vector3 velocity = transform.TransformDirection (movementDirection) * rollingSpeed;
        rb.MovePosition (rb.position + velocity * Time.fixedDeltaTime);
        rollingSpeed -= rollingSpeed * 5f * Time.fixedDeltaTime;
        if (rollingSpeed < 2f) {
            isRolling = false;
            animator.SetBool ("isRolling" , false);
        }

        if (roll) {
            animator.SetTrigger ("roll");
            animator.SetBool ("isRolling" , true);
            roll = false;
        }
    }
    
    void Grounded () {
        Ray ray = new Ray (rb.position + Vector3.up , Vector3.down);
        isGrounded = Physics.SphereCast (ray , collide.radius , 1.1f);
        animator.SetBool ("isGrounded" , isGrounded);
    }

}
