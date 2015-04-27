using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/**
 *This Script handels checking the answer selected by the user and updating the gameController info (cleared rounds, lives, answer, etc.)
 *The Script also creates a new equation
 */ 
public class UpdateOnContact : MonoBehaviour 
{
    public void OnCollisionEnter(Collision collision)
    {
        int obstacleNum = GetComponentInChildren<RandomNumberGenerator>().GetRandomNumber(); //Get the number attached to the obstacle
        int answer = GameObject.Find("Equation").GetComponent<EquationGenerator>().GetAnswer(); //Get the answer

        if (collision.gameObject.tag == "Player" && obstacleNum == answer) //Correct Answer
        {
            //Update gameController
            GameControl gameController = new GameControl();
            gameController.correctAnswer(true); //Correct Answer
            gameController.clearedRound(); //Cleared a round

            //Create new equation
            GameObject.Find("Equation").GetComponent<EquationGenerator>().outputEquation();
        }
        else //Wrong Answer
        {
            //Update gameController
            GameControl gameController = new GameControl();
            gameController.correctAnswer(false); //Wrong Answer
            gameController.LostLife(); //Lost life
        }

        GameObject.Find("Board_GameInfo").GetComponent<BoardDisplay>().UpdateBoard(); //Update the board info (lives, rounds, etc.)
    }
}
