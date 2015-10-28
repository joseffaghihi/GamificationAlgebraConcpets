using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoardDisplay : MonoBehaviour 
{
    GameControl gameController = new GameControl(); //Get current Game info

	// Update is called once per frame
	public void UpdateBoard () 
    {
        GetComponent<Text>().text = "Lives Left: " + gameController.GetLives()  +
                                    "\nRound: " + gameController.GetCurrentRound() + "/" + gameController.GetTotalRounds();
	}
}
