using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class AI_UserMastery : MonoBehaviour
    {

        /*
         *                                              GAME ARTIFICIAL INTELLIGENCE
         *                                                     USER MASTERY
         * This script monitors the user's performance with the material and tries to adjust based on the user's mastery.  If the user is doing exceptionally well, then this will try to enforce a more challenge for the user.  If the user is not
         *  well or failing, this AI component will try at best to keep the user in the game and try to enforce the material on the user.
         *
         * NOTES:
         *  This AI Component is mainly just a grading scale and tries at best to keep the player motivated; this doesn't change the environment by itself, it still requires the dependencies to push the changes.
         *
         * STRUCTURAL DEPENDENCY NOTES:
         *      User Mastery [AI]
         *          |_ <warten...>
         *
         * GOALS:
         *      Tries to keep the player invulved and motivated
         *      Requests the environment or game to be more challenging or easier, based on user's mastery.
         *      Tries to make sure that the user understands that material while keeping the game fun.
         */



        // Declarations and Initializations
        // ---------------------------------
            private int userPrefScoreCorrect = 0;
            private int userPrefScoreWrong = 0;
            private int userPrefScorePossible = 0;
            private static short userPrefScorePossible_EnableAI = 10;
            // User Performance Array
                private static short userPrefArrayIndexSize = 3;
                private bool[] userPrefArray = new bool[userPrefArrayIndexSize];
                private short userPrefArrayIndex_HighLight = 0; // Use for scanning array
            // BITFIELD EMULATED IDENTIFIER
                // 0 - Empty or Default
                // 1 - Minion Speed has changed
                // 2 - Spawner Frequency
                private short userPrefChallenge = 0;
        // ---------------------------------





        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Detected (or heard)
        /// </summary>
        private void OnEnable()
        {
            Score.ScoreUpdate_Correct += Daemon_UserPerformance_IncrementCorrectScore;
            Score.ScoreUpdate_Incorrect += Daemon_UserPerformance_IncrementWrongScore;
        } // OnEnable()



        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Deactivated
        /// </summary>
        private void OnDisable()
        {
            Score.ScoreUpdate_Correct -= Daemon_UserPerformance_IncrementCorrectScore;
            Score.ScoreUpdate_Incorrect -= Daemon_UserPerformance_IncrementWrongScore;
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
                // User understands the material thus far
                if (!Daemon_UserPerformance_Array())
                    Daemon_UserPerformance_PerformanceGradingLibrary((userPrefScoreCorrect / userPrefScorePossible * 100));

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
        private void Daemon_UserPerformance_PerformanceGradingLibrary(int userGrade)
        {
            // Sorry for this long conditional, I couldn't find a nicer way to do this with a Switch statement :(
            if (95 < userGrade && userGrade <= 100)
            {
                // Skill Level: Very-High
            }

            else if (90 < userGrade && userGrade <= 95)
            {
                // Skill Level: Medium-High
            }

            else if (85 < userGrade && userGrade <= 90)
            {
                // Skill Level: Medium
            }

            else if (80 < userGrade && userGrade <= 85)
            {
                //   Skill Level: Medium-Low
            }

            else if (75 < userGrade && userGrade <= 80)
            {
                //  Skill Level: Low
            }

            else if (70 < userGrade && userGrade <= 75)
            {
                //  Skill Level: WeakFoundation - Low
            }

            else if (65 < userGrade && userGrade <= 70)
            {
                //  Skill Level: WeakFoundation - Medium
            }

            else if (60 < userGrade && userGrade <= 65)
            {
                //  Skill Level: WeakFoundation - High
            }

            else if (userGrade <= 60)
            {
                //  Skill Level: WeakFoundation - Failed
            }

            else
            {
                // Incase the grade parameter is something unpredictable, output the error on the terminal.
                Debug.Log("<!> ATTENTION: RUN AWAY DETECTED <!>");
                Debug.Log("Using grade value of: " + userGrade);
            }
        } // Daemon_UserPerformance_PerformanceGradingLibrary()



        /// <summary>
        ///     Check to make sure that the user understands the material.
        ///     This is done by managing the array which holds the user's performance
        /// </summary>
        /// <returns>
        ///     True = User did not understand the material\n
        ///     False = User understands the material
        /// </returns>
        private bool Daemon_UserPerformance_Array()
        {
            short userIncorrectAnswers = 0;
            // Read the array and make sure that the user understands the material
            for (short i = 0; i <= userPrefArrayIndexSize; ++i)
                if (userPrefArray[i] == false)
                    userIncorrectAnswers++;

            // User may not have understood the material or is having difficulties
            if (userIncorrectAnswers == userPrefArrayIndexSize)
                return true;
            else
                return false;
        } // Daemon_UserPerformance_Array()



        /// <summary>
        ///     Update values within the array based on the user's actual performance.
        /// 
        ///     Array Index Value Key:
        ///     True = Correct Answer
        ///     False = Wrong Answer
        /// </summary>
        private void Daemon_UserPerformance_ArrayUpdateField(bool userFeedback)
        {
            // Make sure that we're not overflowing the array, move the highlight to the start of the index if needed.
            if (userPrefArrayIndex_HighLight >= userPrefArrayIndexSize)
                userPrefArrayIndex_HighLight = 0;

            // Update the array at the highlighted index
            userPrefArray[userPrefArrayIndex_HighLight] = userFeedback;
            // Highlight the next index
            userPrefArrayIndex_HighLight++;
        } // Daemon_UserPerformance_ArrayUpdateField()



        /// <summary>
        ///     Update the correct score for the Daemon service
        /// </summary>
        private void Daemon_UserPerformance_IncrementCorrectScore()
        {
            userPrefScoreCorrect++;

            // Update the array that holds the user performance
            Daemon_UserPerformance_ArrayUpdateField(true);
            // Update the possible score
            Daemon_UserPerformance_UpdatePossibleScore();

        } // Daemon_UserPerformance_IncrementCorrectScore()



        /// <summary>
        ///     Update the incorrect score for the Daemon service
        /// </summary>
        private void Daemon_UserPerformance_IncrementWrongScore()
        {
            userPrefScoreWrong++;

            // Update the array that holds the user performance
            Daemon_UserPerformance_ArrayUpdateField(false);
            // Update the possible score
            Daemon_UserPerformance_UpdatePossibleScore();
        } // Daemon_UserPerformance_IncrementWrongScore()



        /// <summary>
        ///     Update the possible score possible by adding the scores.
        /// </summary>
        private void Daemon_UserPerformance_UpdatePossibleScore()
        {
            userPrefScorePossible = (userPrefScoreCorrect + userPrefScoreWrong);
        } // Daemon_UserPerformance_UpdatePossibleScore()



        /// <summary>
        ///     This will reset the scores; game restarted
        /// </summary>
        private void Daemon_UserPerformance_ResetAllScores()
        {
            userPrefScorePossible = 0;
            userPrefScoreCorrect = 0;
            userPrefScoreWrong = 0;
        } // Daemon_UserPerformance_ResetAllScores()
    } // End of Class
} // Namespace