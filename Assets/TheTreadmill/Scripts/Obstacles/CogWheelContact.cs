using UnityEngine;
using System.Collections;

public class CogWheelContact : MonoBehaviour 
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") //Obstacle hit the Player
        {
            //Update gameController
            GameControl gameController = new GameControl();
            gameController.DelayWave(true);
            gameController.LostLife(); //Lost life
        }
        if(collision.gameObject.tag != "Player")
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
        }

        GameObject.Find("Board_GameInfo").GetComponent<BoardDisplay>().UpdateBoard(); //Update the board info (lives, rounds, etc.)
    }
}
