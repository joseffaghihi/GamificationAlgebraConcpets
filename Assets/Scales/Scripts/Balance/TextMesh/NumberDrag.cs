using UnityEngine;
using System.Collections;

public class NumberDrag : MonoBehaviour
{

    private bool collided;

    // Use this for initialization
    void Start()
    {
        collided = false;
    }

    void OnMouseDrag()
    {
        float mousePositionX = Input.mousePosition.x;
        float mousePositionY = Input.mousePosition.y;
        float mousePositionZ = 100.0F;

        //Set Gui Text position to the mouse position (in World coordinates) to allow collisions
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePositionX, mousePositionY, mousePositionZ));
    }

    //Executes on Collision
    void OnTriggerEnter(Collider other)
    {
        //Put text Mesh on the collided GameObject and destroy the current GameObject
        if (other.gameObject.tag == "Weight" && !collided)
        {
            collided = true;
            other.gameObject.GetComponentInChildren<TextMesh>().text = gameObject.GetComponent<TextMesh>().text;
            Destroy(gameObject);
        }
    }
}
