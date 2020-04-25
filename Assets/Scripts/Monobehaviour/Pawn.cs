using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField]
    [Range (0 , 15)]
    protected float speedWalk = 6f;

    [SerializeField]
    [Range (0 , 15)]
    protected float speedSprint = 12f;

    public float SpeedWalk { get { return speedWalk; } protected set { } }
    public float SpeedSprint { get { return speedSprint; } protected set { } }
}
