using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour 
{
    public Vector3 minBoundaries = new Vector3(0.0f, 0.0f, 0.0f); //Store minimum boundaries
    public Vector3 maxBoundaries = new Vector3(0.0f, 0.0f, 0.0f); //Store maximum boundaries

    //Enable/Disable boundaries
    public bool enableXBoundary = true;
    public bool enableYBoundary = true;
    public bool enableZBoundary = true;

    //public float ZBoundary = 0;

    void Update()
    {
        //X-Boundary
        if ((GetComponent<Transform>().position.x < minBoundaries.x || GetComponent<Transform>().position.x > maxBoundaries.x) && enableXBoundary)
        {
            Destroy(gameObject);
        }

        //Y-Boundary
        if ((GetComponent<Transform>().position.y < minBoundaries.y || GetComponent<Transform>().position.y > maxBoundaries.y) && enableYBoundary)
        {
            Destroy(gameObject);
        }

        //Z-Boundary
        if ((GetComponent<Transform>().position.z < minBoundaries.z || GetComponent<Transform>().position.z > maxBoundaries.z) && enableZBoundary)
        {
            Destroy(gameObject);
        }
    }
	
}
