using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour 
{	
	// Update is called once per frame
	void Update () 
    {
        GetComponent<Transform>().Rotate(new Vector3(0, 160, 0) * Time.deltaTime);
	}
}
