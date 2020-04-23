using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    public void Move (Vector3 direction, float speed) {
        transform.Translate (direction * speed * Time.deltaTime);
    }
}
