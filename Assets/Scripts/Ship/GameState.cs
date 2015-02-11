using UnityEngine;
using UnityEngine.UI; // Include this so we can use the 'Text' functionality.
using System.Collections;

public class GameState : MonoBehaviour
{

        /*                         Game State
         * This script will be used to start the game, restart the game, and anything else necessary.
         * Essentially, this script will be 'main'.
         * 
         * Goals:
         *      Determine if the user wins or fails
         *      Any Startup algorithms (help, tutorials, etc)
         *      Toggles execution of the spawners
         */


    // Declarations and Initializations
    // ---------------------------------
    // References
        // Public reference to the winLoseTextbox added to the scene [DC]
            public Text winLoseText;
        // Link to score script
            public Score score;
        // Game Scores
            private int scoreCorrect = 0;
            private int scoreIncorrect = 0;
        // Max Score Limits
            public int scoreCorrectMax = 10; // [NG]
            public int scoreIncorrectMax = 5; // [NG]
        // Game State Modes (SWITCHES)
            private bool gameStateNormal = false; // Enable the regular game
            private bool gameStateTutorial = false; // Enable tutorial mode
            private bool gameStateOver = false; // Enable a stop signal; game over
        // Toggle var for displaying 'Restart $key' after game is over.
            private bool displayRestartButton = true;
        // Primary Spawner Switch
            private bool activateSpawner = false; // This switch is the primary - game level that will toggle wither the spawner is to be active or not.
        // Normal Game State Activator Switch
            private bool activateNormalState = true; // When true, this will activate the spawners and reset the score
        // Audio Control References [DC]
            public AudioSource audio;
            public AudioClip failSound;
        // More references
            public FinalDestroyer finalDestroyer;

    // ----




	// Use this for initialization
	void Start ()
    {
	    // CALL TUTORIAL HERE
	} // End of Start


	
	// Update is called once per frame
	void Update ()
    {
        if (gameStateTutorial == false)  // iif (if and only if) we're not running the tutorial mode
        {
            if (gameStateOver == false) // If the game is not over
            {
                GameStateExecutionNormal(); // Run the game as intended
            }

            else if (gameStateOver == true)
            {
                // Allow the user to restart the game if a key is pressed at this given moment
                if (Input.GetButtonDown("Restart Game"))
                    GameStateActivateNormal();
                else
                    if (displayRestartButton == true)
                        DisplayRestartGameMsg(); // This function will deal with the restart message.
            } // End of IF: gameStateOver
        }
        else if (gameStateTutorial == true)
        {
            // Nothing to do
            // The player is still in the tutorial function
        }
        else // Run Away Function
        {
            // Something went horribly wrong
            Debug.LogError(" RUN AWAY PROTECTION ");
            Debug.Break(); // Halt unity's testing environment from further execution
        } // End of IF: gameStateTutorial

	} // End of Update



    // Game State: Normal
    void GameStateExecutionNormal()
    {
        if (activateNormalState == true)
            GameStateActivateNormal(); // Setup the environment before the game starts
        // Fetch the current score
            FetchScores();
        // Check the score
            CheckScore();
    } // End of GameStateExecutionNormal



    // Check the score of the game
    void CheckScore()
    {
        // If the player hits the max limit, change the game state [DC]
        if (scoreIncorrect >= scoreIncorrectMax)
            GameStateGameOver(true);
        else if (scoreCorrect >= scoreCorrectMax)
            GameStateGameOver(false);
    } // End of CheckScore



    // When the game is over, execute this function
    void GameStateGameOver(bool grade)
    {
        // Game Over
        gameStateOver = true;
        gameStateNormal = false;

        // Stop the spawners from further execution
        activateSpawner = false;


        // Kill the minions in the scene
        finalDestroyer.AccessMinionGenocide();

        // If the user won or failed the game, display the message on the screen.
        // Following with the common standards of the CUI:
        //      0 = Passed
        //      1 = Failed or Error
        if (grade == false)
            winLoseText.text = "You Won!";
        else
        {
            winLoseText.text = "Try Again!";
            //audio.clip = failSound;
            //audio.Play();
            //audio.loop = false;
        }
    } // End of GameStateGameOver



    // This function will allow the spawners to begin their execution, reset the scores, and anything else that must be dealt with before a new game is initialized.
    void GameStateActivateNormal()
    {
        activateSpawner = true; // Turn on the spawners
        gameStateOver = false; // Disable the 'Game Over' game state
        score.AccessThrashScores(); // Flush the current scores
        winLoseText.text = ""; // Remove any existing text string of wither the player lost or won
        activateNormalState = false; // Turn this off
        gameStateNormal = true;
        DisplayRestartGameMsg(); // Clear the restart message from the screen
    } // End of GameStateActivateNormal



    // This function will merely display text after the game is over.
    void DisplayRestartGameMsg()
    {
        if (gameStateNormal == true)
        {
            // Remove the text from the screen
            displayRestartButton = true; // Once the game is completed, enable this flag to show the 'restart' button to reset the game.
            return;
        }
        else
        {
            Debug.Log("Game Over\n Press the 'R' key to restart!");
            displayRestartButton = false; // Only display the message once, not repeatedly to avoid over processing on the registers
        }
    } // End of DisplayRestartGameMsg



    // Fetch the game score
    void FetchScores()
    {
        // Access the score script to retrieve the scoreCorrect and scoreIncorrect.
        scoreCorrect = score.ScoreCorrect;
        scoreIncorrect = score.ScoreIncorrect;
    } // End of FetchScores



    // Return the primary spawner switch value to the calling script.
    public bool ActivateSpawner
    {
        get { return activateSpawner; }
    } // End of ActivateSpawner
}