using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour 
{	
	// Update is called once per frame
	void Update () 
    {
        GetComponent<Transform>().Rotate(GetComponent<ConstantForce>().force * Time.deltaTime);
	}
}
