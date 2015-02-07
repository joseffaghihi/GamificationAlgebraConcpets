using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalDestroyer : MonoBehaviour
{
    // Link to other scripts
    public ProblemBox problemBox;
    public LetterBox letterbox;
    public MinionsSpawnWait minionSpawnWait;
    public Text letterBox;
    public Score score;
    //public Text scoreBox;
    // Toggle the minion spawners
    public bool activateSpawner;
    // Wait timer for the minion check
    public int waitTimer = 5;
    // Lock the function from executing; work around gross hack
    private bool lockCheckFunction;


    // ReadOnly Accessor; should the spawners be activated?
    public bool ActivateSpawner
    {
        // thanks to Bob for giving me the idea in his previous code.
        get { return activateSpawner; }
    } // End of ActivateSpawner



    // Initialize the declarations
    void Start()
    {
        activateSpawner = true;
        lockCheckFunction = false;
    } // End of Start



    // When the object hits the object, run the check.
    void OnTriggerEnter(Collider other)
    //Should this be a static function
    {
        if (lockCheckFunction == false)
        {
            CreatureIdentity id = other.gameObject.GetComponent<CreatureIdentity>();

            if (GetNumber() == id.Number)
                CorrectAnswer();
            else
                IncorrectAnswer();
        }

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
        // Update the score
        score.AccessUpdateScoreCorrect();
        // ----

        // Stop the spawners
        activateSpawner = false;

        // Lock the exit from further comparison
        lockCheckFunction = true;
        // ----


        // Murder the minions!
        MinionGenocide();
        // ----


        // Generate a new equation
        letterbox.Generate();


        // Timer to unlock the 'Lock Check Function' variable
        StartCoroutine(WaitTimer(10));
        //lockCheckFunction = false;
    } // End of CorrectAnswer



    // Once activated; this halts the action within the function before executing anything further.
    public IEnumerator WaitTimer(int waitTime)
    {
        // Waits for $waitTime seconds
        yield return new WaitForSeconds(waitTime);
        // Unlock the function [OnTriggerEvent]
        lockCheckFunction = false;
        // Resume the minion spawners
        activateSpawner = true;
    } // End of WaitTimer



    // When the user has the incorrect answer, this function will manage the routines
    private void IncorrectAnswer()
    {
        score.AccessUpdateScoreIncorrect();
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
        }

    } // End of MinionGenocide

} // End of Class



/*using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalDestroyer : MonoBehaviour
{
    int score = 0;
    int failure = 0;
	private Animator letterSwap;
    public ProblemBox problemBox;
    public LetterBox letterbox;
    public Text letterBox;
    public Text scoreBox;
	public Text winLoseText;
	public AudioSource audio;
	public AudioClip failSound;

	void Awake()
	{
		letterSwap = letterBox.GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider other)
	{
        CreatureIdentity id = other.gameObject.GetComponent<CreatureIdentity>();

        if(GetNumber() == id.Number)
        {
            score++;
            scoreBox.text = "SCORE: " + score.ToString();
            letterbox.Generate();
			problemBox.Generate();
			letterSwap.SetTrigger("LetterChange");
        }
        else
        {
            failure++;
            // update scoreboard
        }

		Destroy(other.gameObject);
	}

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
    }

	void Update()
	{
		if (failure >= 5)//______________________________________________________________________________________________DAVID
		{
			winLoseText.text = "You Fail!";
			audio.clip = failSound;
			//audio.Play ();
			//audio.loop = false;
		}	else if (score >= 10)
			{
				winLoseText.text = "You Win!";
			}	else
				{
					winLoseText.text = "";
				}
	}
}
*/