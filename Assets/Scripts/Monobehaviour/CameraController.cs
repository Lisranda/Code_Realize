using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    Quaternion initRotation;
    Camera cam;


    void Awake () {
        player = transform.parent.gameObject;
        initRotation = transform.rotation;
        transform.position = player.transform.position - 15f * transform.forward;
        cam = GetComponent<Camera> ();
    }

    void LateUpdate () {
        FixCamera ();
    }

    void FixCamera () {
        transform.rotation = initRotation;
        transform.position = player.transform.position - 15f * transform.forward;
    }
}
