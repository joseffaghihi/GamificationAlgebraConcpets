using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EquationGenerator : MonoBehaviour 
{
    public int min, max; //Set the minimum and maximum range for the random Number

    private int randomNumber;

	// Use this for initialization
	void Start () 
    {
        //Set Random number to a random number except for 0
        randomNumber = 0;
        while(randomNumber == 0)
        {
            randomNumber = Random.Range(min, max);
        }
        
        //Adjust text to account for negative and positive sign
        if(randomNumber > 0)
            GetComponent<Text>().text = "x + " + randomNumber + " = 0";
        else if(randomNumber < 0)
            GetComponent<Text>().text = "x - " + Mathf.Abs(randomNumber) + " = 0";
	}

    public int GetRandomNumber()
    {
        return randomNumber;
    }
}
