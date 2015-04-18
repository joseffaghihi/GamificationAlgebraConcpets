using UnityEngine;
using System.Collections;

public class PlayerEffects : MonoBehaviour 
{
    public GameObject correctCoinEffect;
    public GameObject falseCoinEffect;
    public GameObject text;

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
                GameObject tempText = (Instantiate(text, new Vector3(-5, 18, -10), new Quaternion(0,-180,0,0)) as GameObject); //Instantiate Text
                Destroy(tempText, 3); //Destroy Text after 3 seconds
            }
            else //On False answer/coin
            {
                (Instantiate(falseCoinEffect, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity) as GameObject).transform.parent =
                                GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
    }
}
