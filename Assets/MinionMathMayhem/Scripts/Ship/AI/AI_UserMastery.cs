using UnityEngine;
using System.Collections.Generic; // This way we can use use 'List' type
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class AI_UserMastery : MonoBehaviour
    {

        /*
         *                                                   GAME ARTIFICIAL INTELLIGENCE
         *                                                          USER MASTERY
         * This script monitors the user's performance with the material and tries to adjust based on the user's mastery.  If the user is doing exceptionally well, then this will try to enforce a more challenge for the user.  If the user is not
         *  well or failing, this AI component will try at best to keep the user in the game and try to enforce the material on the user.
         *
         * NOTES:
         *  This AI Component is mainly just a grading scale and tries at best to keep the player motivated; this doesn't change the environment by itself, it still requires the dependencies to push the changes.
         *
         * STRUCTURAL DEPENDENCY NOTES:
         *      User Mastery [AI]
         *          |_ Score [Score]
         *          |_ GameController [GameController]
         *
         * GOALS:
         *      Tries to keep the player invulved and motivated
         *      Requests the environment or game to be more challenging or easier, based on user's mastery.
         *      Tries to make sure that the user understands that material while keeping the game fun.
         */



        // Declarations and Initializations
        // ---------------------------------
            // User's current tentative scores
            //  These scores will be flushed every so often within the game at run time.
            //  These scores are mainly for progress and comprehension levels.
                private int userPrefScoreCorrect = 0;
                private int userPrefScoreWrong = 0;
            // Possible Scores
                private int userPrefScorePossible = 0;
            // Game State; is the game over?
                private bool gameOver = false;
            // How many times the user has been graded
                private int gradeEvaluation = 0;
            // Activate this AI component when the possible score has reached been reached by specific value
                // NOTES: Higher the value, the longer it takes for the AI to run and monitor the user's performance.
                //          Shorter the value, the quicker it takes for the AI to run and monitor the user's performance.
                private static short userPrefScorePossible_EnableAI = 4;
            // User Performance Array
                private static short userPrefArrayIndexSize = 4;
                private bool[] userPrefArray = new bool[userPrefArrayIndexSize];
                private short userPrefArrayIndex_HighLight = 0; // Use for scanning array
            // Scan User Performance in 'x' tries - well after the AI does its first initial scan.
                public short scanUserStatsTries = 3;
            // Next scan to compare with the Possible Score variable; this variable determines when the next scan should take place.
                private int userPrefNextScan = 0;
            // Events and Delegates
                // Minion Speed
                    public delegate void MinionSpeedDelegate(float runningSpeed, float climbingSpped);
                    public static event MinionSpeedDelegate MinionSpeed;
                // Tutorial session (if the user isn't understanding the material)
                    public delegate void TutorialSessionDelegate(bool random, bool movie = false, bool window = false, int indexKey = 0);
                    public static event TutorialSessionDelegate TutorialSession;
                // Report User's Grade
                    // gradeLetter = Current Grade
                    // gradePercent = Score (grade) percertage
                    // gradeEvaluated = How many times the score has been evaluated or checked
                    public delegate void UserGradedPerformance(char gradeLetter, int gradePercent, int gradeEvaluated);
                    public static event UserGradedPerformance ReportPlayerGrade;
        // ---------------------------------
        



        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Detected (or heard)
        /// </summary>
        private void OnEnable()
        {
            Score.ScoreUpdate_Correct += IncrementCorrectScore;
            Score.ScoreUpdate_Incorrect += IncrementWrongScore;
            GameController.GameStateEnded += GameState_ToggleGameOver;
            GameController.GameStateRestart += ResetAllScores;
        } // OnEnable()



        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Deactivated
        /// </summary>
        private void OnDisable()
        {
            Score.ScoreUpdate_Correct -= IncrementCorrectScore;
            Score.ScoreUpdate_Incorrect -= IncrementWrongScore;
            GameController.GameStateEnded -= GameState_ToggleGameOver;
            GameController.GameStateRestart -= ResetAllScores;
        } // OnDisable()



        /// <summary>
        ///     This daemon servicer will determine how the game should interact with the player; this is done by anaylizing -
        ///     the user's score and getting the user's grade (by precentage) and understand how well the end-user understands -
        ///     the material presented.
        /// 
        ///     What this Controls:
        ///         Toggle Dynamic Equation Generator's complexity level
        ///         Change the Minion's speed
        ///                 OR
        ///         Change the Minion actor spawner
        ///         Provide demonstration tutorials when needed
        ///         Kick the player if foundation is bad
        /// </summary>
        public void Main()
        {
                // Only run when the possible points has reached a certain value and if the game isn't over.
                if ((userPrefScorePossible >= userPrefScorePossible_EnableAI && !gameOver) && ((userPrefNextScan == userPrefScorePossible) || userPrefNextScan == 0))
                {
                    // DEBUG MODE
                        DebugUserStats();


                // User may not understand the material
                if (UserPerformance_Array())
                        TutorialSession(true);

                    // Update when the next scan should take place
                        userPrefNextScan = userPrefScorePossible + scanUserStatsTries;
                } // if AI active and monitoring
        } // Main()



        /// <summary>
        ///     This function will merely spit out information about the user's current score and statistics were available
        /// </summary>
        private void DebugUserStats()
        {
            Debug.Log("AI Mastery_Correct: " + userPrefScoreCorrect);
            Debug.Log("AI Mastery_Incorrect: " + userPrefScoreWrong);
            Debug.Log("AI Mastery_Possible Score: " + userPrefScorePossible);
            Debug.Log("AI Mastery_User's Score: " + string.Format("{0:0.00}", ((float)userPrefScoreCorrect / (float)userPrefScorePossible * 100)));
        } // DebugUserStats()



        /// <summary>
        ///     Check to make sure that the user understands the material.
        ///     This is done by managing the array which holds the user's performance
        /// </summary>
        /// <returns>
        ///     True = User did not understand the material\n
        ///     False = User understands the material
        /// </returns>
        private bool UserPerformance_Array()
        {
            short userIncorrectAnswers = 0;
            // Read the array and make sure that the user understands the material
            for (short i = 0; i <= (userPrefArrayIndexSize - 1); ++i)
                if (userPrefArray[i] == false)
                    userIncorrectAnswers++;

            // User may not have understood the material or is having difficulties
            if (userIncorrectAnswers == userPrefArrayIndexSize)
                return true;
            else
                return false;
        } // UserPerformance_Array()



        /// <summary>
        ///     Update values within the array based on the user's actual performance.
        /// 
        ///     Array Index Value Key:
        ///     True = Correct Answer
        ///     False = Wrong Answer
        /// </summary>
        private void ArrayUpdateField(bool userFeedback)
        {
            // Make sure that we're not overflowing the array, move the highlight to the start of the index if needed.
            if (userPrefArrayIndex_HighLight >= userPrefArrayIndexSize)
                userPrefArrayIndex_HighLight = 0;

            // Update the array at the highlighted index
                userPrefArray[userPrefArrayIndex_HighLight] = userFeedback;
            // Highlight the next index
                userPrefArrayIndex_HighLight++;
        } // ArrayUpdateField()



        /// <summary>
        ///     Update the correct score for the Daemon service
        /// </summary>
        private void IncrementCorrectScore()
        {
            userPrefScoreCorrect++;

            // Update the array that holds the user performance
                ArrayUpdateField(true);
            // Update the possible score
                UpdatePossibleScore();

        } // IncrementCorrectScore()



        /// <summary>
        ///     Update the incorrect score for the Daemon service
        /// </summary>
        private void IncrementWrongScore()
        {
            userPrefScoreWrong++;

            // Update the array that holds the user performance
                ArrayUpdateField(false);
            // Update the possible score
                UpdatePossibleScore();
        } // IncrementWrongScore()



        /// <summary>
        ///     Update the possible score possible by adding the scores.
        /// </summary>
        private void UpdatePossibleScore()
        {
            userPrefScorePossible = (userPrefScoreCorrect + userPrefScoreWrong);
        } // UpdatePossibleScore()



        /// <summary>
        ///     This will reset the scores; game restarted
        /// </summary>
        private void ResetAllScores()
        {
            userPrefScorePossible = 0;
            userPrefScoreCorrect = 0;
            userPrefScoreWrong = 0;

            // Flip the value
            GameState_ToggleGameOver();
        } // ResetAllScores()



        /// <summary>
        ///     Toggles the gameOver variable when the game has reached it's end.
        /// </summary>
        private void GameState_ToggleGameOver()
        {
            gameOver = !gameOver;
        } // GameState_GameOver()
    } // End of Class
} // Namespace