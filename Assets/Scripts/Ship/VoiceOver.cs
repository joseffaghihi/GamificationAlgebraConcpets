using UnityEngine;
using System.Collections;

public class VoiceOver : MonoBehaviour {

    public AudioClip[] voiceOver = new AudioClip[10];

	// Use this for initialization
	void Start () {
        StartCoroutine(PlayTutorial()); // plays tutorial clips in array only once
	}	

   IEnumerator PlayTutorial() // Plays audio for tutorial clips
    {
       foreach (AudioClip tutorialClip in voiceOver)
       {
           audio.clip = tutorialClip;
           audio.Play();
           yield return new WaitForSeconds(tutorialClip.length);
       }
    }
}
