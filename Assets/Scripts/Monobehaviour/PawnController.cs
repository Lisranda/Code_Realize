using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnController : MonoBehaviour
{
    public Vector3 AxesMovement { get; protected set; }
    //public float AngleRotation { get; protected set; }
    public Speed SpeedLevel { get; protected set; }

    protected Vector3 velocity;

    protected virtual void Awake () {
        SpeedLevel = Speed.Run;
    }
}
