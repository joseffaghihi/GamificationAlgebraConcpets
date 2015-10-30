using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/**
 *This Script handels checking the answer selected by the user and updating the gameController info (cleared rounds, lives, answer, etc.)
 *The Script also creates a new equation
 */ 
public class UpdateOnContact : MonoBehaviour 
{
    public Canvas flyingNumber; //Flying Number
    public Sprite Original, EqualTo, GreaterThan, LessThan;

    //Set Original settings for the feedback system
    public void Start()
    {
        GameObject.FindGameObjectWithTag("Feedback").GetComponent<Image>().sprite = Original; //Change sprite to Original
        GameObject.FindGameObjectWithTag("Feedback").transform.GetChild(0).gameObject.SetActive(true); //Enable Original Text
        GameObject.FindGameObjectWithTag("Feedback").transform.GetChild(1).gameObject.SetActive(false); //Disable Number Text
        GameObject.FindGameObjectWithTag("Feedback").transform.GetChild(2).gameObject.SetActive(false); //Disable Number Text
        GameObject.FindGameObjectWithTag("Feedback").transform.GetChild(1).position = new Vector3(-14.5f, 20f, 40); //Change sprite
        GameObject.FindGameObjectWithTag("Feedback").transform.GetChild(2).position = new Vector3(4.8f, 20f, 40); //Change sprite
    }

    public void OnCollisionEnter(Collision collision)
    {
        GameObject.Find("Equation").GetComponent<EquationGenerator>().Invoke("outputEquation", 8); //Create new Equation whenever the player collides with a coin
        int num = gameObject.GetComponentInChildren<RandomNumberGenerator>().GetRandomNumber(); //Store number on coin
		GameControl gameController = new GameControl();
        int obstacleNum = GetComponentInChildren<RandomNumberGenerator>().GetRandomNumber(); //Get the number attached to the obstacle
        int answer = GameObject.Find("Equation").GetComponent<EquationGenerator>().getAnswer(); //Get the answer

        //Set numbers on the balance
        GameObject.FindGameObjectWithTag("Feedback").transform.GetChild(1).GetComponent<Text>().text = num.ToString();
        GameObject.FindGameObjectWithTag("Feedback").transform.GetChild(2).GetComponent<Text>().text = answer.ToString();

        //Create the flying Number
        if (collision.gameObject.tag == "Player")
        {
            GameObject number = (Instantiate(flyingNumber, gameObject.transform.position, Quaternion.identity) as GameObject);
            GameObject.Find("FlyingNumber(Clone)").GetComponent<MoveNumber>().setNumber(num);

            GameObject.FindGameObjectWithTag("Feedback").transform.GetChild(0).gameObject.SetActive(false); //Disable Orignal Text
            GameObject.FindGameObjectWithTag("Feedback").transform.GetChild(1).gameObject.SetActive(true); //Enable Number Text
            GameObject.FindGameObjectWithTag("Feedback").transform.GetChild(2).gameObject.SetActive(true); //Enable Number Text
        }

        if (collision.gameObject.tag == "Player" && obstacleNum == answer) //Correct Answer
        {
            //Update gameController
            gameController.correctAnswer(true); //Correct Answer
            gameController.clearedRound(); //Cleared a round

            GameObject.FindGameObjectWithTag("Feedback").GetComponent<Image>().sprite = EqualTo; //Change sprite
        }
        else if(collision.gameObject.tag == "Player") //Wrong Answer
        {
            //Update gameController
            gameController.correctAnswer(false); //Wrong Answer
            gameController.LostLife(); //Lost life

            //Change the Sprite according to whether it is greater or less than the actual answer
            if(obstacleNum < answer)
            {
                GameObject.FindGameObjectWithTag("Feedback").GetComponent<Image>().sprite = LessThan; //Change sprite
                GameObject.FindGameObjectWithTag("Feedback").transform.GetChild(1).position -= new Vector3(0, 1.25f, 0); //Change sprite
                GameObject.FindGameObjectWithTag("Feedback").transform.GetChild(2).position += new Vector3(-0.3f, 2f, 0); //Change sprite
            }

            else if(obstacleNum > answer)
            {
                GameObject.FindGameObjectWithTag("Feedback").GetComponent<Image>().sprite = GreaterThan; //Change sprite
                GameObject.FindGameObjectWithTag("Feedback").transform.GetChild(1).position += new Vector3(0, 1.75f, 0); //Change sprite
                GameObject.FindGameObjectWithTag("Feedback").transform.GetChild(2).position -= new Vector3(0.6f, 1.4f, 0); //Change sprite
            }
        }
	
		gameController.DelayWave (true);
        GameObject.Find("Board_GameInfo").GetComponent<BoardDisplay>().UpdateBoard(); //Update the board info (lives, rounds, etc.)
    }
}
