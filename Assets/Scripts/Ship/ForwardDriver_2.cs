using UnityEngine;
using System.Collections;

public class ForwardDriver_2 : MonoBehaviour 
{
    public GameObject exit;
    public float speed = 1.0f;
	
	// Update is called once per frame
	void Update () 
    {
		transform.LookAt (exit.transform.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}
}
