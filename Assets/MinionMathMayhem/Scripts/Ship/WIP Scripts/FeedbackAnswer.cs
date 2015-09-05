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
		text = GetComponent<Text>().text;
		answerText = answerObject.GetComponent<Text>().text;
	}
	
	// Update is called once per frame
	void Update () {
		text = answerText;
	}
}
