using UnityEngine;
using System.Collections;

public class ForwardDriver : MonoBehaviour 
{
    public float speed;

	void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}
}
