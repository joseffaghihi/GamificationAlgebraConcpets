using UnityEngine;
using System.Collections;

public class MoveObstacleHorizontally : MonoBehaviour 
{
    public float speed = 20;

	// Update is called once per frame
	void FixedUpdate () 
    {
        GetComponent<Transform>().Translate(Vector3.left * Time.deltaTime * speed, Space.World);
	}
}
