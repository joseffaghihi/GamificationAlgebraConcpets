using UnityEngine;
using UnityEngine.UI; // Used for 'text' type
using System.Collections;

public class CreatureIdentity : MonoBehaviour
{

    /*                      CREATURE IDENTITY
     * This class is designed for the minion to self generate its own randomized number given by a specific range.
     *  This essentially gives the minion its own unique number that they choose.
     * 
     * GOALS:
     *  Self-Generate a number
     */



    // Declarations
    // -------------
    private int number;
    //private Text numText;
    public ProblemBox problemBox;
    


    void Awake()
    {
        // Fetch a random number from the Problem Box script.
        number = problemBox.Access_GetRandomNumber();
        // Initialization for using the text on the minion's back.
        Text numText = GetComponentInChildren<Text>();
        // Put the self-assigned unique number on the minion's back
        numText.text = number.ToString();
    } // End of Awake



    // Return the value of the minion's self-assigned number.
    public int Number
    {
        get { return number; }
    } // End of Number

} // End of Class