using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour 
{
    private bool inMenu = false;

	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !inMenu)
        {
            Time.timeScale = 0; //Pause Game
            GetComponent<CanvasGroup>().alpha = 1.0F; //Display Game Menu
            inMenu = true; //Now in the game menu
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && inMenu)
        {
            Time.timeScale = 1; //Continue Game
            GetComponent<CanvasGroup>().alpha = 0.0F; //Hide Game Menu
            inMenu = false; //Back into game mode
        }
	}

    //Load Menu
    public void LoadMainMenu()
    {
        Application.LoadLevel("Menu");
    }
}
