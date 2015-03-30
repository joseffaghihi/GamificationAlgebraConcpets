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
        // Spawner Toggle
            private bool SpawnMinions;
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
            // Scores
                public Score scriptScore;
            // Game Controller
                public GameController scriptGameController;
			// What-Is Object
				private WhatIsDisplay whatIsDisplay; // [DC]
            // What-Is Index Object
				private Animator eventLetterAnim; // [DC]
            // ----




    // Signal Listener: Detected
    private void OnEnable()
    {
        FinalDestroyer.GameEventSignal += Driver;
    } // OnEnable()



    // Signal Listener: Deactivate
    private void OnDisable()
    {
        FinalDestroyer.GameEventSignal -= Driver;
    } // OnDisable()



    // Specialized initialization
    private void Awake()
    {
        // Event Letter Animations
            eventLetterAnim = msgWhatIs.GetComponent<Animator>(); // finds the whatis text G.O. and gets the animator.
    } // Awake()



    // This function is immediately executed once the actor is in the game scene.
    private void Start()
    {
        // Reference initialization
            letterBoxController = letterBox.GetComponent<Animator>();
            whatIsDisplay = GetComponent<WhatIsDisplay>();
        // First make sure that all the scripts and actors are properly linked
            CheckReferences();	
    } // Start()
    


    // Consistently check the minion actors that has reached the exit map spot
    private void Driver()
    {
        if (scriptFinalDestroyer.ActorIdentity == GetQuadraticEquation_Index())
            // Correct Answer
            StartCoroutine(AnswerCorrect());
        else
            // Incorrect Answer
            AnswerIncorrect();
    } // Driver()



    // When the user has the correct answer, this function will be executed
    private IEnumerator AnswerCorrect()
    {
        // Play sounds
            AnswerCorrect_Sounds();
        // Update the score
            AnswerCorrect_UpdateScore();
        // Pause the spawners
            SpawnerToggleValue();
        // Murder the minions!
            MinionGenocide();
        // Slight pause
            yield return (WaitTimer(2));
        // Generate a new quation
            AnswerCorrect_Generate();
        // Animations
            AnswerCorrect_FinalAnimations();
		// Display the 'What-is' messages
            whatIsDisplay.Access_NextLetterEventPlay(0f); // [DC]
        yield return null;
    } // AnswerCorrect()



    // When the answer was correct, play some sounds.
    private void AnswerCorrect_Sounds()
    {
        GetComponent<AudioSource>().clip = successSound; 
        GetComponent<AudioSource>().Play (); 
    } // AnswerCorrect_Sounds()



    // When the answer was correct, update the score board
    private void AnswerCorrect_UpdateScore()
    {
        scriptScore.AccessUpdateScoreCorrect();
    } // AnswerCorrect_UpdateScore()



    // When the answer was correct, temporarily stop the spawner
    private void MinionGenocide()
    {
        if (MinionGenocide_CheckMinions() != false)
        {
            // Fetch all of the minions in one array 
                GameObject[] minionsInScene = GameObject.FindGameObjectsWithTag("Minion");
            // Kill them 
                for (int i = 0; i < minionsInScene.Length; i++)
                    DestroyObject(minionsInScene[i]);
        } // if

    } // AnswerCorrect_MinionGenocide()



    // Check all actors within the scene and find an actor with tag 'Minion'
    private bool MinionGenocide_CheckMinions()
    {
        // Find 'any' GameObject that has the tag 'Minion' attached to it.
        if (GameObject.FindGameObjectWithTag("Minion") == null)
            // If there is no minions in the scene
            return false;
        else
            // There is minions in the scene
            return true;
    } // MinionGenocide_CheckMinions()



    // Generate a new quadratic equation
    private void AnswerCorrect_Generate()
    {
        // If the game is not over, generate a new equation
        if (scriptGameController.GameOver == false)
        {
            // Generate a new equation
                scriptProblemBox.Access_Generate();
                scriptLetterBox.Access_Generate();
            // Notify the user of index update
                letterBoxController.SetTrigger("LetterChange");
                //msgWhatIs.GetComponent<Animation>().Play();
        } // If
    } // AnswerCorrect_Generate()



    // A temporary pause function
    // This is useful for merely doing a temporary pause or wait within the code execution.
    private IEnumerator WaitTimer(float time)
    {
        yield return new WaitForSeconds(time);
    } // WaitTimer()



    // When the game starts once the tutorial has finished, this script will run any actions required when the game begins.
    private IEnumerator FirstRun_Animations()
    {
        // Animations
            AnswerCorrect_FinalAnimations();
        // Notify the user of index update
            letterBoxController.SetTrigger("LetterChange");
            whatIsDisplay.Access_NextLetterEventPlay(0f); // [DC] Display the index letter
        yield return new WaitForSeconds(2f);
    } // FirstRun_Animations()



    // This function will kindly access the FirstRun_Animation, due to the protection level.
    public void Access_FirstRun_Animations()
    {
        StartCoroutine(FirstRun_Animations());
    } // Access_FirstRun_Animations()



    // Whatis Text Animations
    private void AnswerCorrect_FinalAnimations()
    {
        eventLetterAnim.SetTrigger("SlideIn");
    } // AnswerCorrect_FinalAnimations()



    // When the user has the incorrect answer, this function will be executed
    private void AnswerIncorrect()
    {
        // Update the score
            scriptScore.AccessUpdateScoreIncorrect();
        // Play Sounds
            AnswerIncorrect_Sounds();
    } // AnswerIncorrect()



    // When the answer is incorrect, this will play a sound clip.
    private void AnswerIncorrect_Sounds()
    {
        GetComponent<AudioSource>().clip = failSound;
        GetComponent<AudioSource>().Play();
    } // AnswerIncorrect_Sounds()



    // This function is only going to flip the bit of the Spawner value.
    private void SpawnerToggleValue()
    {
        SpawnMinions = !SpawnMinions;
    } // SpawnerToggleValue()



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



    // Return the value of the Spawners behavior; should they be on or off at this time.
    public bool AccessSpawnMinions
    {
        get { return SpawnMinions; }
    } // AccessSpawnMinions



    // Access the MinionGenocide function as it is a private function; this will allow other scripts to call the desired function.
    public void Access_MinionGenocide()
    {
        MinionGenocide();
    } // AccessMinionGenocide()



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
        if (scriptScore == null)
            MissingReferenceError("Scores");
        if (scriptGameController == null)
            MissingReferenceError("Game Controller");
        if (letterBoxController == null)
            MissingReferenceError("Letter Box Controller");
        if (eventLetterAnim == null)
            MissingReferenceError("Event Letter Animation");
        if (letterBox == null)
            MissingReferenceError("Letter Box [text]");
        if (whatIsDisplay == null)
            MissingReferenceError("What Is Display Object");
    } // CheckReferences()



    // When a reference has not been properly initialized, this function will display the message within the console and stop the game.
    private void MissingReferenceError(string refLink = "UNKNOWN_REFERENCE_NOT_DEFINED")
    {
        Debug.LogError("Critical Error: Could not find a reference to [ " + refLink + " ]!");
        Debug.LogError("  Can not continue further execution until the internal issues has been resolved!");
        Time.timeScale = 0; // Halt the game
    } // MissingReferenceError()
} // End of Class
