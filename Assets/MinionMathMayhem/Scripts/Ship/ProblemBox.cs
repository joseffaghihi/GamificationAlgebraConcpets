using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class ProblemBox : MonoBehaviour
    {

        /*                    PROBLEM BOX
         * This class will simply generate the quadratic equation by using the RNG within the given range specified within the inspector.
         *  
         * GOALS:
         *  Generate the quadratic equation indexes
         *  Return a random number that requires the RNG boundary
         */



        // Declarations and Initializations
        // ---------------------------------
            // Quadratic Equation Indexes
                private int index_A,
                            index_B,
                            index_C;
            // Random Number Generator (RNG) range [minimum, maximum]
                public int minValue,
                           maxValue;

            // Accessors and Communication
                private Text problemBox;
        // ----



        // Use this for initialization
        private void Awake()
        {
            // Reference initialization
                problemBox = GetComponent<Text>();
        } // Awake()



        // This script is called once, after the actor has been placed in the scene
        private void Start()
        {
            // Initialize the Quadratic Equation indexes
                Generate();
        } // Start()



        // Generate the Quadratic Equation
        private void Generate()
        {
            // Generate the new equation indexes
                Generate_Indexes();
            // Display the new equation
                Generate_Display();
        } // Generate()



        // Generate the Quadratic Equation Indexes
        private void Generate_Indexes()
        {
            index_A = GetRandomNumber();
            index_B = GetRandomNumber();
            index_C = GetRandomNumber();
        } // Generate_Indexes()



        // Display the Quadratic Equation on the HUD
        private void Generate_Display()
        {
            problemBox.text = index_A.ToString() + "x +" + index_B.ToString() + "x+" + index_C.ToString();
        } // Generate_Display()



        // Return the range of the 'Random Number Generator' or simply the RNG of the Quadratic Equation.
        private int GetRandomNumber()
        {
            return Random.Range(minValue, maxValue);
        } // GetRandomNumber()



        // This function will call the RNG function (which is private) and return the value to the outside class
        public int Access_GetRandomNumber()
        {
            return (GetRandomNumber());
        } // Access_GetRandomNumber()



        // This function will call the Quadratic Equation Generator, as it is set to private.
        public void Access_Generate()
        {
            Generate();
        } // Access_Generate()



        // Returning Quadratic Equation Index: A
        public int Index_A
        {
            get {
                    return index_A;
                } // get
        } // Index_A



        // Returning Quadratic Equation Index: B
        public int Index_B
        {
            get {
                    return index_B;
                } // get
        } // Index_B



        // Returning Quadratic Equation Index: C
        public int Index_C
        {
            get {
                    return index_C;
                } // get
        } // Index_C
    } // End of Class
} // Namespace