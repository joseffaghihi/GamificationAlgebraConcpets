using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    /** Destroy Obstacle when hit by player
     * @return void
     */
    public void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
