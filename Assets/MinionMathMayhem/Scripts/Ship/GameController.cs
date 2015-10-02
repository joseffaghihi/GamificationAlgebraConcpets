using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class GameController : MonoBehaviour
    {
        /*                                      GAME CONTROLLER
         * This script is the spin of the game and controls the main game environment.  This will execute the game tutorial and manage the scores of the game, in which this script will determine if the game is over.  When the game is over, it is possible to restart the game environment - fresh.
         * 
         * 
         * GOALS:
         *  Control the game environment
         *  Execute the tutorial
         *  Determine the game state (Won or lost)
         *  Enable the spawners
         *  Restart the game
         */



        // Declarations and Initializations
        // ---------------------------------
            // How many to win each wave
                public uint maxScore = 10;
            // How many can the user get wrong on each wave
                public uint maxScoreFail = 5;
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
			//  Rules Control on the rulesCanvas GameObject  

				private RulesControl rulesControl;

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
                // Game Over
                    public delegate void GameStateEventEnded();
                    public static event GameStateEventEnded GameStateEnded;
                // Game Restarted
                    public delegate void GameStateEventRestart();
                    public static event GameStateEventRestart GameStateRestart;
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
				// Rules
					public GameObject rulesCanvas;



		private void Awake()
		{
			rulesControl = rulesCanvas.GetComponent<RulesControl>();
		}

        // Signal Listener: Detected
        private void OnEnable()
        {
            // Tutorial movie ended
                MoviePlay.TutorialStateEnded += TutorialMode_Ended;
        } // OnEnable()



        // Signal Listener: Deactivate
        private void OnDisable()
        {
            // Tutorial movie ended
                MoviePlay.TutorialStateEnded -= TutorialMode_Ended;
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



        /// <summary>
        ///     When enabled via Unity's Inspector, this gives the developer the ability to change the game's speed depending on the float value.
        ///         Default: 1
        ///         Increase speed iif: x > 1
        ///         Decrease speed iif: x < 1
        /// </summary>
        /// <returns>
        ///     Nothing is returned
        /// </returns>
        private IEnumerator HeartbeatTimer()
        {
            while (true)
            {
                // If the heartbeat has been enabled
                if (heartbeat)
                {
                    // Is the new value matched up with the current game speed value.
                    if (HeartbeatTimerCorrection(heartbeatTimer) != Time.timeScale)
                    {
                        // Make sure that the heartbeat has the correct value set
                            heartbeatTimer = HeartbeatTimerCorrection(heartbeatTimer);
                        // Update the game speed to match with the assigned value
                            Time.timeScale = heartbeatTimer;
                        // Output that the game speed has been altered to the new value
                            Debug.Log("ATTN: Heartbeat has been changed to value: " + heartbeatTimer);
                    } // If game speed has been change
                } // Heartbeat is enabled


                // If the heartbeat has been disabled
                else if (!heartbeat)
                {
                    // If the game speed has been altered
                    if (Time.timeScale != 1f) // The default value is 1f.
                    {
                        // Update the game speed
                            Time.timeScale = 1f;
                        // Restore the game speed back to it's default setting
                            Debug.Log("ATTN: Heartbeat has been restored to its default setting.");
                    } // If game speed has been changed, previously
                } // Heartbeat is disabled

                // Wait before re-looping
                    yield return new WaitForSeconds(0.2f);
            } // while
        } // HeartbeatTimer()



        /// <summary>
        ///     This function will try to correct the value of the heartbeat value to be a valid float value as the program expects it to be.
        /// </summary>
        /// <param name="heartbeatValue">
        ///     New Heartbeat Value
        /// </param>
        /// <returns>
        ///     Corrected (if it was corrected) value of the heartbeat timer.
        /// </returns>
        private float HeartbeatTimerCorrection(float heartbeatValue)
        {
            if (heartbeatValue < 0f)
                // Flip the sign
                heartbeatValue = heartbeatValue * -1;
            else
                // If the heatbeat timer is null, slightly increase the timer.  If it is set to zero, the game will freeze completely.
                if (heartbeatValue == 0)
                    heartbeatValue += 0.1f;

            return heartbeatValue;
        } // HeartbeatTimerCorrection()



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
                yield return null;
            // ----
            // Execute the Tutorial
                 yield return (StartCoroutine(GameExecute_Tutorial()));
            // Display the animations and environment settings at the very start of the game
				// StartCoroutine(rulesControl.Access_WaitForRulesToFinish());
				//Time.timeScale = 0.0f;
	                scriptGameEvent.Access_FirstRun_Animations();
	            // Initiate the wait delay on the spawners
	                RequestGraceTime();
	            // ----
	            while (true) // Always check the state of the game.
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
                // Send signal that the game is over
                    GameStateEnded();
                // ----

                // Did the user win?
                if (scriptScore.ScoreCorrect >= maxScore)
                {
                    gameOverWin = true;
                    gameOverFail = false;
                    GameOver_WinText();
                } // if (User Won)

                // Did the user lost?
                if (scriptScore.ScoreIncorrect >= maxScoreFail)
                {
                    gameOverWin = false;
                    gameOverFail = true;
                    GameOver_LostText();
                } // if (User lost)
            } // if (Check Scores)
        } // CheckScores()



        // Display the text that the user has won the game.
        private void GameOver_WinText()
        {
            textWinOrLose.text = "You Won!" + "\n\n" +
                "Press 'R' to Restart!";
        } // GameOver_WinText()



        // Display the text that the user has lost the game.
        private void GameOver_LostText()
        {
            textWinOrLose.text = "Try again!" + "\n\n" +
                "Press 'R' to Restart!";
        } // GameOver_LostText()



        // Thrash the displayed text that indicates that the user has won or lost.
        private void GameOver_ClearText()
        {
            textWinOrLose.text = "";
        } // GameOver_ClearText()



        // Game Tutorial Sequence front-end.
        private IEnumerator GameExecute_Tutorial()
        {
            
            // Enable the tutorial objects
                objectTutorial_SkipButton.SetActive(true);
                objectTutorial_Movie.SetActive(true);
                objectTutorial_Canvas.SetActive(true);
            // Send the 'Tutorial Active' signal
                TutorialStateStart();
            // Run a signal detector; once the signal has been detected, the tutorial is finished.
            //    Once the tutorial is finished, the rest of the game can execute.
                yield return (StartCoroutine(GameExecute_Tutorial_ScanSignal()));
            // Disable the tutorial objects
                objectTutorial_Movie.SetActive(false);
                objectTutorial_Canvas.SetActive(false);
                objectTutorial_SkipButton.SetActive(false);
            
				yield return (StartCoroutine(rulesControl.Access_WaitForRulesToFinish()));
                rulesCanvas.SetActive(false);
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



        // This will reset the game's environment
        private void RestartGame()
        {
            // flip the gameOver variables
                gameOver = !gameOver;
            // Send signal that the game is restarting
                GameStateRestart();

            if (gameOverWin == true)
                gameOverWin = !gameOverWin;

            if (gameOverFail == true)
                gameOverFail = !gameOverFail;

            // Clear the Game Over text
                GameOver_ClearText();
            // Call the 'What-Is' animation
                scriptGameEvent.Access_FirstRun_Animations();
            // Issue a delay before activating the spawners.
                RequestGraceTime();
        } // RestartGame()



        // Return the value of: spawnMinions to outside classes
        public bool SpawnMinions
        {
            get {
                    return spawnMinions;
                } // get
        } // SpawnMinions



        // Return the value of: gameOverWin to outside classes
        public bool GameOverWon
        {
            get {
                    return gameOverWin;
                } // get
        } // GameOverWon



        // Return the value of: gameOverFail to outside classes
        public bool GameOverFail
        {
            get {
                    return gameOverFail;
                } // get
        } // GameOverFail



        // Return the value of: gameOver to outside classes
        public bool GameOver
        {
            get {
                    return gameOver;
                } // get
        } // GameOver



        // Return the value of: Maximum Score Possible
        public uint MaxScore
        {
            get
            {
                return maxScore;
            } // get
        } // MaxScore



        // Return the value of: Maximum Incorrect Score Possible
        public uint MaxScoreIncorrect
        {
            get
            {
                return maxScoreFail;
            } // get
        } // MaxScoreIncorrect



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