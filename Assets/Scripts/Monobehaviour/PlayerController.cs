using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speedBase = 8f;
    [SerializeField] float speedRunMultiplier = 1.5f;

    LayerMask mouseLookLayer;
    Camera cam;
    Motor motor;
    
    void Awake () {
        mouseLookLayer = LayerMask.GetMask ("MouseLook");
        cam = GetComponentInChildren<Camera> ();
        motor = GetComponent<Motor> ();
    }

    void Update () {
        FaceMouse ();
        Movement ();
    }

    void FaceMouse () {
        Ray ray = cam.ScreenPointToRay (Input.mousePosition);
        if (Physics.Raycast (ray , out RaycastHit hit , 1000f , mouseLookLayer)) {
            transform.LookAt (hit.point);
        }
    }

    void Movement () {
        Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal") , 0 , Input.GetAxisRaw ("Vertical"));
        motor.Move (input , speedBase);
    }

}
