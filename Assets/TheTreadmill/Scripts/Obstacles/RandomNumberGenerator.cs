using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RandomNumberGenerator : MonoBehaviour 
{
    public Text front;
    public Text back;
    public int min;
    public int max;

    private int randomNumber;

	// Use this for initialization
	void Start () 
    {
        randomNumber = Random.Range(min, max);
        front.text = back.text = randomNumber.ToString();
	}

    public int GetRandomNumber()
    {
        return randomNumber;
    }
}
