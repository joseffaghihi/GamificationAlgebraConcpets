using UnityEngine;
using UnityEngine.UI; // Include this so we can use the 'Text' functionality.
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class Score : MonoBehaviour
    {

        /*                            SCORE
         * This class is designed to manage the general score of the game.  This will hold the scores of the game of what the user got right and wrong.
         *  In addition, the scores are publicly available within the scope of the namespace.  Thus, the scores can be easily fetched throughout the entire game, whichother scripts can easily acess.
         * 
         * GOALS:
         *  Managing the scores
         *      Increment the correct or wrong score
         *      Reset or nullify the scores
         *  Returning the score values (when called)
         */



        // Declarations and Initializations
        // ---------------------------------
            // Scores
                private int scoreCorrect = 0;
                private int scoreIncorrect = 0;
                private double scoreCorrectPercent = 0.0;
                private double scoreIncorrectPercent = 0.0;

            // Accessors and Communication
                // HUD: Score box
                    public Text scoreBox;
                // HUD: Wrong score box
                    public Text wrongScoreBox;
                // Score Precentage
                    public Score_Precentage scriptScore_Percentage;
                // Game Controller
                    public GameController scriptGameController;
        // ----




        // Signal Listener: Detected
        private void OnEnable()
        {
            // Reset the scores
                GameController.GameStateRestart += Reset;
        } // OnEnable()



        // Signal Listener: Deactivate
        private void OnDisable()
        {
            // Reset the scores
                GameController.GameStateRestart -= Reset;
        } // OnDisable()



        // This function is immediately executed once the actor is in the game scene.
        private void Start()
        {
            // First make sure that all the scripts and actors are properly linked
                CheckReferences();
        } // Start()



        // Increment the 'Correct' score and display it on the screen.
        private void UpdateScoreCorrect()
        {
            // Increment the score of what the user got right.
                scoreCorrect++;
            // Update the 'Correct' score on the HUD
                UpdateScoreDisplay();   
            // Get the new percentage of the score
                UpdateScoreCorrect_Percentage();
        } // UpdateScore()



        // This function will only merely update the score canvas string that is being displayed in the scene as a HUD
        private void UpdateScoreDisplay()
        {
            scoreBox.text = "Score: " + scoreCorrect.ToString();
        } // UpdateScoreDisplay()



        // This function will update the incorrect score canvas string that is on the HUD
        private void UpdateWrongScoreDisplay()
        {
            wrongScoreBox.text = "Oopsies: " + scoreIncorrect.ToString();
        } // UpdateWrongScoreDisplay()



        // Increment the 'Incorrect' score and display it on the screen.
        private void UpdateScoreIncorrect()
        {
            // Increment the score of what the user got wrong.
                scoreIncorrect++;
            // Update the 'Incorrect' score on the HUD
                UpdateWrongScoreDisplay();
            // Get the new percentage of the score
                UpdateScoreIncorrect_Percentage();
        } // UpdateScoreIncorrect()



        // This function is designed to completely reset the entire scores kept within this script.
        private void Reset()
        {
            // Thrash the scores that is internally kept within the script.
                scoreCorrect = 0;
                scoreIncorrect = 0;
                scoreCorrectPercent = 0.0;
                scoreIncorrectPercent = 0.0;
            // Update the score on the HUD.
                UpdateScoreDisplay();
                UpdateWrongScoreDisplay();
        } // ThrashScore()



        /// <summary>
        ///     Update the correct answers percentage
        /// </summary>
        private void UpdateScoreCorrect_Percentage()
        {
            // Get the maximum score possible
            int maxScore = (int)scriptGameController.MaxScore;

            // Get the new precentage
            scoreCorrectPercent = scriptScore_Percentage.CalculateScorePercentageInterface(scoreCorrect, maxScore);
        } // UpdateScoreCorrect_Percentage()



        /// <summary>
        ///     Update the incorrect answers percentage
        /// </summary>
        private void UpdateScoreIncorrect_Percentage()
        {
            // Get the maximum incorrect score possible
            int maxIncorrect = (int)scriptGameController.maxScoreFail;

            // Get the new precentage
            scoreIncorrectPercent = scriptScore_Percentage.CalculateScorePercentageInterface(scoreIncorrect, maxIncorrect);
        } // UpdateScoreIncorrect_Percentage()



        // Allow outside scripts to access the 'UpdateScoreCorrect' function; which is a private function.
        public void AccessUpdateScoreCorrect()
        {
            UpdateScoreCorrect();
        } // AccessUpdateScoreCorrect()



        // Allow outside scripts to access the 'UpdateScoreIncorrect' function; which is a private function.
        public void AccessUpdateScoreIncorrect()
        {
            UpdateScoreIncorrect();
        } // AccessUpdateScoreCorrect()



        // Return the value of the score that the user got right, to the calling script.
        public int ScoreCorrect
        {
            get {
                    return scoreCorrect;
                } // get
        } // ScoreCorrect



        // Return the value of the score that the user got incorrect, to the calling script.
        public int ScoreIncorrect
        {
            get {
                    return scoreIncorrect;
                } // get
        } // ScoreIncorrect



        // Return the value of the correct score precentage to the calling script.
        public double ScoreCorrectPercent
        {
            get
            {
                return scoreCorrectPercent;
            } // get
        } // ScoreCorrectPercent



        // Return the value of the incorrect score precentage to the calling script.
        public double ScoreIncorrectPercent
        {
            get
            {
                return scoreIncorrectPercent;
            } // get
        } // ScoreIncorrectPercent



        // This function will check to make sure that all the references has been initialized properly.
        private void CheckReferences()
        {
            if (scoreBox == null)
                MissingReferenceError("Score Box [HUD]");
            if (wrongScoreBox == null)
                MissingReferenceError("Wrong Score Box [HUD]");
            if (scriptScore_Percentage == null)
                MissingReferenceError("Score Precentage Interface");
            if (scriptGameController == null)
                MissingReferenceError("Game Controller");
        } // CheckReferences()



        // When a reference has not been properly initialized, this function will display the message within the console and stop the game.
        private void MissingReferenceError(string refLink = "UNKNOWN_REFERENCE_NOT_DEFINED")
        {
            Debug.LogError("Critical Error: Could not find a reference to [ " + refLink + " ]!");
            Debug.LogError("  Can not continue further execution until the internal issues has been resolved!");
            Time.timeScale = 0; // Halt the game
        } // MissingReferenceError()
    } // End of Class
} // Namespace