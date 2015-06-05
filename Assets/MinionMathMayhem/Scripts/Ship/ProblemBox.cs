using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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
            // Quadratic Equation Indexes [Properties]
                private List<object> index_A_Prop = new List<object>();
                private List<object> index_B_Prop = new List<object>();
                private List<object> index_C_Prop = new List<object>();
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
            // Initialize Index Property Fields
                InitializeIndexProp();
            // Initialize the Quadratic Equation indexes
                Generate();
        } // Start()



        // Initialize the required indexes that will be used for the index properties.
        private void InitializeIndexProp()
        {
            // Index A
            index_A_Prop.Add((int)0);
            index_A_Prop.Add((char)'X');
            // Index B
            index_B_Prop.Add((int)0);
            index_B_Prop.Add((char)'X');
            // Index C
            index_C_Prop.Add((int)0);
            index_C_Prop.Add((char)'X');
        } // InitializeIndexProp()



        // Generate the Quadratic Equation
        private void Generate()
        {
            // Generate the new equation indexes
                Generate_Indexes();
            // Translate the Indexes
                Generate_TranslateIndexes();
            // Display the new equation
                Generate_Display();
        } // Generate()



        // Generate the Quadratic Equation Indexes
        private void Generate_Indexes()
        {
            // LIST FORMAT STANDARD
            // --------------------
            // Index 0: Value [int]
            // Index 1: Position (left or right of the equal sign) [char]
            // ==============================

            // Index A
                index_A_Prop[0] = ((int)GetRandomNumber());
                index_A_Prop[1] = ((char)GetRandomPosition());
            // Index B
                index_B_Prop[0] = ((int)GetRandomNumber());
                index_B_Prop[1] = ((char)GetRandomPosition());
            // Index C
                index_C_Prop[0] = ((int)GetRandomNumber());
                index_C_Prop[1] = ((char)GetRandomPosition());
        } // Generate_Indexes()



        // Display the Quadratic Equation on the HUD
        private void Generate_Display()
        {
            problemBox.text = index_A.ToString() + "x +" + index_B.ToString() + "x+" + index_C.ToString() + " =0";
            // Translate the Index Properties
                
        } // Generate_Display()



        // Translate the index properties into the index variables for ready use.
        private void Generate_TranslateIndexes()
        {
            if ((char)index_A_Prop[1] == (char)'R')
                index_A = -((int)index_A_Prop[0]);
            else
                index_A = ((int)index_A_Prop[0]);

            if ((char)index_B_Prop[1] == (char)'R')
                index_B = -((int)index_B_Prop[0]);
            else
                index_B = ((int)index_B_Prop[0]);

            if ((char)index_C_Prop[1] == (char)'R')
                index_C = -((int)index_C_Prop[0]);
            else
                index_C = ((int)index_C_Prop[0]);
        } // Generate_TranslateIndexes()



        // Return the range of the 'Random Number Generator' or simply the RNG of the Quadratic Equation.
        private int GetRandomNumber()
        {
            return Random.Range(minValue, maxValue);
        } // GetRandomNumber()



        // Return what position the index is currently located
        private char GetRandomPosition()
        {
            if (System.Convert.ToBoolean(UnityEngine.Random.Range(0, 2)))
                // Left
                return 'L';
            else
                // Right
                return 'R';
        } // GetRandomPosition()



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