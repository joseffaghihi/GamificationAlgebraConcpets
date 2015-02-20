using UnityEngine;
using System.Collections;

public class VoiceOver : MonoBehaviour
{

    // Declarations and Initializations
    // ---------------------------------
    public AudioClip[] voiceOver = new AudioClip[10];
    // This variable triggers the game state of wither or not the tutorial is activated.
    private bool tutorialMode;
    // Skip Signal
    private bool skip = false;
    // ----



	// Use this for initialization
    void Start()
    {
        tutorialMode = true; // We're in tutorial mode right now.
        StartCoroutine(PlayTutorial()); // plays tutorial clips in array only once
    } // End of Start



   IEnumerator PlayTutorial() // Plays audio for tutorial clips
    {
       foreach (AudioClip tutorialClip in voiceOver)
       {
           // Run tutorial
           if (skip == false)
           {
               audio.clip = tutorialClip;
               audio.Play();
               yield return new WaitForSeconds(tutorialClip.length);
           }
           
           // Skip tutorial; wait for 2 seconds for prepare time.
           else
               yield return new WaitForSeconds(2);
       } // End foreach

       // turn off the tutorial mode
       tutorialMode = false;

    } // End of PlayTutorial



    // This function, when called by another skip, will toggle the tutorial skip variable.
    public void ToggleSkip(bool toggle)
   {
       if (toggle == true)
           skip = true;
       else
           skip = false;
   } // End of ToggleSkip



    //Accessor; allow scripts to access this function to fetch the variable value of 'Tutorial Mode'
    public bool TutorialMode
   {
       get { return tutorialMode; }
   }
    // ----
}
