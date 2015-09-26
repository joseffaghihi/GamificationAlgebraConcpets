using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HowToWinIntro : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Time.timeScale = 0; //Pause Game
	}
	
	//Function to start the game
    public void StartGame()
    {
        Time.timeScale = 1; //Set time scale back to 1
    }
}
