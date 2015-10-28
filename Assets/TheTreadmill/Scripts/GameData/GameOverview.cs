using UnityEngine;
using System.Collections;

public class GameOverview : MonoBehaviour 
{
    private GameControl gameController = new GameControl();

	// Update is called once per frame
	void Update () 
    {
        if (gameController.isGameOver()) //Display Game over
        {
            GameObject.FindGameObjectWithTag("GameMenu").GetComponent<GameMenu>().openMenu(true); //Open Menu
            GameObject.Find("GameOver").GetComponent<CanvasGroup>().alpha = 1; //Show Game over box

            //Teleport to last Scene
            //LevelManager.LoadPreviousLevel();
        }
        else if(gameController.isGameOver() && GameObject.Find("GameOver").GetComponent<CanvasGroup>().alpha == 1) //Hide Game over
        {
            GameObject.Find("GameOver").GetComponent<CanvasGroup>().alpha = 0; //Show Game Over box
        }

        if(gameController.GameWon()) //Game has been won
        {
            GameObject.FindGameObjectWithTag("GameMenu").GetComponent<GameMenu>().openMenu(true); //Open Menu
            //GameObject.Find("GameWon").GetComponent<CanvasGroup>().alpha = 1; //Show Game over box
        }
	}
}
