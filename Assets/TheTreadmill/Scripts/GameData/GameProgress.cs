using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * This script handles updating other scripts by checking the user's selected answer.
 * It also instantiates effects based on whether the user was correct or not.
 * Updates the following Scripts: GameControl
 */

public class GameProgress : MonoBehaviour 
{
    //Particle Effects
    public GameObject correctCoinEffect;
    public GameObject falseCoinEffect;

    //Updated Scripts
    private GameControl gameController = new GameControl();

    //On Collision
    public void OnCollisionEnter(Collision collision)
    {
        int obstacleNum = GetComponentInChildren<RandomNumberGenerator>().GetRandomNumber(); //Get the number attached to the obstacle
        int answer = GameObject.Find("Equation").GetComponent<EquationGenerator>().GetAnswer(); //Get the answer to the problem

        if (collision.gameObject.tag == "Player" && obstacleNum == answer) //Correct Answer
        {
            gameController.clearedRound(); //Update Game Info

            //Instantiate Particle Effect
            (Instantiate(correctCoinEffect, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity) as GameObject).transform.parent =
                            GameObject.FindGameObjectWithTag("Player").transform;
        }
        else //Wrong Answer
        {   
            gameController.LostLife(); //Update Game Info
        }

		gameController.DelayWave (true);
        GameObject.Find("Board_GameInfo").GetComponent<BoardDisplay>().UpdateBoard(); //Update the board info (lives, rounds, etc.)
    }
}
