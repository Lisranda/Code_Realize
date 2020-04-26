using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Speed { Walk , Run , Sprint }

public class PlayerController : PawnController {
    Camera cam;
    CharacterController controller;
    PawnPlayer pawnPlayer;
    Level level;

    bool isGrounded = true;
    
    protected override void Awake () {
        cam = GetComponentInChildren<Camera> ();
        controller = GetComponent<CharacterController> ();
        pawnPlayer = GetComponent<PawnPlayer> ();
    }

    void OnEnable () {
        level = GetComponentInParent<Level> ();
    }

    void Update () {
        FaceMouse ();
        Movement ();
    }

    void FaceMouse () {
        Plane normalPlane = new Plane (Vector3.up , transform.position);
        Ray ray = cam.ScreenPointToRay (Input.mousePosition);
        if (normalPlane.Raycast (ray , out float hitDistance)) {
            Vector3 hitPoint = ray.GetPoint (hitDistance);
            transform.LookAt (hitPoint);
        }
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

            transform.rotation = Quaternion.Slerp (transform.rotation , targetRotation , 7f * Time.deltaTime);            
        }
    }

    void Movement () { 
        if (isGrounded) {
            AxesMovement = new Vector3 (Input.GetAxisRaw ("Horizontal") , 0f , Input.GetAxisRaw ("Vertical"));
            velocity = transform.TransformDirection (AxesMovement) * GetSpeed (AxesMovement);          

            if (Input.GetButtonDown ("Jump")) {
                velocity.y = pawnPlayer.JumpStrength;
            }
        }
        velocity.y -= level.Gravity * pawnPlayer.GravityModifier * Time.deltaTime;        
        isGrounded = (controller.Move (velocity * Time.deltaTime) & CollisionFlags.Below) != 0;
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
