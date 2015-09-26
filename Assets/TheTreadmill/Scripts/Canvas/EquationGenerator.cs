using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * This script generates the equation and outputs it onto the coin.
 */ 

public class EquationGenerator : MonoBehaviour 
{
    private const int min = -9, max = 9; //Set the minimum and maximum range for the random Number

    //Stores the number on the left/right side of the equal sign
    private int rightSideNumber = 0;
    private int leftSideNumber = 0;

    private int answer = 0; //Stores the answer

    private GameControl gameState = new GameControl(); //Get current info on the game state (current round)

	// Use this for initialization
	void Start () 
    {
        outputEquation(); //Output the Equation
	}

    public void outputEquation()
    {
        updateRandomNumbers(); //Create the random equation

		//Extra for the first two rounds (equation is different in form x = a)
		if(gameState.GetCurrentRound() == 1) // First Round
		{
			answer = Random.Range (0,max);
			GetComponent<Text>().text = "x = " + answer;

		}
		else if(gameState.GetCurrentRound() == 2)
		{
			answer = Random.Range (min, 0);
			GetComponent<Text>().text = "x = " + answer;
		}

        //Adjust text to account for negative and positive sign
        else if (leftSideNumber >= 0)
            GetComponent<Text>().text = "x + " + leftSideNumber + " = " + rightSideNumber;
        else if (leftSideNumber < 0)
            GetComponent<Text>().text = "x - " + Mathf.Abs(leftSideNumber) + " = " + rightSideNumber;
    }

    public int GetAnswer()
    {
        return answer;
    }

    //Creates an equation depending on difficulty selected.
    public void updateRandomNumbers()
    {
        //Addition with the right site equal to zero
        if (gameState.GetCurrentRound() <= 4)
        {
            leftSideNumber = Random.Range(min, 0);
            rightSideNumber = 0;
        }

        //Addition with both sides containing non-zero numbers
        else if (gameState.GetCurrentRound() <= 6)
        {
            leftSideNumber = Random.Range(min, 0);
            rightSideNumber = Random.Range(min, 0);
        }

        //Subtraction with right side equal to zero
        else if (gameState.GetCurrentRound() <= 8)
        {
            leftSideNumber = Random.Range(0, max);
            rightSideNumber = 0;
        }

        //Subtraction with both sides containing non-zero numbers
        else if (gameState.GetCurrentRound() <= 10)
        {
            leftSideNumber = Random.Range(0, max);
            rightSideNumber = Random.Range(0, max);
        }

        //Mix of it all - everything is possible
        else
        {
            leftSideNumber = Random.Range(min, max); //Set the left-side Number

            int calculatedMin, calculatedMax; //Hold the possible numbers for the right side of the equation

            //Make sure the answer will not be above the maximum or below the minimum
            if(leftSideNumber < 0) //If left-side number is smaller than 0 - boundaries change
            {
                calculatedMin = leftSideNumber - min;
                calculatedMax = leftSideNumber + max;
            }

            if (leftSideNumber > 0) //If left-side number is bigger than 0 - boundaries change
            {
                calculatedMin = leftSideNumber + min;
                calculatedMax = leftSideNumber - max;
            }

            else //If left-side number is equal to 0 - boundaries stay the same
            {
                calculatedMin = min;
                calculatedMax = max;
            }

            rightSideNumber = Random.Range(calculatedMin, calculatedMax); //Set the right-side Number
        }

        answer = rightSideNumber - leftSideNumber;
    }

    public int getLeftSide()
    {
        return leftSideNumber;
    }

    public int getRightSide()
    {
        return rightSideNumber;
    }

    public int getAnswer()
    {
        return answer;
    }
}
