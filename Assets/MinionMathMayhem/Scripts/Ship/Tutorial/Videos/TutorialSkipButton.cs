using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class TutorialSkipButton : MonoBehaviour
    {

        /*                    TUTORIAL SKIP BUTTON
         * This script, being simple, allows the player to skip the entire tutorial sequence.
         *      This is done by sending a broadcast signal (or event) to abort or stop the tutorial sequence.
         *  
         * GOALS:
         *  When activated (or true), stop the tutorial sequence and start the game or the next part of the game.
         */


        // Declarations and Initializations
        // ---------------------------------
            // Accessors and Communication
                public delegate void SkipTutorial();
                public static event SkipTutorial SkipTutorialDemand;
        // ----

        // When the object is preparing to be removed from the virtual world; broadcast a signal (event) that the tutorial squence needs to be skipped.
        private void OnDisable()
        {
            SkipTutorialDemand();
        } // OnDisable()
    } // End of Class
} // Namespace