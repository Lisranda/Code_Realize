using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField]
    [Range (0 , 25)]
    protected float speedWalk = 3f;
    public float SpeedWalk => speedWalk;

    [SerializeField]
    [Range (0 , 25)]
    protected float speedRun = 6f;
    public float SpeedRun => speedRun;

    [SerializeField]
    [Range (0 , 25)]
    protected float speedSprint = 12f;
    public float SpeedSprint => speedSprint;

    [SerializeField]
    [Range (0 , 25)]
    protected float speedRoll = 18f;
    public float SpeedRoll => speedRoll;
    
    [SerializeField]
    [Range (0 , 200)]
    protected float jumpStrength = 20f;
    public float JumpStrength => jumpStrength;
}
