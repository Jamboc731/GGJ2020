using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{

    [SerializeField] int invert = 1;

    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(Input.acceleration.x, Input.acceleration.y, -2) * invert, Vector3.up * invert), .5f);

    }
}
