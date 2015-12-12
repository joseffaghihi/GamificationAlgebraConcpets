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
            private static int[] numberSetArray = new int[20];
        // Highlight Array Index
            private static short arrayCounter = 0;

        #endregion
    }
}