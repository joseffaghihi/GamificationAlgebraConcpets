using UnityEngine;
using System.Collections;


public class BallsRotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (45, 0, 0) * Time.deltaTime);
	}
}
