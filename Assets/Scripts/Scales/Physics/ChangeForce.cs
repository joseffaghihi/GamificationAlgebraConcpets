using UnityEngine;
using System.Collections;

public class ChangeForce : MonoBehaviour {

    private Vector3 originalPosition;
    private Quaternion originalRotation;

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void setPosition(GameObject weight)
    {
        //Store the Weights intial Information
        originalPosition = weight.transform.position;
        originalRotation = weight.transform.rotation;
    }

    //Reset the Gameobject
    public void Reset(GameObject weight)
    {
        //Sets the objects position to it's default
        weight.transform.position = originalPosition;
        weight.transform.rotation = originalRotation;

        //Execute if the gameobject is a rigidbody
        //Sets the rigidbodie's forces to their null state
        if (weight.rigidbody != null)
        {
            weight.rigidbody.velocity = Vector3.zero;
            weight.rigidbody.angularVelocity = Vector3.zero;
            weight.rigidbody.useGravity = false;
            weight.constantForce.force = Vector3.down * 0;
        }
    }

    //Execute on an in-game button click
    //Enables forces to make the Gameobject fall
    public void OnButtonClicked()
    {
        constantForce.force = Vector3.down * 100;
        rigidbody.useGravity = true;
    }
}
