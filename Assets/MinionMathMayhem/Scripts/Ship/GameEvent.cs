using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameEvent : MonoBehaviour
{
        /*                      GAME EVENT
         * Within this script, this will manage the game flow and game attributes dynamically.
         *     This will check if the user has the correct answer, toggle the score value (mainly sending you a signal),
         *     clear the scene by expunging all of the actors within the scene, and anything else that has value within
         *     the gameplay aspect.
         * 
         * GOALS:
         *  Check if the user has the correct or incorrect answer.
         *  Send a signal to update the score
         *  Send a signal to thrash the scene of all actors
         */



    // Declarations and Initializations
    // ---------------------------------
        // Sounds
            // Game Sounds
                public AudioSource gameSounds;
            // Incorrect Answer
                public AudioClip failSound;
            // Correct Answer
                public AudioClip successSound;
            // Game Over
                public AudioClip gameOverSound;
        // Animations
            private Animator letterBoxController;
            private Animator eventLetterAnim; // will control the animations of the 'what is' [DC]
        // GameObjects
            // Letter Box Texting UI
                public Text letterBox;
            // Quadratic Equation Updated; 'What Is' message
                public GameObject msgWhatIs;
            // Event Letter Change
                public GameObject EventLetterChange;
        // Accessors and Communication
            // Final Destroyer
                public FinalDestroyer scriptFinalDestroyer;
            // Quadratic Equation Index Letter Box
                public LetterBox scriptLetterBox;
            // Quadratic Equation Problem Box
                public ProblemBox scriptProblemBox;
            

            // Update the scores
                public delegate void UpdateScore_Correct();
                public static event UpdateScore_Correct UpdateScoreCorrect;
                // --
                public delegate void UpdateScore_Incorrect();
                public static event UpdateScore_Incorrect UpdateScoreIncorrect;
    // ----




    // Specialized initialization
    private void Awake()
    {
        eventLetterAnim = msgWhatIs.GetComponent<Animator>(); // finds the whatis text G.O. and gets the animator.
    } // Awake()



    // This function is immediately executed once the actor is in the game scene.
    private void Start()
    {
        // First make sure that all the scripts and actors are properly linked
            CheckReferences();
        // Reference initialization
            letterBoxController = letterBox.GetComponent<Animator>();
    } // Start()
    


    // Consistently check the minion actors that has reached the exit map spot
    private void Driver()
    {
        if (scriptFinalDestroyer.ActorIdentity == scriptFinalDestroyer.ActorIdentity)
            // Correct Answer
            AnswerCorrect();
        else
            // Incorrect Answer
            AnswerIncorrect();
    } // Driver()



    // When the user has the correct answer, this function will be executed
    private void AnswerCorrect()
    {

    } // AnswerCorrect()



    // When the user has the incorrect answer, this function will be executed
    private void AnswerIncorrect()
    {

    } // AnswerIncorrect()



    // When invoked, this will return the current Quadratic index used in the Letter Box
    private int GetQuadraticEquation_Index()
    {

        switch (scriptLetterBox.Access_SelectedIndex)
        {
            case 'A':
                return scriptProblemBox.Index_A;
            case 'B':
                return scriptProblemBox.Index_B;
            case 'C':
                return scriptProblemBox.Index_C;
            default:
                return 9999;
        } // Switch
    } // GetQuadraticEquation_Index()



    // This function will check to make sure that all the references has been initialized properly.
    private void CheckReferences()
    {
        if (scriptFinalDestroyer == null)
            MissingReferenceError("Final Destroy");
        if (scriptLetterBox == null)
            MissingReferenceError("Letter Box");
        if (scriptProblemBox == null)
            MissingReferenceError("Problem Box");
        if (msgWhatIs == null)
            MissingReferenceError("What Is [object]");
        if (EventLetterChange == null)
            MissingReferenceError("Event Letter Change");
    } // CheckReferences()



    // When a reference has not been properly initialized, this function will display the message within the console and stop the game.
    private void MissingReferenceError(string refLink = "UNKNOWN_REFERENCE_NOT_DEFINED")
    {
        Debug.LogError("Critical Error: Could not find a reference to [ " + refLink + " ]!");
        Debug.LogError("  Can not continue further execution until the internal issues has been resolved!");
        Time.timeScale = 0; // Halt the game
    } // MissingReferenceError()
} // End of Class
