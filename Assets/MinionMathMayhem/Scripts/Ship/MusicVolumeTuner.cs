using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
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
                public float volumeTutorial = 0.1f;
        // ----



        // Signal Listener: Detected
        private void OnEnable()
        {
            GameController.TutorialStateStart += MusicTurner_Reduce;
            MoviePlay.TutorialStateEnded += MusicTurner_Return;
        } // OnEnable()



        // Signal Listener: Deactivate
        private void OnDisable()
        {
            GameController.TutorialStateStart -= MusicTurner_Reduce;
            MoviePlay.TutorialStateEnded -= MusicTurner_Return;
        } // OnDisable()



        // Tutorial is running, reduce the volume of the background music.
        private void MusicTurner_Reduce()
        {
            MusicTurnerCheckValue(1);
            GetComponent<AudioSource>().volume = volumeTutorial;
        } // MusicTurner_Reduce()



        // Tutorial has ended, revert the volume of the background music.
        private void MusicTurner_Return()
        {
            MusicTurnerCheckValue(0);
            GetComponent<AudioSource>().volume = volumeNormal;
        } // MusicTurner_Return()



        // Check the values; prevent negated values
        private void MusicTurnerCheckValue(short checkMode = 9999)
        {
            // Check Normal Volume
            if (checkMode == 0 || checkMode == 9999)
            {
                if (volumeNormal < 0)
                    volumeNormal = (volumeNormal * -1);
            } // Check Normal Volume


            // Check Tutorial Volume
            if (checkMode == 1 || checkMode == 9999)
            {
                if (volumeTutorial < 0)
                    volumeTutorial = (volumeTutorial * -1);
            } // Check Tutorial Volume
        } // MusicTurnerCheckValue()
    } // End Class
} // Namespace