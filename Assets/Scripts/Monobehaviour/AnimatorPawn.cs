using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPawn : MonoBehaviour
{
    Animator animator;
    Pawn pawn;
    PawnController controller;

    [Header ("Smooth Times")]
    [SerializeField]
    [Range (0 , 1)]
    float locomotionSmooth = .1f;

    void Awake () {
        animator = GetComponentInChildren<Animator> ();
        pawn = GetComponent<Pawn> ();
        controller = GetComponent<PawnController> ();
    }

    void Update () {
        AnimateMovement ();
    }

    void AnimateMovement () {
        float sprintMultiplier = controller.SpeedLevel == Speed.Sprint ? 2f : 1f;
        animator.SetFloat ("axesHorizontal" , controller.AxesMovement.x * sprintMultiplier , locomotionSmooth , Time.deltaTime);
        animator.SetFloat ("axesVertical" , controller.AxesMovement.z * sprintMultiplier , locomotionSmooth , Time.deltaTime);
    }
}
