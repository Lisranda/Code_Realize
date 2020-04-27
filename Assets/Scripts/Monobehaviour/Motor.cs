using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    public void Move (Vector3 velocity) {
        transform.Translate (velocity * Time.deltaTime);
    }

    public void Jump (float jumpStrength) {
        transform.Translate (Vector3.up * jumpStrength * Time.deltaTime);
    }
}
