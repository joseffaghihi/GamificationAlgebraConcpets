using UnityEngine;
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
         *
         * GOALS:
         *      Tries to keep the player invulved and motivated
         *      Requests the environment or game to be more challenging or easier, based on user's mastery.
         *      Tries to make sure that the user understands that material while keeping the game fun.
         */



        // Declarations and Initializations
        // ---------------------------------
            // User's current scores
                private int userPrefScoreCorrect = 0;
                private int userPrefScoreWrong = 0;
            // Possible Scores
                private int userPrefScorePossible = 0;
            // Activate the mastery when possible score reached at defined: {VALUE}
                private static short userPrefScorePossible_EnableAI = 10;
            // User Performance Array
                private static short userPrefArrayIndexSize = 3;
                private bool[] userPrefArray = new bool[userPrefArrayIndexSize];
                private short userPrefArrayIndex_HighLight = 0; // Use for scanning array
            // Challenge Game Environment
                // BITFIELD EMULATED IDENTIFIER
                    // 0 - Empty or Default
                    // 1 - Minion Speed has changed
                    // 2 - Spawner Frequency
                    private short userPrefChallenge = 0;
            // Events and Delegates
                // Minion Speed
                    public delegate void MinionSpeedDelegate(float runningSpeed, float climbingSpped);
                    public static event MinionSpeedDelegate MinionSpeed;
        // ---------------------------------
        



        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Detected (or heard)
        /// </summary>
        private void OnEnable()
        {
            Score.ScoreUpdate_Correct += IncrementCorrectScore;
            Score.ScoreUpdate_Incorrect += IncrementWrongScore;
        } // OnEnable()



        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Deactivated
        /// </summary>
        private void OnDisable()
        {
            Score.ScoreUpdate_Correct -= IncrementCorrectScore;
            Score.ScoreUpdate_Incorrect -= IncrementWrongScore;
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
            // Only run when the possible points has reached a certain value.
            if (userPrefScorePossible >= userPrefScorePossible_EnableAI)
            {
                Debug.Log("Correct: " + userPrefScoreCorrect);
                Debug.Log("Incorrect: " + userPrefScoreWrong);
                Debug.Log("Possible Score: " + userPrefScorePossible);

                // User understands the material thus far
                if (!UserPerformance_Array())
                    PerformanceGradingLibrary((userPrefScoreCorrect / userPrefScorePossible * 100));

                // User may not understand the material
                else
                {
                    // Do something
                }
            } // if
        } // Main()



        /// <summary>
        ///     This function will scan its internal library and determine how to control the environment based on the user's grade.
        /// </summary>
        private void PerformanceGradingLibrary(int userGrade)
        {
            // DEBUG STUFF
                string debugString = "failed to initialize";

            // Sorry for this long conditional, I couldn't find a nicer way to do this with a Switch statement :(
            if (95 < userGrade && userGrade <= 100)
            {
                // Skill Level: Very-High
                debugString = "Very-High";
            }

            else if (90 < userGrade && userGrade <= 95)
            {
                // Skill Level: Medium-High
                debugString = "Medium-High";
            }

            else if (85 < userGrade && userGrade <= 90)
            {
                // Skill Level: Medium
                debugString = "Medium";
            }

            else if (80 < userGrade && userGrade <= 85)
            {
                //   Skill Level: Medium-Low
                debugString = "Medium-Low";
            }

            else if (75 < userGrade && userGrade <= 80)
            {
                //  Skill Level: Low
                debugString = "Low";
            }

            else if (70 < userGrade && userGrade <= 75)
            {
                //  Skill Level: WeakFoundation - Low
                debugString = "WeakFoundation - Low";
            }

            else if (65 < userGrade && userGrade <= 70)
            {
                //  Skill Level: WeakFoundation - Medium
                debugString = "WeakFoundation - Medium";
            }

            else if (60 < userGrade && userGrade <= 65)
            {
                //  Skill Level: WeakFoundation - High
                debugString = "WeakFoundation - High";
            }

            else if (userGrade <= 60)
            {
                //  Skill Level: WeakFoundation - Failed
                debugString = "WeakFoundation - Failed";
            }

            else
            {
                // Incase the grade parameter is something unpredictable, output the error on the terminal.
                Debug.Log("<!> ATTENTION: RUN AWAY DETECTED <!>");
                Debug.Log("Using grade value of: " + userGrade);
            }

            // DEBUG
                Debug.Log("User Master is: " + debugString);
        } // PerformanceGradingLibrary()



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
        } // ResetAllScores()
    } // End of Class
} // Namespace