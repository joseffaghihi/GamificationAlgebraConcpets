using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class GameController : MonoBehaviour
    {
        /*                      GAME CONTROLLER
         * This class is designed to manage the general state of the game.
         * 
         * GOALS:
         *  Execute the tutorial
         *  Enable the spawners
         *  Restart the game
         */



        // Declarations and Initializations
        // ---------------------------------
            // How many to win each wave
                // Can be manipulated within Unity's Inspector
                public int maxScore = 10;
            // How many can the user get wrong on each wave
                // Can be manipulated within Unity's Inspector
                public int maxScoreFail = 5;
            // [GameManager] Determining if the game is still executing or is finished.
                private bool gameOver = false;
            // [GameManager] Determines if the game is over; user won.
                private bool gameOverWin = false;
            // [GameManager] Determines if the game is over; user failed.
                private bool gameOverFail = false;
            // [GameManager] Enables or disables the spawners
                private bool spawnMinions = false;
            // [GameManager] Tutorial Ended switch
                private bool gameTutorialEnded = false;

            // Accessors and Communication
                // Win/Lose HUD Message
                    public Text textWinOrLose;
                // Scores
                    public Score scriptScore;
                // Tutorial
                    public VoiceOver scriptTutorial;
                // Game Controller
                    public GameEvent scriptGameEvent;
                // Tutorial State
                    public delegate void TutorialStateEventStart();
                    public static event TutorialStateEventStart TutorialStateStart;
                // Spawn Controller
                    public SpawnController scriptSpawnController;
                // Request Grace-Time Period; Broadcast Event
                    public delegate void RequestGraceTimePeriodSig();
                    public static event RequestGraceTimePeriodSig RequestGraceTime;

            // Debug Tools
                // Heartbeat Timer
                    public bool heartbeat = false;
                    public float heartbeatTimer = 1f;

            // GameObjects
                // Tutorial
                    public GameObject objectTutorial_Movie;
                    public GameObject objectTutorial_Canvas;
                    public GameObject objectTutorial_SkipButton;




        // Signal Listener: Detected
        private void OnEnable()
        {
            VoiceOver.TutorialStateEnded += TutorialMode_Ended;
        } // OnEnable()



        // Signal Listener: Deactivate
        private void OnDisable()
        {
            VoiceOver.TutorialStateEnded -= TutorialMode_Ended;
        } // OnDisable()



        // When the signal has been detected that the tutorial is over, this function will be called.
        private void TutorialMode_Ended()
        {
            // Toggle this variable; this is used to tell the other functions that the tutorial is over.
                gameTutorialEnded = !gameTutorialEnded;
        } // TutorialMode_Ended()



        // This function is immediately executed once the actor is in the game scene.
        private void Start()
        {
            // First make sure that all the scripts and actors are properly linked
                CheckReferences();
            // Debug Timer; when enabled, this can slow down the heartbeat of the game.
                StartCoroutine(HeartbeatTimer());
            // Start the main game manager
                StartCoroutine(GameManager());
        } // Start()



        // When enabled through the Unity's inspector, this gives the ablity to slow down the game.
        private IEnumerator HeartbeatTimer()
        {
            while (true)
            {
                // Change the heartbeat to a new value
                    if (heartbeat == true)
                    {
                        Time.timeScale = heartbeatTimer;
                        Debug.Log("ATTN: Heartbeat has been changed to value: " + heartbeatTimer);
                    } // if

                // Restore the heartbeat to it's original value
                    else if (heartbeat == false && Time.timeScale != 1f)
                    {
                        Time.timeScale = 1f;
                        Debug.Log("ATTN: Heartbeat has been restored to its default setting.");
                    } // else-if

                // Wait before re-looping
                    yield return new WaitForSeconds(0.5f);
            } // while
        } // HeartbeatTimer()



        // This function is always called on each frame.
        private void Update()
        {
            // Check to see if the game is over
            // If the game is over, we must detect if the user is pushing the 'restart' key as defined in the Input Manager in Unity.
            if (gameOver == true)
                if (Input.GetButtonDown("Restart Game"))
                    // Restart the game
                    RestartGame();
        } // Update()



        // This function manages how the game is controlled - from start to finish.
        private IEnumerator GameManager()
        {
            // Without this, things go horribly wrong [reference errors, even though everything is initialized]
                yield return null;
            // ----
            // Execute the Tutorial
                // BY-REQUEST; the 'VoiceOver'tutorial is now considered deprecated [NG]
                yield return (StartCoroutine(GameExecute_Tutorial()));
            // Display the animations and environment settings at the very start of the game
                scriptGameEvent.Access_FirstRun_Animations();
            // Initiate the wait delay on the spawners
                RequestGraceTime();
            // ----
            while (true) // This is a never ending loop
            {
                // Fetch the scores and compute the scores
                    CheckScores();
                // Brief wait time to ease the CPU
                    yield return new WaitForSeconds(0.5f);

                // Manage the spawners, if needed.
                if (gameOver == !true)
                {
                    // Spawner toggle: True
                    if (spawnMinions == !true)
                        FlipMinionSpawner();
                }
                else if (gameOver == !false)
                {
                    // Spawner toggle: False
                    if (spawnMinions == !false)
                        FlipMinionSpawner();
                }
                // ----
            } // while loop
        } // GameManager()



        // This function will only flip the bool value of the spawner variable.
        private void FlipMinionSpawner()
        {
            spawnMinions = !spawnMinions;
        } // FlipMinionSpawner()



        // Fetch and compute the scores
        private void CheckScores()
        {
            if (scriptScore.ScoreCorrect >= maxScore || scriptScore.ScoreIncorrect >= maxScoreFail)
            {
                // The game is over
                    gameOver = true;
                // Kill the Minions from the scene
                    scriptGameEvent.Access_MinionGenocide();
                // ----

                // Did the user win?
                if (scriptScore.ScoreCorrect >= maxScore)
                {
                    gameOverWin = true;
                    gameOverFail = false;
                    GameOver_WinText();
                } // if

                // Did the user lost?
                if (scriptScore.ScoreIncorrect >= maxScoreFail)
                {
                    gameOverWin = false;
                    gameOverFail = true;
                    GameOver_LostText();
                } // if
                // ----
            } // if
        } // CheckScores()



        private void GameOver_WinText()
        {
            textWinOrLose.text = "You Won!" + "\n\n" +
                "Press 'R' to Restart!";
        } // GameOver_WinText()



        private void GameOver_LostText()
        {
            textWinOrLose.text = "Try again!" + "\n\n" +
                "Press 'R' to Restart!";
        } // GameOver_LostText()



        private void GameOver_ClearText()
        {
            textWinOrLose.text = "";
        } // GameOver_ClearText()



        // Run the game tutorial; requires the script reference
        private IEnumerator GameExecute_Tutorial()
        {
            // Enable the tutorial objects
                objectTutorial_Movie.SetActive(true);
                objectTutorial_Canvas.SetActive(true);
                objectTutorial_SkipButton.SetActive(true);
            // Send the 'Tutorial Active' signal
                TutorialStateStart();
            // Run a signal detector; once the signal has been detected, the tutorial is finished.
            //    Once the tutorial is finished, the rest of the game can execute.
                yield return (StartCoroutine(GameExecute_Tutorial_ScanSignal()));
            // Disable the tutorial objects
                objectTutorial_Movie.SetActive(false);
                objectTutorial_Canvas.SetActive(false);
                objectTutorial_SkipButton.SetActive(false);
        } // GameExecute_Tutorial()



        // Continually loop until the tutorial has ended.  This function will be released once the tutorial has ended.
        private IEnumerator GameExecute_Tutorial_ScanSignal()
        {
            while (gameTutorialEnded == false)
            {
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        } // GameExecute_Tutorial_ScanSignal()



        // This function is going to reset the game
        private void RestartGame()
        {
            // flip the gameOver variables
                gameOver = !gameOver;

            if (gameOverWin == true)
                gameOverWin = !gameOverWin;

            if (gameOverFail == true)
                gameOverFail = !gameOverFail;

            // Clear the Game Over text
                GameOver_ClearText();
            // Reset the scores to null
                scriptScore.AccessReset();
            // Call the 'What-Is' animation
                scriptGameEvent.Access_FirstRun_Animations();
            // Issue a delay before activating the spawners.
                RequestGraceTime();
        } // RestartGame()



        // Return the value of: spawnMinions to outside classes
        public bool SpawnMinions
        {
            get { return spawnMinions; }
        } // SpawnMinions



        // Return the value of: gameOverWin to outside classes
        public bool GameOverWon
        {
            get { return gameOverWin; }
        } // GameOverWon



        // Return the value of: gameOverFail to outside classes
        public bool GameOverFail
        {
            get { return gameOverFail; }
        } // GameOverFail



        // Return the value of: gameOver to outside classes
        public bool GameOver
        {
            get { return gameOver; }
        } // GameOver



        // This function will check to make sure that all the references has been initialized properly.
        private void CheckReferences()
        {
            if (scriptScore == null)
                MissingReferenceError("Score");
            if (scriptTutorial == null)
                MissingReferenceError("Tutorial");
            if (objectTutorial_Movie == null)
                MissingReferenceError("Tutorial Actor");
            if (scriptGameEvent == null)
                MissingReferenceError("Game Event");
            if (textWinOrLose == null)
                MissingReferenceError("Win Or Lose [Text]");
        } // CheckReferences()



        // When a reference has not been properly initialized, this function will display the message within the console and stop the game.
        private void MissingReferenceError(string refLink = "UNKNOWN_REFERENCE_NOT_DEFINED")
        {
            Debug.LogError("Critical Error: Could not find a reference to [ " + refLink + " ]!");
            Debug.LogError("  Can not continue further execution until the internal issues has been resolved!");
            Time.timeScale = 0; // Halt the game
        } // MissingReferenceError()
    } // End of Class
} // Namespace