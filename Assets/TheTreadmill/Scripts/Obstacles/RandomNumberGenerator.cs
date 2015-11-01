using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RandomNumberGenerator : MonoBehaviour 
{
    public Text front;
    public Text back;
    public int min = -9;
    public int max = 10;

    private int randomNumber;

	// Use this for initialization
	void Start () 
    {
        GameControl gc = new GameControl();
        int answer = GameObject.Find("Equation").GetComponent<EquationGenerator>().GetAnswer(); //Get the answer

        //Lower the limits between which the numbers can fall the longer (lowest it can narrow down to is 2 below and above the actual answer)
        if (min + gc.getCurrentRoundTime() <= answer - 2) //decrease minimum until 2 away from actual answer
            min += gc.getCurrentRoundTime();

        if (max - gc.getCurrentRoundTime() >= answer + 2)
            max -= gc.getCurrentRoundTime();

        randomNumber = Random.Range(min, max);
        front.text = back.text = randomNumber.ToString();
	}

    public int GetRandomNumber()
    {
        return randomNumber;
    }
}
