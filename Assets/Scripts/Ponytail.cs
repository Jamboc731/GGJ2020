using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ponytail : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(Input.acceleration.x, Input.acceleration.y, -Input.acceleration.z)), .9f);
    }
}
