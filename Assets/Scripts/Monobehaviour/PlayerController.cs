using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Speed { Walk , Run , Sprint }

public class PlayerController : PawnController {
    [SerializeField]
    bool isGrounded = true;

    Camera cam;
    Motor motor;
    PawnPlayer pawnPlayer;
    
    protected override void Awake () {
        cam = GetComponentInChildren<Camera> ();
        motor = GetComponent<Motor> ();
        pawnPlayer = GetComponent<PawnPlayer> ();        
    }

    void Update () {
        //FaceMouse ();
        RotationTest ();
        Movement ();
    }

    void FaceMouse () {
        Plane normalPlane = new Plane (Vector3.up , transform.position);
        Ray ray = cam.ScreenPointToRay (Input.mousePosition);
        if (normalPlane.Raycast (ray , out float hitDistance)) {
            Vector3 hitPoint = ray.GetPoint (hitDistance);

            Vector3 direction = hitPoint - transform.position;
            float currentAngle = Mathf.Atan2 (transform.forward.x , transform.forward.z) * Mathf.Rad2Deg;
            float targetAngle = Mathf.Atan2 (direction.x , direction.z) * Mathf.Rad2Deg;
            float deltaAngle = Mathf.DeltaAngle (currentAngle , targetAngle);
            Debug.Log (deltaAngle);

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

            Vector3 direction = hitPoint - transform.position;
            float currentAngle = Mathf.Atan2 (transform.forward.x , transform.forward.z) * Mathf.Rad2Deg;
            float targetAngle = Mathf.Atan2 (direction.x , direction.z) * Mathf.Rad2Deg;
            AngleRotation = Mathf.DeltaAngle (currentAngle , targetAngle);

            transform.rotation = Quaternion.Slerp (transform.rotation , targetRotation , 7f * Time.deltaTime);            
        }
    }

    void Movement () {
        if (!isGrounded)
            return;

        AxesMovement = new Vector3 (Input.GetAxisRaw ("Horizontal") , 0f , Input.GetAxisRaw ("Vertical"));
        motor.Move (AxesMovement * GetSpeed ());
    }

    float GetSpeed () {
        float speed = pawnPlayer.SpeedWalk;
        SpeedLevel = Speed.Walk;
        if (Input.GetButton ("Sprint")) {
            speed = pawnPlayer.SpeedSprint;
            SpeedLevel = Speed.Sprint;
        }
        return speed;
    }
}
