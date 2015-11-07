using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class TutorialMovie_GeneralScript : MonoBehaviour
    {
        /*                                          TUTORIAL MOVIE [GENERAL SCRIPT]
         * This class is designed to offer a standard base as to how the movies will work with the tutorial protocol.
         *
         * GOALS:
         *  Initalizations for the specific movie object
         */

        // Declarations and Initializations
        // ---------------------------------
            public GameObject objectTutorial_Movie, objectTutorial_Canvas, objectTutorial_SkipButton;

            // Broadcast tutorial ended
                public delegate void TutorialEndedSignal();
                public static event TutorialEndedSignal TutorialEnded;
        // ---------------------------------


        
        /// <summary>
        ///     Front-End Function to activate the movie tutorial
        /// </summary>
            public void ActivateTutorial()
        {
            StartCoroutine(RenderObject());
        } // ActivateTutorial()



        /// <summary>
        ///     Plays the movie at the specific index
        /// </summary>
        /// <returns>
        ///     Returns nothing useful
        /// </returns>
        private IEnumerator RenderObject()
        {
            Debug.LogWarning("asdfasdf");
            yield return null;
            // Enable the tutorial objects
            objectTutorial_SkipButton.SetActive(true);
            objectTutorial_Movie.SetActive(true);
            objectTutorial_Canvas.SetActive(true);
            // Send the 'Tutorial Active' signal
            //TutorialStateStart();
            // Run a signal detector; once the signal has been detected, the tutorial is finished.
            //    Once the tutorial is finished, the rest of the game can execute.
            //yield return (StartCoroutine(GameExecute_Tutorial_ScanSignal()));
            // Disable the tutorial objects
            objectTutorial_Movie.SetActive(false);
            objectTutorial_Canvas.SetActive(false);
            objectTutorial_SkipButton.SetActive(false);

            CloseTutorial();
        } // RenderObject()



        /// <summary>
        ///     This will terminate the tutorial.
        /// </summary>
        private void Destroy()
        {

        } // Destroy()



        /// <summary>
        ///     When called by other scripts\classes, this will activate a forcible kill of the tutorial.
        /// </summary>
        public void Access_Destroy()
        {
            Destroy();
        } // Access_Destroy()



        /// <summary>
        ///     Notify the Tutorial Main that this script is ready to self-terminate.
        /// </summary>
        private void CloseTutorial()
        {
            // Broadcast signal that we're done
                TutorialEnded();
        } // CloseTutorial()
    } // End of Class
} // Namespace