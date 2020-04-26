using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    [Range (0 , 50)]
    float gravity = 35f;

    public float Gravity { get { return gravity; } private set { } }
}
