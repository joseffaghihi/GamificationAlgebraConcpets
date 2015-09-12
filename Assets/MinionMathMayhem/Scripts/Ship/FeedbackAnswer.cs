// This script supplies the fb_answer object with the correct number
// matching the A,B, or C in the quadratic equation textbox.
// Whichever one the top-right UI element is asking for, this script provides
// the answer

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FeedbackAnswer : MonoBehaviour {
	private Text text;
	private Text answerText;

	// This variable is a reference to the A,B, or C textbox
	public GameObject answerObject;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		answerText = answerObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		text = answerText;
	}
}
