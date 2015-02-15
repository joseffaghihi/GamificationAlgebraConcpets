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
        number = problemBox.GetRandomNumber();
        // Initialization for using the text on the minion's back.
        Text numText = GetComponentInChildren<Text>();
        // Put the self-assigned unique number on the minion's back
        numText.text = number.ToString();
    } // End of Awake



    void Start()
    {

    }



    // Return the value of the minion's self-assigned number.
    public int Number
    {
        get { return number; }
    } // End of Number












    /*

     //
     // DEPENDENCY FILES:
     //   Driver Manager [SCENE01]
     //


    // Declarations and Initializations
    // ---------------------------------
    // Access GameObject's Scripts
    // Create a link to the Driver Manager
    public DriverManager accessDriverManager;
    public GameObject DriverManagerObject;
    // Declarations
    // Debug mode
    private bool debugMode = true;
    // This variable is used to cache the creature's unique self-assigned number
    private int creatureNumber = -1;
    // Text Canvas text buffer declaration; need further documentation
    private Text textEditor;
    // ----



    // Before the instance of the creature is made, immediately execute this function.
    private void Awake()
    {
        // Declarations
        // ----
        // Initialize the link to the Driver Manager by finding an object with the tag 'MainDriver' which /should/ be attached to the Main Driver.
        accessDriverManager = GameObject.FindGameObjectWithTag("MainDriver").gameObject.GetComponent<DriverManager>();
        // Initialize the text buffer component.
        textEditor = GetComponentInChildren<Text>();
        // ----
    } // End of Awake



    // Use this for initialization
    void Start()
    {
        creatureNumber = CreatureNumber_Handler();
        DisplayNumber();
    } // End of Start



    // This function is the algorithm for the creature to self-assign a unique number.
    private int CreatureNumber_Handler()
    {
        // ----
        // Declarations that will be used within this function alone
        int returnValue; // This variable will house the actual creature's unique number from the RNG
        int rangeMinimum; // Minimum range possible
        int rangeMaximum; // Maximum range possible
        // -----

        // Fetch the minimum range
        rangeMinimum = FetchNumberBoundry(false);
        // Debug message
        if (debugMode == true)
            Debug.Log("Creature Min: " + rangeMinimum);

        // Fetch the maximum range
        rangeMaximum = FetchNumberBoundry(true);
        // Debug message
        if (debugMode == true)
            Debug.Log("Creature Max: " + rangeMaximum);

        // Generate a self-assigned unique number
        returnValue = CreatureNumber_RNG(rangeMinimum, rangeMaximum);
        // Debug message
        if (debugMode == true)
            Debug.Log("Creature Number: " + returnValue);
        // Done
        return returnValue;
    } // End of CreatureNumber_Handler



    // This function will only fetch the minimum and maximum boundry range and report back to the caller function.
    private int FetchNumberBoundry(bool mode)
    {
        return accessDriverManager.ReturnRNGRange(mode);
    } // End of FetchNumberBoundry



    // Assign the creature a randomized number
    private int CreatureNumber_RNG(int minimum, int maximum)
    {
        // Self-Assign its own unique number
        int cache = Random.Range(minimum, maximum);
        // Return the value
        return cache;
    } // End of CreatureNumber_RNG



    // This function will return the creatures current unique number to the calling script
    public int CreatureNumber_Identify()
    {
        return creatureNumber;
    } // End of CreatureNumber_Identify



    // -----
    //public GameObject creature;
    public void DisplayNumber()
    {
        textEditor.text = "" + creatureNumber;
    } // End of DisplayNumber
    */

} // End of Class