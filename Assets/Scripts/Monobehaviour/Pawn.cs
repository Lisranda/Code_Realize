using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField]
    [Range (0 , 15)]
    protected float speedWalk = 3f;

    [SerializeField]
    [Range (0 , 15)]
    protected float speedRun = 6f;

    [SerializeField]
    [Range (0 , 15)]
    protected float speedSprint = 12f;

    [SerializeField]
    [Range (0 , 50)]
    protected float gravityModifier = 1f;

    [SerializeField]
    [Range (0 , 200)]
    protected float jumpStrength = 20f;

    public float SpeedWalk { get { return speedWalk; } protected set { } }
    public float SpeedRun { get { return speedRun; } protected set { } }
    public float SpeedSprint { get { return speedSprint; } protected set { } }

    public float GravityModifier { get { return gravityModifier; } protected set { } }
    public float JumpStrength { get { return jumpStrength; } protected set { } }
}
