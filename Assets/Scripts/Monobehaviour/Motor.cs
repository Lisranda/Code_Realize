using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    CharacterController controller;

    void Awake () {
        controller = GetComponent<CharacterController> ();
    }

    public void Move (Vector3 velocity) {
        controller.Move (transform.TransformDirection (velocity) * Time.deltaTime);
    }
}
