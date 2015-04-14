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
            openMenu(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && inMenu)
        {
            openMenu(false);
        }
	}

    //Load Menu
    public void LoadMainMenu()
    {
        Application.LoadLevel("Menu");
    }

    //Restart Game
    public void RestartGame()
    {
        GameControl gcontroller = new GameControl();
        gcontroller.RestartGame(); //Reset player/game info
        Application.LoadLevel(Application.loadedLevel); //Load current Scene
        openMenu(false); //Close Menu
    }

    //Open/Close Menu Screen
    public void openMenu(bool state)
    {
        if(state == true) //Open Menu
        {
            Time.timeScale = 0; //Pause Game
            GetComponent<CanvasGroup>().alpha = 1.0F; //Display Game Menu
            inMenu = true; //Now in the game menu
        }
        else if(state == false) //Close Menu
        {
            Time.timeScale = 1; //Continue Game
            GetComponent<CanvasGroup>().alpha = 0.0F; //Hide Game Menu
            inMenu = false; //Back into game mode
        }
    }
}
