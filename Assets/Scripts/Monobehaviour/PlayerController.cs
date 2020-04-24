using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
    [SerializeField] float speedMaximum = 12f;

    //[SerializeField] float speedModifierReverse = 1 / 3f;
    //[SerializeField] float speedModifierStrafe = 1 / 2f;
    [SerializeField] float speedModifierWalk = 2 / 3f;

    [SerializeField] float speedBase = 8f;
    
    float currentSpeed = 0f;

    LayerMask mouseLookLayer;
    Camera cam;
    Motor motor;
    Vector3 velocity = Vector3.zero;
    
    void Awake () {
        mouseLookLayer = LayerMask.GetMask ("MouseLook");
        cam = GetComponentInChildren<Camera> ();
        motor = GetComponent<Motor> ();
    }

    void Update () { 
        FaceMouse ();
        //Movement ();
        MovementTest ();
    }

    public Vector3 Velocity () {
        return velocity;
    }

    public float SpeedMaximum () {
        return speedMaximum;
    }

    void FaceMouse () {
        Plane normalPlane = new Plane (transform.up , transform.position);
        Ray ray = cam.ScreenPointToRay (Input.mousePosition);
        if (normalPlane.Raycast (ray , out float hitDistance)) {
            Vector3 hitPoint = ray.GetPoint (hitDistance);
            transform.LookAt (hitPoint);
        }
    }

    void Movement () {
        Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal") , 0 , Input.GetAxisRaw ("Vertical"));
        motor.Move (input , speedBase);
    }

    void MovementTest () {
        velocity = new Vector3 (Input.GetAxisRaw ("Horizontal") , 0 , Input.GetAxisRaw ("Vertical"));

        if (velocity == Vector3.zero)
            return;

        if (Input.GetButton ("Sprint"))
            velocity *= speedMaximum;
        else
            velocity *= speedMaximum * speedModifierWalk;

        Debug.Log (velocity.magnitude);
        motor.Move (velocity);
    }

}
