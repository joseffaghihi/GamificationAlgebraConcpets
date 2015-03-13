using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventLetterChange : MonoBehaviour {

	/*
		This script sets the value of the EventLetter_Text object to the same letter as the letter_Text
	*/

	private Text ThisText;
	private GameObject referenceText;
	private Text referenceTextLetter;

	// Use this for initialization
	void Start () {
		ThisText = GetComponent<Text>();
		referenceText = GameObject.Find ("Letter_Text");
		if(referenceText == null)
		{
			Debug.Log ("No referenceText object was set.");
		}
		referenceTextLetter = referenceText.GetComponent<Text>();
		//ThisText.text = referenceTextLetter.text;
	}

	void Update()
	{
		ThisText.text = referenceTextLetter.text;
	}
}