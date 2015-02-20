using UnityEngine;
using System.Collections;

public class TutorialSkipButton : MonoBehaviour
{
    /*                    TUTORIAL SKIP BUTTON
     * This script, being simple, allows the player to skip the entire tutorial.  This is done by sending a signal to the tutorial to 'stop' its execution.
     *  
     * GOALS:
     *  When activated (or true), stop the tutorial and start the game or the next part of the game.
     */


    // Declarations
    // -------------
    // Script References
        public VoiceOver tutorial;
    // ----



    // When the user clicks this object, skip the tutorial.
    void OnMouseDown()
    { 
        tutorial.ToggleSkip(true);
    } // End of OnMouseDown
} // End of Class
