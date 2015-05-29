using UnityEngine;
using System.Collections;

public class MoveNumber : MonoBehaviour 
{
	public float speed = 20;
	
	// Update is called once per frame
	void Update () 
	{
		gameObject.GetComponent<Transform> ().localPosition = Vector3.MoveTowards (gameObject.GetComponent<Transform> ().localPosition, new Vector3(0,0,0), speed*Time.deltaTime);
	}
}
