using UnityEngine;
using System.Collections;

public class WhatIsDisplay : MonoBehaviour {

	public GameObject whatIsTextbox;
	public GameObject eventLetterTextbox;
	private Animator whatIsAnim;
	private Animator eventLetterAnim;

	void Awake() {
		whatIsAnim = whatIsTextbox.GetComponent<Animator>();
		eventLetterAnim = eventLetterTextbox.GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	// Plays the what is "A, B, or C" animation
	public void NextLetterEventPlay()
	{
		whatIsAnim.SetTrigger("SlideIn");
		eventLetterAnim.SetTrigger("SlideIn");
	} // NextLetterEventPlay
}
