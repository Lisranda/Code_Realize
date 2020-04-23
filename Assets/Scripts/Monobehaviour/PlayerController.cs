using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask mouseLookLayer;

    Camera cam;
    
    void Awake () {
        cam = GetComponentInChildren<Camera> ();
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
        transform.Translate (input * 8f * Time.deltaTime);
    }

}
