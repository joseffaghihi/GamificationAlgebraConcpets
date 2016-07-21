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
            // Game State; is the game over?
                private bool gameOver = false;
            // AI Grading system switch
                private bool aiSwitch = false;
            // Temporary lock variable
                private bool lockAI = false;
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
            Score.ScoreUpdate_Correct += Update_CorrectScore;
            Score.ScoreUpdate_Incorrect += Update_IncorrectScore;
            GameController.GameStateEnded += GameState_ToggleGameOver;
            GameController.GameStateRestart += ResetScores;
        } // OnEnable()



        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Deactivated
        /// </summary>
        private void OnDisable()
        {
            Score.ScoreUpdate_Correct -= Update_CorrectScore;
            Score.ScoreUpdate_Incorrect -= Update_IncorrectScore;
            GameController.GameStateEnded -= GameState_ToggleGameOver;
            GameController.GameStateRestart -= ResetScores;
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
            // Execute the tentative grading system
            // Periodically check the player's tentative score and determine the state of the game
            if (aiSwitch && !gameOver && InspectQueries_Ready() && !lockAI)
            {
                // Temporarily lock this function from re-looping
                    lockAI = !lockAI;

                // Mastery: Did the user get all of the answers incorrect?
                if (UserPerformance_Array())
                    // Call the tutorial
                    TutorialSession(true);

                // Get the user's percentage rate and determine the game challenge
                // UserMasteryReport_Precentage();
            } // if Grading enabled
        } // Main()



        /// <summary>
        ///     When called; this will determine if there is enough data to inspect the user's tentative mastery to the material.
        /// </summary>
        /// <returns>
        ///     True = Ready for inspection
        ///     False = Not enough data gathered yet.
        /// </returns>
        private bool InspectQueries_Ready()
        {
            if (userPrefArrayIndex_HighLight > userPrefArrayIndexSize)
                return true;
            else
                return false;
        } // InspectQueries_Ready()



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
        /// </summary>
        /// <param name="userFeedback">
        ///     True = Correct Answer; False = Wrong Answer.
        /// </param>
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
        ///     This function will retrieve the players percentage rate of the queries gathered
        /// </summary>
        /// <returns>
        ///     Percentage in integer form.
        /// </returns>
        private int UserMasteryReport_Precentage()
        {
            // Declarations and Initializations
            // --------------------------------
            // Find out how many the user got correct and not correct
                int incorrectScore = 0;
                int correctScore = 0;
            // --------------------------------

            // Scan the array and determine what the user got right or wrong.
            //  We're going to need this for statistics purposes.
            for (int i = 0; i < userPrefArrayIndexSize; i++)
            {
                if (userPrefArray[i])
                    correctScore++;
                else
                    incorrectScore++;
            } // Scan array's queries


            // Methodology: (EarnedPoints / PossiblePoints * 100)
            return ((correctScore / (correctScore + incorrectScore)) * 100);
        } // UserMasteryReport_Precentage()



        /// <summary>
        ///     Update the correct score for the Daemon service
        /// </summary>
        private void Update_CorrectScore()
        {
            // Update the array that holds the user performance
                ArrayUpdateField(true);
            // Unlock the grading system; if necessary
                CheckScore_ToggleLock();
        } // Update_CorrectScore()



        /// <summary>
        ///     Update the incorrect score for the Daemon service
        /// </summary>
        private void Update_IncorrectScore()
        {
            // Update the array that holds the user performance
                ArrayUpdateField(false);
            // Unlock the grading system; if necessary
                CheckScore_ToggleLock();
        } // Update_IncorrectScore()



        /// <summary>
        ///     At restart, reset the mutable working variables to their default values.
        /// </summary>
        private void ResetScores()
        {
            // Flip the value of the game over state
            GameState_ToggleGameOver();
            // Reset the Highlighter used in the performance array.
            userPrefArrayIndex_HighLight = 0;
        } // ResetScores()



        /// <summary>
        ///     Toggles the gameOver variable when the game has reached it's end.
        /// </summary>
        private void GameState_ToggleGameOver()
        {
            gameOver = !gameOver;
        } // GameState_ToggleGameOver()



        /// <summary>
        ///     Unlocks the tentative grading system if the lock is active.
        /// </summary>
        private void CheckScore_ToggleLock()
        {
            // Make sure that if the lock has been set, unlock it upon change.
            if (lockAI)
                lockAI = !lockAI;
        } // CheckScore_ToggleLock()



        /// <summary>
        ///     Toggles the aiSwitch variable; useful when the grading part of the AI is active or not.
        /// </summary>
        private void AIGrading_ToggleAISwitch()
        {
            aiSwitch = !aiSwitch;
        } // AIGrading_ToggleAISwitch()
    } // End of Class
} // Namespace