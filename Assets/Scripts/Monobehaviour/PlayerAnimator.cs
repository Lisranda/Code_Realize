using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    //Animator animator;
    //PlayerController controller;

    //[Header ("Smooth Times")]
    //[SerializeField]
    //[Range (0 , 1)]
    //float locomotionSmooth = .1f;

    void Awake () {
        //animator = GetComponentInChildren<Animator> ();
        //controller = GetComponent<PlayerController> ();
    }

    void Update () {
        //AnimateMovement ();
    }

    void AnimateMovement () {
        //animator.SetFloat ("axesHorizontal" , controller.AxesMovement.x * sprintMultiplier , locomotionSmooth , Time.deltaTime);
        //animator.SetFloat ("axesVertical" , controller.AxesMovement.z * sprintMultiplier , locomotionSmooth , Time.deltaTime);
    }
}
