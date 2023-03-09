using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RotateRocket : MonoBehaviour {
    Quaternion rotation;
    float angle;

    private void Start()
    {
        rotation = transform.rotation;
    }

    void FixedUpdate ()
    {
        angle++;
        Quaternion rotationY = Quaternion.AngleAxis(angle, Vector3.up);
        Quaternion rotationX = Quaternion.AngleAxis(angle, Vector3.right);
        transform.rotation = rotation * rotationX * rotationY;
       
	}
}
