using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour 
{
    public float ZBoundary = 0;

    void Update()
    {
        if(GetComponent<Transform>().position.z < ZBoundary)
        {
            Destroy(gameObject);
        }
    }
	
}
