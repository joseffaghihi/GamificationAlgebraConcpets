using UnityEngine;
using System.Collections;

[System.Serializable]
//Game's Player Boundary
public class Boundary
{
    public float xMin, xMax, yMin, yMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 10.0f; //Speed of player
	public float jumpSpeed = 10.0f; //Speed of the Jump

    public float tiltX; //Tilt to side (rotation X)
    public float tiltZ; //Tilt forward/backward (rotation Z)

    public Boundary boundary;

	bool onGround = true; //Checks to see if the player is on the Ground

    void FixedUpdate()
    {
        //Get User Input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

		//Set Speed accrodingly
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical); //Set player movement
        GetComponent<Rigidbody>().velocity = movement * runSpeed; //Set player velocity based on the movement and speed

		//Check 'Y' coordinate (to see if player is on the ground)
		if (GetComponent<Rigidbody>().position.y <= boundary.yMin)
			onGround = true;

		//Jump
		if (Input.GetKeyDown(KeyCode.Space) && onGround) 
		{
			Jump();
			onGround = false;
		}   

        //Check boundary limits
        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), //Position X
            Mathf.Clamp(GetComponent<Rigidbody>().position.y, boundary.yMin, boundary.yMax), //Position Y
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)  //Position Z
        );

		//Tilt character based on velocity
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(GetComponent<Rigidbody>().velocity.z * tiltZ, 0.0f, GetComponent<Rigidbody>().velocity.x * -tiltX);
    }

	//Jump Function
	void Jump() 
	{
        //GetComponent<Rigidbody>().position += new Vector3(0.0f, jumpSpeed * Time.deltaTime, 0.0f);
		//GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed * 100);	

        //Play jump animation
	}
}