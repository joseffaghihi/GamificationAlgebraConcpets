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
    // Quadratic Equation Index Address
        private Text letterBox;
    // ----



    // Use this for initialization
    private void Awake()
    {
        // Reference initialization
            letterBox = GetComponent<Text>();
    } // Awake()



	// Use this for initialization
	private void Start ()
    {
        // Select an index address
            Generate();
	} // Start()



    // Select an index of the Quadratic Equation
    //  Example of Indexes: ( Ax^2 + Bx + C )
    private void Generate()
    {
        // Use the randomized var to choose which index to select
        switch (Random.Range(1, 4))
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
                letterBox.text = "ERR!"; // Display that there is an error in the box [NG]
                Debug.LogError("!ERROR!: Failed to generate a legal letter for variable [ letterBox.text ]"); // Show that there was an error in the console [NG]
                break;
        } // Switch
    } // Generate()



    // This function will call the generater function (which is private).
    public void Access_Generate()
    {
        Generate();
    } // Access_Generate()

} // End of Class
