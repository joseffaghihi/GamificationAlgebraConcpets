using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FeedbackLetterText : MonoBehaviour {
	private string text;
	private string answerText;
	
	// This variable is a reference to the A,B, or C textbox
	public GameObject answerObject;
	
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>().text;
		answerText = answerObject.GetComponent<Text>().text;
	}
	
	// Update is called once per frame
	// Sets answer text
	void Update () {
		text.text = answerText.text;
	}
}