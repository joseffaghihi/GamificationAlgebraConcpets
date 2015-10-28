using UnityEngine;
using System.Collections;

public class GameControl 
{
    private const int lives = 3;
    private const int rounds = 12;
	static private int livesLeft = lives;
    static private int currentRound = 1;
    static private bool answerIsCorrect = false; //Keep check of whether the right answer was given
	static private bool delayWave = false; //Delays wave
    static private int currentRoundTime = 0; //Keep check of how long the player has been in the same round (couldn't think of a better name)

    //Return time of the user being in the current round (time measured in waves)
    public int getCurrentRoundTime()
    {
        return currentRoundTime; 
    }

    //Adds another wave in the current round
    public void addCurrentRoundTime()
    {
        currentRoundTime++;
    }

    //Set the wave counter
    public void setCurrentRoundTime(int num)
    {
        currentRoundTime = num;
    }

	public void DelayWave(bool state)
	{
		delayWave = state;
	}

	public bool getDelayWave()
	{
		return delayWave;
	}

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
    public bool GameWon()
    {
        if (currentRound >= rounds) //Game Won
        {
            return true;
        }
        else
            return false;
    }

    //Execute this on clearing a round
    public void clearedRound()
    {
        if (currentRound < rounds)//If the round, but not the game, is over
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

    public void correctAnswer(bool correct)
    {
        if (correct)
            answerIsCorrect = true;
        else
            answerIsCorrect = false;
    }

    public bool isAnswerCorrect()
    {
        return answerIsCorrect;
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
