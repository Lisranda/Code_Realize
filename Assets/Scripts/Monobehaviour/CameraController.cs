using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    Quaternion initialRotation;
    Camera cam;

    [SerializeField]
    [Range (5 , 20)]
    float cameraDistance = 15f;

    void Awake () {
        player = transform.parent.gameObject;
        initialRotation = transform.rotation;
        transform.position = player.transform.position - cameraDistance * transform.forward;
        cam = GetComponent<Camera> ();
    }

    void LateUpdate () {
        FixCamera ();
    }

    void FixCamera () {
        transform.rotation = initialRotation;
        transform.position = player.transform.position - cameraDistance * transform.forward;
    }
}
