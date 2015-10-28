using UnityEngine;
using System.Collections;

public class PlayerAudio : MonoBehaviour 
{
    public AudioClip correctCoinSound;
    public AudioClip falseCoinSound;

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            GameControl gameController = new GameControl();

            if(gameController.isAnswerCorrect()) //On Correct answer/coin
            {
                GetComponent<AudioSource>().clip = correctCoinSound;
                GetComponent<AudioSource>().Play();
            }
            else //On False answer/coin
            {
                GetComponent<AudioSource>().clip = falseCoinSound;
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
