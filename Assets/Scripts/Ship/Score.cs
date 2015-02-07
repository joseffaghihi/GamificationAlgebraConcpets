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
		//David > public reference to the winLoseTextbox added to the scene_________________________________________________DAVID
			public Text winLoseText;
    // -------------

	//Beginning of things added By David
	void Update()
	{
		if (scoreIncorrect >= 5)//______________________________________________________________________________________________DAVID
		{
			winLoseText.text = "You Fail!";
		}	else if (scoreCorrect >= 10)
			{
				winLoseText.text = "You Win!";
			}
	}//____________________________________________________________________________________________________DAVID



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
        scoreBox.text = "SCORE: " + scoreCorrect.ToString();
    } // End of UpdateScore



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
}