﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic; // For the 'List' support.

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
            // Quadratic Equation Filler [Meaningless; only used for displaying on screen.  Cache arrays]
                private List<int?> DEG_DisplayLeft = new List<int?>();
                private List<int?> DEG_DisplayRight = new List<int?>();
            // Random Number Generator (RNG) range [minimum, maximum]
                public int minValue,
                           maxValue;
            // Accessors and Communication
                private Text problemBox;
            // Complexity Level
                // False: No terms shift to the right, all terms stay on the left.
                // True: All terms can shift left or right
                    public bool complexLevel = false;
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



        // Initialize the required indexes that will be used for the index properties.
        private void InitializeIndexProp()
        {
            // Index A
                InitializeIndexProp_IndexProperties(index_A_Prop);
            // Index B
                InitializeIndexProp_IndexProperties(index_B_Prop);
            // Index C
                InitializeIndexProp_IndexProperties(index_C_Prop);

            // Cache List for the HUD
                InitializeIndexProp_DEGDisplay(DEG_DisplayLeft);
                InitializeIndexProp_DEGDisplay(DEG_DisplayRight);
        } // InitializeIndexProp()



        // Initialize the Term Properties
        private void InitializeIndexProp_IndexProperties(List<object> objList)
        {
            objList.Add((int)0);
            objList.Add((char)'X');
        } // InitializeIndexProp_IndexProperties()



        // Initialize the DEG_Display[Left|Right] list
        private void InitializeIndexProp_DEGDisplay(List<int?> intList, uint listSize = 3)
        {
            for(int i = 0; i < listSize; ++i)
                intList.Add(null);
        } // InitializeIndexProp_DEGDisplay()



        // Generate the Quadratic Equation
        private void Generate()
        {
            // Initialize the required lists
                InitializeIndexProp();
            // Generate the new equation indexes
                Generate_Indexes();
            // Translate the Indexes
                Generate_TranslateIndexes();
            // Sort the indexes in cached arrays
                Generate_DEGCacheSort();
            // Display the new equation
                Generate_Display_DEG();
            // Thrash Cache Array
                ThrashListCacheValues(DEG_DisplayLeft);
                ThrashListCacheValues(DEG_DisplayRight);
            // Thrash Index Array
                ThrashListIndexValues(index_A_Prop);
                ThrashListIndexValues(index_B_Prop);
                ThrashListIndexValues(index_C_Prop);
        } // Generate()




        // DYNAMIC EQUATION GENERATOR
        // ==================================================
        // --------------------------------------------------




        // Generate the Quadratic Equation Indexes
        private void Generate_Indexes()
        {
            // LIST FORMAT STANDARD
            // --------------------
            // Index 0: Value [int]
            // Index 1: Position (left or right of the equal sign) [char]
            // ==============================

            // Index A
                index_A_Prop[0] = ((int)GetRandomNumber(true));
                index_A_Prop[1] = ((char)GetRandomPosition());
            // Index B
                index_B_Prop[0] = ((int)GetRandomNumber());
                index_B_Prop[1] = ((char)GetRandomPosition());
            // Index C
                index_C_Prop[0] = ((int)GetRandomNumber());
                index_C_Prop[1] = ((char)GetRandomPosition());
        } // Generate_Indexes()



        // Sort the Dynamic Equation Generator in an array format; this will be eventually used to output to the screen.
        private void Generate_DEGCacheSort()
        {
            // Index: A
                if ((char)index_A_Prop[1] == (char)'L')
                {
                    // Left side of equals
                    DEG_DisplayLeft[0] = (int)index_A_Prop[0];
                    DEG_DisplayRight[0] = null;
                }
                else
                {
                    // Right side of equals
                    DEG_DisplayRight[0] = (int)index_A_Prop[0];
                    DEG_DisplayLeft[0] = null;
                }


            // Index: B
                if ((char)index_B_Prop[1] == (char)'L')
                {
                    // Left side of equals
                    DEG_DisplayLeft[1] = (int)index_B_Prop[0];
                    DEG_DisplayRight[1] = null;
                }
                else
                {
                    // Right side of equals
                    DEG_DisplayRight[1] = (int)index_B_Prop[0];
                    DEG_DisplayLeft[1] = null;
                }


            // Index: C
                if ((char)index_C_Prop[1] == (char)'L')
                {
                    // Left side of equals
                    DEG_DisplayLeft[2] = (int)index_C_Prop[0];
                    DEG_DisplayRight[2] = null;
                }
                else
                {  // Right side of equals
                    DEG_DisplayRight[2] = (int)index_C_Prop[0];
                    DEG_DisplayLeft[2] = null;
                }
        } // Generate_DEGCacheSort()



        // Try to put the cached lists together to be displayed on the screen
        private void Generate_Display_DEG()
        {
            // Evaluate the left side of the equation
                string displayCacheLeft = EvaluateIndexFields(DEG_DisplayLeft);
            // Evaluate the right side of the equation
                string displayCacheRight = EvaluateIndexFields(DEG_DisplayRight);

            // Display the data onto the dedicated Problem Box on the HUD
                problemBox.text = displayCacheLeft + " = " + displayCacheRight;

        } // Generate_Display_DEG()



        // Evaluate the index fields
        // This will pre-determine the possible combinations
        private string EvaluateIndexFields(List<int?> listIndexField)
        {
            //Check combinations for: Index A
            // Ax^2 + Bx + C
                if (listIndexField[0] != null && listIndexField[1] != null && listIndexField[2] != null)
                    return listIndexField[0].ToString() + "x^2" + " " + Generate_Display_OperatorSign(listIndexField[1]) + " " + Generate_Display_TranslateValues(listIndexField[1]) + "x" + " " + Generate_Display_OperatorSign(listIndexField[2]) + " " + Generate_Display_TranslateValues(listIndexField[2]);

            // Ax^2 + Bx
                else if (listIndexField[0] != null && listIndexField[1] != null)
                    return listIndexField[0].ToString() + "x^2" + " " + Generate_Display_OperatorSign(listIndexField[1]) + " " + Generate_Display_TranslateValues(listIndexField[1]) + "x";

            // Ax^2 + C
                else if (listIndexField[0] != null && listIndexField[2] != null)
                    return listIndexField[0].ToString() + "x^2" + " " + Generate_Display_OperatorSign(listIndexField[2]) + " " + Generate_Display_TranslateValues(listIndexField[2]);

            // Ax^2
                else if (listIndexField[0] != null)
                    return listIndexField[0].ToString() + "x^2";
            // -----------


            // Check combinations for: Index B
            // Bx + C
                if (listIndexField[1] != null && listIndexField[2] != null)
                    return listIndexField[1].ToString() + "x" + " " + Generate_Display_OperatorSign(listIndexField[2]) + " " + Generate_Display_TranslateValues(listIndexField[2]);
            // Bx
                else if (listIndexField[1] != null)
                    return listIndexField[1].ToString() + "x";
            // -----------


            // Check combinations for: Index C
            // c
                if (listIndexField[2] != null)
                    return listIndexField[2].ToString();
            // -----------

            // If nothing satisfies, then assume nothing in this side exists.
                return "0";
        } // EvaluateIndexFields()



        // Convert the operation sign (in the Quadratic Equation) to the desired sign.
        // For example, if the leading coefficent is a negative, then the 'plus' switches signs as negative.
        private char Generate_Display_OperatorSign(int? intNumber)
        {
            if (intNumber < 0)
                return '-';
            else
                return '+';
        } // Generate_Display_OperatorSign()



        // This method will turncate the negative number and output a 'positive' value; this function is to only
        private int? Generate_Display_TranslateValues(int? intNumber)
        {
            if (intNumber < 0)
                // Flip the negative number to a positive number; for display only that requires the negative to be turncated from the value specifically.
                return (intNumber * -1);
            else
                // Number is already positive.
                return intNumber;
        } // Generate_Display_TranslateValues()



        // Delete all values from the cache arrays
        private void ThrashListCacheValues(List<int?> intList)
        {
            for (int i = (intList.Count - 1); i >= 0; --i)
                intList.RemoveAt(i);
        } // ThrashArrayCacheValues()



        // Delete all values from the DEG Indexes
        private void ThrashListIndexValues(List<object> objList)
        {
            for (int i = (objList.Count - 1); i >= 0; --i)
                objList.RemoveAt(i);
        } // ThrashListIndexValues()



        // Translate the index properties into the index variables for ready use.
        // NOTE: Remember that the variables 'index_[A|B|C]' are for the minions for checking the answer.
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



        // Return what position the index is currently located
        private char GetRandomPosition()
        {
            // All terms can shift left or right
            if (complexLevel == true)
            {
                if (System.Convert.ToBoolean(UnityEngine.Random.Range(0, 2)))
                    // Left
                    return 'L';
                else
                    // Right
                    return 'R';
            }

            // All terms MUST remain on the left side
            else
                return 'L';
        } // GetRandomPosition()




        // END OF: DYNAMIC EQUATION GENERATOR
        // --------------------------------------------------
        // ==================================================




        // Return the range of the 'Random Number Generator' or simply the RNG of the Quadratic Equation.
        private int GetRandomNumber(bool preventNull = false)
        {
            int randNumber;

            // This loop is designed to prevent '0' value; this is controlled by the variable switch 'preventNull'.  By default, the arguement switch is off, but when enabled this will avoid a 0 return.
            do
            {
              randNumber = Random.Range(minValue, maxValue);
            } while (randNumber == 0 && preventNull != false);

            return randNumber;
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