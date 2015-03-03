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
    // References
        public GameState gameState;
    // Volume Tuner; this can be toggled in the inspector
        public float volumeNormal = 1.0f;
    // Volume Tuner; during tutorial mode
        public float volumeTutorial = 0.5f;



	// Update is called once per frame
	void Update ()
    {
        if (gameState.GameStateTutorial == false)
            // Standard music volume
            GetComponent<AudioSource>().volume = volumeNormal;
        else
            // Tutorial mode music volume
            GetComponent<AudioSource>().volume = volumeTutorial;
	} // End of Update

} // End Class
