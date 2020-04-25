using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPawn : MonoBehaviour
{
    [SerializeField]
    [Range (0 , 1)]
    float locomotionAnimationSmoothTime = .1f;

    bool isSprinting = false;

    Animator animator;
    Pawn pawn;
    PawnController controller;

    void Awake () {
        animator = GetComponentInChildren<Animator> ();
        pawn = GetComponent<Pawn> ();
        controller = GetComponent<PawnController> ();
    }

    void Update () {
        AnimateRotation ();
        AnimateMovement ();
    }

    void CheckSprint () {
        isSprinting = false;
        if (controller.SpeedLevel == Speed.Sprint)
            isSprinting = true;
        animator.SetBool ("isSprinting" , isSprinting);
    }

    void AnimateMovement () {
        CheckSprint ();
        float speedMultiplier = isSprinting ? 2f : 1f;
        animator.SetFloat ("axesHorizontal" , controller.AxesMovement.x * speedMultiplier , locomotionAnimationSmoothTime , Time.deltaTime);
        animator.SetFloat ("axesVertical" , controller.AxesMovement.z * speedMultiplier , locomotionAnimationSmoothTime , Time.deltaTime);
    }

    void AnimateRotation () {
        animator.SetFloat ("angleRotation" , controller.AngleRotation , locomotionAnimationSmoothTime , Time.deltaTime);
    }
}
