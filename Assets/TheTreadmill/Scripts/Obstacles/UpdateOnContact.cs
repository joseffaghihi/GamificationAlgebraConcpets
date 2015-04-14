using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateOnContact : MonoBehaviour 
{
    public GameObject correctCoinEffect;
    public GameObject falseCoinEffect;

    public void OnCollisionEnter(Collision collision)
    {
        int obstacleNum = GetComponentInChildren<RandomNumberGenerator>().GetRandomNumber(); //Get the number attached to the obstacle
        int answer = GameObject.Find("Equation").GetComponent<EquationGenerator>().GetAnswer(); //Get the 

        if (collision.gameObject.tag == "Player" && obstacleNum == answer) //Correct Answer
        {
            GameControl gameController = new GameControl();
            gameController.clearedRound();
            (Instantiate(correctCoinEffect, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity) as GameObject).transform.parent =
                            GameObject.FindGameObjectWithTag("Player").transform;
            GameObject.Find("Equation").GetComponent<EquationGenerator>().newRandomEquation();
        }
        else //Wrong Answer
        {   
            GameControl gameController = new GameControl();
            gameController.LostLife();
        }

        GameObject.Find("Board_GameInfo").GetComponent<BoardDisplay>().UpdateBoard(); //Update the board info (lives, rounds, etc.)
    }
}
