using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class Minion_RandomSetNumbers : MonoBehaviour
    {
        /*                          MINION RANDOM SET NUMBERS
         *  This class is designed to generate a specific set of numbers that each minion will be assigned.
         *   This will help to control when the 'right' answer appears to the user -- without this
         *   enforcement control and due to randomness of generated at time, time will elapse of when the
         *   user is bored and when the answer appears.  This will control what numbers each minions will have
         *   and try to make it challenging to the user if needed or easier.
         *
         *
         * STRUCTURAL DEPENDENCY NOTES:
         *      Quadratic Equation TextBox
         *           |_ Problem Box
         *      Letter_Text
         *           |_ LetterBox
         *      Game Controller
         *           |_ GameEvent
         *
         *
         * GOALS:
         *  Fetch the number ranges
         *  Fetch for the expected answer
         *  Randomize the entire number set
         *  Place the right answer in a random index
         *  Regenerate when needed
         */

        #region Declaration and Initializations
        // Number set array
            private static short[] numberSetArray = new short[20];
        // Highlight Array Index
            private static short arrayCounter = 0;
        // Objects
            // Problem Box - To fetch random number
                private static ProblemBox scriptProblemBox;
            // Game Event - Fetch the 'correct' answer
                private static GameEvent scriptGameEvent;
        #endregion



        /// <summary>
        ///     Unity Function
        ///     
        ///     This will be automatically called when the object is in the scene
        /// </summary>
        private void Awake()
        {
            // Fetch the Problem Box class instance
                scriptProblemBox = GameObject.FindGameObjectWithTag("RandomNumberGenerator").GetComponent<ProblemBox>();
            // Fetch the Game Event class instance
                scriptGameEvent = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameEvent>();
        }


        /// <summary>
        ///     DEBUG PURPOSES ONLY
        /// 
        ///     Output the contents within the array
        /// </summary>
        private static void Output()
        {
            // Scan and output the contents
            for (short i = 0; i < numberSetArray.Length; i++)
                Debug.Log("Array RandSet at [" + i + "] set to: " + numberSetArray[i]);
        } // Output()



        /// <summary>
        ///     Fill the array with randomized numbers
        /// </summary>
        private static void FillArray()
        {
            // Fill the array
            for (short i = 0; i < numberSetArray.Length; i++)
            {
                numberSetArray[i] = (short)scriptProblemBox.Access_GetRandomNumber();
            }

            // Fetch a random index
            int randKey = Random.Range(0, numberSetArray.Length);
            // Set the answer to the highlighted index
            numberSetArray[randKey] = FetchAnswer();

            Debug.Log("Answer was selected at index: " + randKey);
            Output();
        } // FillArray()



        /// <summary>
        ///     Retrives the answer that the user needs to win
        /// </summary>
        /// <returns>
        ///     Answer
        /// </returns>
        private static short FetchAnswer()
        {
            return (short)scriptGameEvent.Access_GetQuadraticEquation_Index();
        } // FetchAnswer()



        /// <summary>
        ///     Retrieve the array elements and pass it to the minions
        /// </summary>
        /// <returns>
        ///     Randomized number from the array
        /// </returns>
        private static int GetNumber()
        {
            if (numberSetArray.Length == arrayCounter)
            {
                // Refill the array
                arrayCounter = 0;
                FillArray();
            }

            int value = numberSetArray[arrayCounter];
            arrayCounter++;
            return value;
        } // GetNumber()



        /// <summary>
        ///     Call a private function to retrieve the array elements and pass it to the minions
        /// </summary>
        /// <returns>
        ///     Randomized number from the array
        /// </returns>
        public static int Access_GetNumber()
        {
            return GetNumber();
        } // GetNumber()



        /// <summary>
        ///     Allow other classes to call this function to access the 
        ///         FillArray() function.  Which generates the randomization set
        /// </summary>
        public void Access_FillArray()
        {
            FillArray();
        } // Access_FillArray()
    }
} // 168