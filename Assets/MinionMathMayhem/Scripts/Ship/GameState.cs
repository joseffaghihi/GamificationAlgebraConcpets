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
         *      Toggles execution of the spawner's
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
        // Primary Spawner Switch
            private bool activateSpawner = false; // This switch is the primary - game level that will toggle wither the spawner is to be active or not.
        // Normal Game State Activator Switch
            private bool activateNormalState = true; // When true, this will activate the spawner's and reset the score
        // Audio Control References [DC]
            public AudioSource audio;
            public AudioClip failSound;
        // More references
            public FinalDestroyer finalDestroyer;
            public VoiceOver tutorial;
            //public SceneMusic music;
    // ----




    // Accessors; used for returning the value of the game state
    // Game State: Game Over
    public bool GameStateOver
    {
        get { return gameStateOver; }
    }

    // Game State: Tutorial Mode
    public bool GameStateTutorial
    {
        get { return gameStateTutorial; }
    }

    // Game State: Normal Mode
    public bool GameStateNormal
    {
        get { return gameStateNormal; }
    }
    // ----



	
	// Update is called once per frame
	void Update ()
    {
        // Check to see if the tutorial mode is active
        CheckTutorialMode();

        // Check the game state:
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

        // Stop the spawner's from further execution
        activateSpawner = false;


        // Kill the minions in the scene
        finalDestroyer.AccessMinionGenocide();

        // If the user won or failed the game, display the message on the screen.
        // Following with the common standards of the CUI:
        //      0 = Passed
        //      1 = Failed or Error
        if (grade == false)
            winLoseText.text = "You Won!" + "\n\n" +
                                "Press 'R' to Restart!";
        else
            winLoseText.text = "Try Again!" + "\n\n" + 
                                "Press 'R' to Restart!";
    } // End of GameStateGameOver



    // This function will allow the spawner's to begin their execution, reset the scores, and anything else that must be dealt with before a new game is initialized.
    void GameStateActivateNormal()
    {
        activateSpawner = true; // Turn on the spawner's
        gameStateOver = false; // Disable the 'Game Over' game state
        score.AccessReset(); // Flush the current scores
        winLoseText.text = ""; // Remove any existing text string of wither the player lost or won
        activateNormalState = false; // Turn this off
        gameStateNormal = true;
    } // End of GameStateActivateNormal



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



    // Check to see if the Tutorial mode is active at this given time
    private void CheckTutorialMode()
    {
        gameStateTutorial = tutorial.TutorialMode;
    } // End of CheckTutorialMode

} // End of Class