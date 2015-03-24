using UnityEngine;
using System.Collections;

public class MusicVolumeTuner : MonoBehaviour
{

    /*                          MUSIC VOLUME TUNER
     * This script will reduce the volume of the main music when the game is in tutorial mode.
     * 
     * Goals:
     *      Reduce music volume during Tutorial mode
     */



    // Declarations and Initializations
    // ---------------------------------
        // Volume Tuner; this can be toggled in the inspector
            public float volumeNormal = 1.0f;
        // Volume Tuner; during tutorial mode
            public float volumeTutorial = 0.5f;



    // Signal Listener: Detected
    private void OnEnable()
    {
        GameController.TutorialStateStart += MusicTurner_Reduce;
        VoiceOver.TutorialStateEnded += MusicTurner_Return;
    } // OnEnable()



    // Signal Listener: Deactivate
    private void OnDisable()
    {
        GameController.TutorialStateStart -= MusicTurner_Reduce;
        VoiceOver.TutorialStateEnded -= MusicTurner_Return;
    } // OnDisable()



    // Tutorial is running, reduce the volume of the background music.
    private void MusicTurner_Reduce()
    {
        GetComponent<AudioSource>().volume = volumeTutorial;
    } // MusicTurner_Reduce()



    // Tutorial has ended, revert the volume of the background music.
    private void MusicTurner_Return()
    {
        GetComponent<AudioSource>().volume = volumeNormal;
    } // MusicTurner_Return()
} // End Class
