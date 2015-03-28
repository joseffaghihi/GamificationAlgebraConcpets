using UnityEngine;
using System.Collections;

public class WhatIsDisplay : MonoBehaviour
{

	public GameObject whatIsTextbox;
	public GameObject eventLetterTextbox;
	private Animator whatIsAnim;
	private Animator eventLetterAnim;

	void Awake()
    {
		whatIsAnim = whatIsTextbox.GetComponent<Animator>();
		eventLetterAnim = eventLetterTextbox.GetComponent<Animator>();
	}



	// Plays the what is "A, B, or C" animation
	public IEnumerator NextLetterEventPlay(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		whatIsAnim.SetTrigger("SlideIn");
		eventLetterAnim.SetTrigger("SlideIn");
	} // NextLetterEventPlay
}
