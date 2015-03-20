using UnityEngine;
using UnityEngine.UI; // Include this so we can use the 'Text' functionality.
using System.Collections;

public class Score : MonoBehaviour
{

    /*                            SCORE
     * This class is designed to manage the general score of the game.
     * 
     * GOALS:
     *  Managing the scores
     *  Returning the score values (when called)
     */



    // Declarations and Initializations
    // ---------------------------------
        // Scores
            private int scoreCorrect = 0;
            private int scoreIncorrect = 0;

        // Accessors and Communication
            // HUD: Score box
                public Text scoreBox;
            // HUD: Wrong score box
                public Text wrongScoreBox;
    // ----




    // This function is immediately executed once the actor is in the game scene.
    private void Start()
    {
        // First make sure that all the scripts and actors are properly linked
            CheckReferences();
    } // Start()




    // Update the correct score
    private void UpdateScoreCorrect ()
    {
        scoreCorrect++;
        UpdateScoreDisplay(); // Update the score display   
    } // UpdateScore()



    // This function will only merely update the score canvas string that is being displayed in the scene as a HUD
    private void UpdateScoreDisplay()
    {
        scoreBox.text = "Score: " + scoreCorrect.ToString();
    } // UpdateScoreDisplay()



    // This function will update the wrong answer score canvas string that is on the HUD
    private void UpdateWrongScoreDisplay()
    {
        wrongScoreBox.text = "Oopsies: " + scoreIncorrect.ToString();
    } // UpdateWrongScoreDisplay()



    // Update the incorrect\fail score
    private void UpdateScoreIncorrect ()
    {
        scoreIncorrect++;
        UpdateWrongScoreDisplay();
    } // UpdateScoreIncorrect()



    // Reset Score function
    private void Reset()
    {
        // This function - will thrash the current scores
        scoreCorrect = 0;
        scoreIncorrect = 0;
        UpdateScoreDisplay();
        UpdateWrongScoreDisplay();
    } // ThrashScore()



    // Allow outside scripts to access the 'UpdateScoreCorrect' function.
    public void AccessUpdateScoreCorrect()
    {
        // Because the function is private and should remain this way, this function will kindly access that function and invoke it.
        UpdateScoreCorrect();
    } // AccessUpdateScoreCorrect()



    // Allow outside scripts to access the 'UpdateScoreIncorrect' function.
    public void AccessUpdateScoreIncorrect()
    {
        // Because the function is private and should remain this way, this function will kindly access that function and invoke it.
        UpdateScoreIncorrect();
    } // AccessUpdateScoreCorrect()



    // Access reset score function; as the function is set to 'private'
    public void AccessReset()
    {
        Reset();
    } // AccessThrashScores()



    // Return the correct score value to the calling script.
    public int ScoreCorrect
    {
        get { return scoreCorrect; }
    } // ScoreCorrect



    // Return the incorrect\fail score value to the calling script.
    public int ScoreIncorrect
    {
        get { return scoreIncorrect; }
    } // ScoreIncorrect



    // This function will check to make sure that all the references has been initialized properly.
    private void CheckReferences()
    {
        if (scoreBox == null)
            MissingReferenceError("Score");
        if (wrongScoreBox == null)
            MissingReferenceError("Tutorial");
    } // CheckReferences()



    // When a reference has not been properly initialized, this function will display the message within the console and stop the game.
    private void MissingReferenceError(string refLink = "UNKNOWN_REFERENCE_NOT_DEFINED")
    {
        Debug.LogError("Critical Error: Could not find a reference to [ " + refLink + " ]!");
        Debug.LogError("  Can not continue further execution until the internal issues has been resolved!");
        Time.timeScale = 0; // Halt the game
    } // MissingReferenceError()
} // End of Class