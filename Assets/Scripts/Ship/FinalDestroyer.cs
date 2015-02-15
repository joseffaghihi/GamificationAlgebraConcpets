using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalDestroyer : MonoBehaviour
{

    /*                      FINAL DESTROYER
     * This class handles with determining if the minion's that collided with the 'exit' actor, is the correct or incorrect answer to the given problem.
     *  If the answer is correct, this script will handle with the routine algorithm of updating the game state.
     *  
     * GOALS:
     *  Check the minion's self-assigned number and compare against the current quadratic equation index
     *  Manage when the equation should be created
     *  Determining if the answer is correct or incorrect
     *  Toggle the spawner's; given in specific condition
     *  Clear scene of the minions
     */



    // Declarations
    // -------------
    // Link to other scripts
        public ProblemBox problemBox;
        public LetterBox letterbox;
        public MinionsSpawnWait minionSpawnWait;
        public Text letterBox;
	    public Text whatIsText;
        public Score score;
        public GameState gameState;
    // Toggle the minion spawner's
        public bool activateSpawner;
    // Wait timer for the minion check
        public int waitTimer = 5;
    // Lock the function from executing; work around gross hack
        private bool lockCheckFunction;
    // Sounds
	    public AudioSource gameSounds;
	    public AudioClip failSound;
	    public AudioClip successSound;
	    public AudioClip gameOverSound;
    // Animations
	    private Animator letterBoxController;
    // ----



    // ReadOnly Accessors; should the spawner's be activated?
    public bool ActivateSpawner
    {
        // thanks to Bob for giving me the idea in his previous code.
        get { return activateSpawner; }
    } // End of ActivateSpawner



    // Initialize the declarations
    void Start()
    {
        // Turn on the spawner's
        activateSpawner = true;
        // Turn off the lock
        lockCheckFunction = false;
        // Reference initialization
		letterBoxController = letterBox.GetComponent<Animator>();
    } // End of Start



    // When the object hits the object, run the check.
    void OnTriggerEnter(Collider other)
    {
        if (lockCheckFunction == false)
        {
            // Fetch the minion's uniquely assigned number; this will be used for comparison.
            CreatureIdentity id = other.gameObject.GetComponent<CreatureIdentity>();

            // If the answer is correct
            if (GetNumber() == id.Number)
                // Answer is correct
                CorrectAnswer();
            else
                // Answer is not correct
                IncorrectAnswer();
        } // End If

        // Destroy the game object when they activate this trigger
        Destroy(other.gameObject);
    } // End of OnTriggerEnter



    // When invoked, this will return the current Quadratic index used in the Letter Box (from Canvas)
    int GetNumber()
    {
        switch (letterBox.text)
        {
            case "A":
                return problemBox.a;
            case "B":
                return problemBox.b;
            default:
                return problemBox.c;
        }
    } // End of GetNumber



    // When the player has the right answer, this function will house the algorithm
    private void CorrectAnswer()
    {
		// Plays the 'successSound' if the right minion goes through________________________DAVID
		audio.clip = successSound;
		audio.Play ();
		// ----

        // Update the score
        score.AccessUpdateScoreCorrect();
        // ----

        // Stop the spawner's
        activateSpawner = false;

        // Lock the exit from further comparison
        lockCheckFunction = true;
        // ----


        // Murder the minions!
        MinionGenocide();
        // ----


        // Check the game state to see if the game is over or still on going
        StartCoroutine(waitTimer_GameStateCheck(1));
        // ----


        // Timer to unlock the 'Lock Check Function' variable
        StartCoroutine(WaitTimer(waitTimer + 1));
        // ----

    } // End of CorrectAnswer



    // Once activated; this halts the action within the function before executing anything further.
    private IEnumerator WaitTimer(int waitTime)
    {
        // Waits for $waitTime seconds
        yield return new WaitForSeconds(waitTime);
        // Unlock the function [OnTriggerEvent]
        lockCheckFunction = false;
        // Resume the minion spawner's
        activateSpawner = true;
    } // End of WaitTimer



    // Pause the script to wait for the game over state to catch up
    private IEnumerator waitTimer_GameStateCheck(int waitCheck)
    {
        yield return new WaitForSeconds(waitCheck);

        // If the game is not over, continue on generating a new equation and the rest of the algorithm
        if (gameState.GameStateOver == false)
        {
            // Generate a new equation
            problemBox.Generate();
            letterbox.Generate();
            // ----


            // Notify the user of index update
            letterBoxController.SetTrigger("LetterChange");
            whatIsText.animation.Play();
        } // End If
    } // End of Pause_GameOverCheck



    // When the user has the incorrect answer, this function will manage the routines
    private void IncorrectAnswer()
    {
        // Update the incorrect score
        score.AccessUpdateScoreIncorrect();

		// Plays the 'failSound' if the wrong minion goes through________________________DAVID
		audio.clip = failSound;
		audio.Play ();
		// ----

    } // End of IncorrectAnswer



    // Kill all minions in the scene
    private void MinionGenocide()
    {
        // Run a detection of the minions in the scene
        if (minionSpawnWait.CheckMinions() == true)
        {
            // Fetch all of the minions in one array
            GameObject[] minionsInScene = GameObject.FindGameObjectsWithTag("Minion");
            // Kill them
            for (int i = 0; i < minionsInScene.Length; i++)
                DestroyObject(minionsInScene[i]);
        } // End If

    } // End of MinionGenocide



    // As the 'MinionGenocide' is a private function, kindly just call it for the outside scripts.
    public void AccessMinionGenocide()
    {
        MinionGenocide();
    } // End of AccessMinionGenocide

} // End of Class