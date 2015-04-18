using UnityEngine;
using System.Collections;

public class PlayerEffects : MonoBehaviour 
{
    public GameObject correctCoinEffect;
    public GameObject falseCoinEffect;

     public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            GameControl gameController = new GameControl();

            if(gameController.isAnswerCorrect()) //On Correct answer/coin
            {
                //Instantiate particle effect
                (Instantiate(correctCoinEffect, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity) as GameObject).transform.parent =
                                GameObject.FindGameObjectWithTag("Player").transform;
            }
            else //On False answer/coin
            {
                (Instantiate(falseCoinEffect, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity) as GameObject).transform.parent =
                                GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
    }
}
