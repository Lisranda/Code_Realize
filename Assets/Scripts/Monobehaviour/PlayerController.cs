using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Speed { Walk , Run , Sprint }

public class PlayerController : PawnController {
    Camera cam;
    Motor motor;
    PawnPlayer pawnPlayer;
    Rigidbody rb;

    bool isGrounded = true;
    float jumpForce = 0f;
    Quaternion targetRotation = Quaternion.identity;
    
    protected override void Awake () {
        cam = GetComponentInChildren<Camera> ();
        motor = GetComponent<Motor> ();
        pawnPlayer = GetComponent<PawnPlayer> ();
        rb = GetComponent<Rigidbody> ();
    }

    void Update () {
        CheckGrounding ();
        FaceMouseInput ();
        MovementInput ();
        JumpInput ();
    }

    void FixedUpdate () {
        FaceMousePhysics ();
        MovementPhysics ();
        JumpPhysics ();
    }

    void FaceMouseInput () {
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

    void RotationTest () {
        Plane normalPlane = new Plane (Vector3.up , transform.position);
        Ray ray = cam.ScreenPointToRay (Input.mousePosition);
        if (normalPlane.Raycast (ray , out float hitDistance)) {
            Vector3 hitPoint = ray.GetPoint (hitDistance);
            Quaternion targetRotation = Quaternion.LookRotation (hitPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;

            //Vector3 direction = hitPoint - transform.position;
            //float currentAngle = Mathf.Atan2 (transform.forward.x , transform.forward.z) * Mathf.Rad2Deg;
            //float targetAngle = Mathf.Atan2 (direction.x , direction.z) * Mathf.Rad2Deg;
            //AngleRotation = Mathf.DeltaAngle (currentAngle , targetAngle);

            rb.rotation = Quaternion.Slerp (transform.rotation , targetRotation , 7f * Time.deltaTime);            
        }
    }

    void MovementInput () {
        if (!isGrounded)
            return;
        AxesMovement = new Vector3 (Input.GetAxisRaw ("Horizontal") , 0f , Input.GetAxisRaw ("Vertical"));
        velocity = transform.TransformDirection (AxesMovement) * GetSpeed (AxesMovement);
    }

    void MovementPhysics () {
        rb.MovePosition (rb.position + velocity * Time.fixedDeltaTime);
    }

    void JumpInput () {
        if (!isGrounded)
            return;
        if (Input.GetButtonDown ("Jump")) {
            jumpForce = pawnPlayer.JumpStrength;
        }
    }

    void JumpPhysics () {
        if (jumpForce == 0)
            return;
        rb.AddForce (0f , jumpForce , 0f , ForceMode.VelocityChange);
        jumpForce = 0;
        isGrounded = false;
    }

    void CheckGrounding () {
        isGrounded = Physics.Raycast (rb.position + Vector3.up , Vector3.down , 1.25f) ? true : false;
    }

    float GetSpeed (Vector3 MovementVector) {
        if (MovementVector.z < 0) {
            SpeedLevel = Speed.Walk;
            return pawnPlayer.SpeedWalk;
        }

        if (MovementVector.x != 0) {
            SpeedLevel = Speed.Run;
            return pawnPlayer.SpeedRun;
        }

        if (Input.GetButton ("Sprint")) {
            SpeedLevel = Speed.Sprint;
            return pawnPlayer.SpeedSprint;
        }

        SpeedLevel = Speed.Run;
        return pawnPlayer.SpeedRun;
    }
}
