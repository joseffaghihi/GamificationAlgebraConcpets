using UnityEngine;
using System.Collections;

[System.Serializable]
//Game's Player Boundary
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed; //Speed of player
    public float tiltX; //Tilt to side (rotation X)
    public float tiltZ; //Tilt forward/backward (rotation Z)
    public Boundary boundary;

    private float boundaryY; //Position Y

    void Start()
    {
        boundaryY = GetComponent<Transform>().position.y; //Get starting position - coordinate Y of the player
    }

    void FixedUpdate()
    {
        //Get User Input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical); //Set player movement
        GetComponent<Rigidbody>().velocity = movement * speed; //Set player velocity based on the movement and speed
            
        //Check boundary limits
        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), //Position X
            boundaryY,                                                                       //Position Y
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)  //Position Z
        );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(GetComponent<Rigidbody>().velocity.z * tiltZ, 0.0f, GetComponent<Rigidbody>().velocity.x * -tiltX);
    }
}