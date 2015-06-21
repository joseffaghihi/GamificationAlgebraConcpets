using UnityEngine;
using UnityEngine.UI; // Allow the use of the 'text' component
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class EventLetterChange : MonoBehaviour
    {

        /*                              EVENT LETTER CHANGE
         * This script is designd to display what 'letter' is to be selected when the 'What is...' appears on the screen.
         *  NOTE: This is NOT related to the 'Letter Box' object, which is an independent object.  This script will merely try to
         *   fetch the selected index letter (of the quadratic) and display it when the "What is" appears on the screen.
         *   
         * DEPENDENCIES:
         *      Scene GameObjects
         *          Letter_Box (HUD object)
         */




        // Declarations and Initializations
        // ---------------------------------
                private Text ThisText;
                private GameObject referenceText;
                private Text referenceTextLetter;
        // ----





        // Use this for initialization
        void Start()
        {
            // Initialization
            ThisText = GetComponent<Text>();
            referenceText = GameObject.Find("Letter_Text");
            if (referenceText == null)
            {
                Debug.Log("No referenceText object was set.");
            }
            referenceTextLetter = referenceText.GetComponent<Text>();
        } // Start()



        // Update on each frame
        void Update()
        {
            ThisText.text = referenceTextLetter.text;
        } // Update()
    } // End of Class
} // Namepsace