using UnityEngine;
using System.Collections;

public class VoiceOver : MonoBehaviour
{
   /*                    VOICE OVER TUTORIAL
    * This script manages the tutorial that is given by the Minion Spirit.
    *  
    * GOALS:
    *  Manage the Minion Spirit's tutorial
    *  Interrupt tutorial
    */



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
                GetComponent<AudioSource>().clip = tutorialClip;
                GetComponent<AudioSource>().Play();

                // Check to see if the user is skipping the tutorial before issuing a wait.
                for (int i = 0; i < tutorialClip.length && skip == false; i++)
                    yield return new WaitForSeconds(1);
            }
            
            // Stop the audio that is currently being played.  This is useful when the skip tutorial has been activated.
            GetComponent<AudioSource>().Stop();

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
