using UnityEngine;
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
