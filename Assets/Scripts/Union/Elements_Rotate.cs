using UnityEngine;
using System.Collections;


public class Elements_Rotate : MonoBehaviour {



	public float rotate_speed = 5.0f;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, 0, 15)*Time.deltaTime*rotate_speed);
	}
}
