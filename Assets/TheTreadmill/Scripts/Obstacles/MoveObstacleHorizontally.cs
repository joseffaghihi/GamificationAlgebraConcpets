using UnityEngine;
using System.Collections;

public class MoveObstacleHorizontally : MonoBehaviour 
{
    public float speed = 20;

	// Update is called once per frame
	void FixedUpdate () 
    {
        GetComponent<Transform>().Translate(Vector3.left * Time.deltaTime * speed, Space.World);

		//Destroy the danger zone once the obstacle has passed
		if(GetComponent<Transform>().position.x < -7 && GetComponent<Transform>().position.x > -8)
		{
			Destroy(GameObject.Find("DangerArea(Clone)"));
		}
	}
}
