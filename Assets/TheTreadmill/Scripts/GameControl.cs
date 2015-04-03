using UnityEngine;
using System.Collections;

public class GameControl 
{
	static public int lives = 3;
    public int treadmillSpeed = 1;

    private bool gameWon = false;

	//Subtract a life and check for EndOfGame
	public void LostLife()
	{
		lives--;
		if(isGameOver())
		{
			//Display Game Over on the Screen

			//Teleport to last Scene
			LevelManager.LoadPreviousLevel();
		}
	}
	
    public void GameWon()
    {
        gameWon = true;
        Debug.Log("won");
    }

    public int GetLives()
    {
        return lives;
    }

	//Check Game State
	private bool isGameOver()
	{
		if (lives <= 0)
			return true;
		else
			return false;
	}
}
