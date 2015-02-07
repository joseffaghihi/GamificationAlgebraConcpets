using UnityEngine;
using UnityEngine.UI; // Include this so we can use the 'Text' functionality.
using System.Collections;

public class Score : MonoBehaviour
{
    /*                            SCORE
     * This class is designed to manage the general score of the game.
     */



    // Declarations
    // -------------
        // Scores
            private int scoreCorrect;
            private int scoreIncorrect;
        // References
            public Text scoreBox; // Directly link to the score box gameobject
    // -------------



	// Use this for initialization
	private void Start ()
    {
	    // Initialization of the score
        scoreCorrect = 0;
        scoreIncorrect = 0;
	} // End of Start
	


    // Update the correct score
    private void UpdateScoreCorrect ()
    {
        scoreCorrect++;
        UpdateScoreDisplay(); // Update the score display   
    } // End of UpdateScore



    // This function will only merely update the score canvas string that is being displayed in the scene as a HUD
    private void UpdateScoreDisplay()
    {
        scoreBox.text = "SCORE: " + scoreCorrect.ToString();
    } // End of UpdateScoreDisplay



    // Update the incorrect\fail score
    private void UpdateScoreIncorrect ()
    {
        scoreIncorrect++;
    } // End of UpdateScoreIncorrect



    // Allow outside scripts to access the 'UpdateScoreCorrect' function.
    public void AccessUpdateScoreCorrect()
    {
        // Because the function is private and should remain this way, this function will kindly access that function and invoke it.
        UpdateScoreCorrect();
    } // End of AccessUpdateScoreCorrect



    // Allow outside scripts to access the 'UpdateScoreIncorrect' function.
    public void AccessUpdateScoreIncorrect()
    {
        // Because the function is private and should remain this way, this function will kindly access that function and invoke it.
        UpdateScoreIncorrect();
    } // End of AccessUpdateScoreCorrect



    // Return the correct score value to the calling script.
    public int ScoreCorrect
    {
        get { return scoreCorrect; }
    } // End of ScoreCorrect



    // Return the incorrect\fail score value to the calling script.
    public int ScoreIncorrect
    {
        get { return scoreIncorrect; }
    } // End of ScoreIncorrect



    // Reset Score function
    private void ThrashScores()
    {
        // This function - will thrash the current scores
        scoreCorrect = 0;
        scoreIncorrect = 0;
        UpdateScoreDisplay();
    } // End of ThrashScore



    // Access reset score function; as the function is set to 'private'
    public void AccessThrashScores()
    {
        ThrashScores();
    } // End of AccessThrashScores
}