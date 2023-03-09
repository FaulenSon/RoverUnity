using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Transform Rocket;
	void Start ()
    {

        Rocket.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
	}
	
	
}
