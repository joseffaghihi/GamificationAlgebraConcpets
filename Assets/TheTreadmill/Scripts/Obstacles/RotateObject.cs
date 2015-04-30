using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour 
{
    //Store rotations
    public bool xRotate = true;
    public bool yRotate = true;
    public bool zRotate = true;

	// Update is called once per frame
	void Update () 
    {
        if(xRotate)
            GetComponent<Transform>().Rotate(new Vector3(160, 0, 0) * Time.deltaTime);
        if(yRotate)
            GetComponent<Transform>().Rotate(new Vector3(0, 160, 0) * Time.deltaTime);
        if(zRotate)
            GetComponent<Transform>().Rotate(new Vector3(0, 0, 160) * Time.deltaTime);
	}
}
