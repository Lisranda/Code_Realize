using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPawn : MonoBehaviour
{
    const float locomotionAnimationSmoothTime = .1f;

    Animator animator;
    PlayerController playerController;

    void Awake () {
        animator = GetComponentInChildren<Animator> ();
        playerController = GetComponent<PlayerController> ();
    }

    void Update () {
        float speedPercent = playerController.Velocity ().magnitude / playerController.SpeedMaximum ();
        animator.SetFloat ("speedPercent" , speedPercent , locomotionAnimationSmoothTime , Time.deltaTime);
    }
}
