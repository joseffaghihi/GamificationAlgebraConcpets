using UnityEngine;
using UnityEngine.UI; // To use the 'text' type, we must include this
using System.Collections;

public class LetterBox : MonoBehaviour
{

        /*                         LETTER BOX
         * Within the Letter Box class, this will automatically - upon called - randomly select which index position to select.
         *  Example: Indexes within the Quadratic Equation is: A, B, and C.  As follows: Ax^2 + Bx + C.  This script will only select the index, but nothing further.
         * 
         * Goals:
         *      Randomly select an index position of the quadratic equation.
         */



    // Declarations and Initializations
    // ---------------------------------
    // References
        Text letterBox;
    // ----



    void Awake()
    {
        // Initialize the reference
        letterBox = GetComponent<Text>();
    } // End of Awake



	// Use this for initialization
	void Start ()
    {
        // Select an index
        Generate();
	} // End of Start



    // Select an index of the Quadratic Equation
    //  Example of Indexes: ( Ax^2 + Bx + C )
    public void Generate()
    {
        // Randomize variable
        int r = Random.Range(1, 4);

        // Use the randomized var to choose which index to select
        switch(r)
        {
            case 1:
                letterBox.text = "A";
                break;
            case 2:
                letterBox.text = "B";
                break;
            case 3:
                letterBox.text = "C";
                break;
            default:
                letterBox.text = "ZZ";
                break;
        } // End Switch
    } // End of Generate

} // End of Class
