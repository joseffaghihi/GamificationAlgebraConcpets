using UnityEngine;
using System.Collections;

public class GameControl 
{
    private const int lives = 3;
    private const int rounds = 10;
	static private int livesLeft = lives;
    static private int currentRound = 1;

	//Subtract a life and check for EndOfGame
	public void LostLife()
	{
		livesLeft--;
	}

    //Set game's info to the default value
    public void RestartGame()
    {
        currentRound = 1; //Reset rounds
        livesLeft = lives; //Reset player lives
    }
	
    //Execute this on winning the game
    public void GameWon()
    {
        Debug.Log("won");
    }

    //Execute this on clearing a round
    public void clearedRound()
    {
        if (currentRound >= 10) //If the game is over
        {
            GameWon();
        }
        else //If the round, but not the game, is over
        {
            currentRound++;
        }
    }

    public int GetLives()
    {
        return livesLeft;
    }

    public int GetCurrentRound()
    {
        return currentRound;
    }

    public int GetTotalRounds()
    {
        return rounds;
    }

	//Check Game State
	public bool isGameOver()
	{
		if (livesLeft <= 0)
			return true;
		else
			return false;
	}
}
