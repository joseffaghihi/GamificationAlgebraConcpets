using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoardDisplay : MonoBehaviour 
{
    GameControl gameController = new GameControl(); //Get current Game info

	// Update is called once per frame
	public void UpdateBoard () 
    {
        GameObject.Find("Lives_Text").GetComponent<Text>().text = "" + gameController.GetLives();
        GameObject.Find("Lives_Text").GetComponent<Animator>().SetTrigger("Play");

        Invoke("UpdateRounds", 2);
    }

    public void UpdateRounds()
    {
        GameObject.Find("Rounds_Text").GetComponent<Text>().text = gameController.GetCurrentRound() + " / 12";
        GameObject.Find("Rounds_Text").GetComponent<Animator>().SetTrigger("Play");
    }
}
