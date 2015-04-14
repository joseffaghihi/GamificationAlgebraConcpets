using UnityEngine;
using System.Collections;

public class GameControl 
{
	public int lives = 3;
    public int treadmillSpeed = 1;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

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
	
	//Check Game State
	bool isGameOver()
	{
		if (lives <= 0)
			return true;
		else
			return false;
	}
}
