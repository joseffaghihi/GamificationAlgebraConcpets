using UnityEngine;
using UnityEngine.UI; // Used for 'text' type
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class Minion_Identity : MonoBehaviour
    {

        /*                      MINION IDENTITY
         * This class is designed for the minion to self generate its own randomized number given by a specific range.
         *  This essentially gives the minion its own unique number that they choose.
         * 
         * GOALS:
         *  Self-Generate a number
         */



        // Declarations and Initializations
        // ---------------------------------
            // This will hold the minion's unique number (or id)
                private int number;
            // This variable will hold the component to attach the self-assigned number on it's back.
                public Text numText;

            // Accessors and Communication
                // Use the RNG from ProblemBox as contains the boundry limit.
                    public ProblemBox problemBox;
        // ----




        // Use this for initialization
        private void Awake()
        {
            // Initialize the component
                numText = GetComponentInChildren<Text>();
        } // Awake()



        // This script is called once, after the actor has been placed in the scene
        private void Start()
        {
            // Fetch a random number from the Problem Box script.
                number = problemBox.Access_GetRandomNumber();
            // Put the self-assigned unique number on the minion's back
                numText.text = number.ToString();
        } // Start()



        // Return the value of the minion's self-assigned number.
        public int MinionNumber
        {
            get { return number; }
        } // MinionNumber
    } // End of Class
} // Namespace