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
                Object_Activation(true);

            // Send the 'Tutorial Active' signal
            //TutorialStateStart();
            // Run a signal detector; once the signal has been detected, the tutorial is finished.
            //    Once the tutorial is finished, the rest of the game can execute.
            //yield return (StartCoroutine(GameExecute_Tutorial_ScanSignal()));

            // Disable the tutorial objects
             Object_Activation(false);

        CloseTutorial();
        } // RenderObject()



        /// <summary>
        ///     Changes the actor's activation
        /// </summary>
        /// <param name="state">
        ///     true = Actor is activated in the Virtual World
        ///     false = Actor is unactivated in the Virtual World
        /// </param>
        private void Object_Activation(bool state)
        {
            // Skip Button
                objectTutorial_SkipButton.SetActive(state);
            // Movie Box
                objectTutorial_Movie.SetActive(state);
            // Movie Canvas
                objectTutorial_Canvas.SetActive(state);
        } // Object_Activation()



        /// <summary>
        ///     This will terminate the tutorial.
        /// </summary>
        private void Destroy()
        {

        } // Destroy()



        /// <summary>
        ///     Notify the Tutorial Main that this script is ready to self-terminate.
        /// </summary>
        private void CloseTutorial()
        {
            // Broadcast signal that we're done
                TutorialEnded();
        } // CloseTutorial()




        // -------------------------------------------------
        //              PUBLIC BRIDGES
        // -------------------------------------------------




        /// <summary>
        ///     Front-End Function to activate the movie tutorial
        /// </summary>
        public void ActivateTutorial()
        {
            StartCoroutine(RenderObject());
        } // ActivateTutorial()



        /// <summary>
        ///     When called by other scripts\classes, this will activate a forcible kill of the tutorial.
        /// </summary>
        public void Access_Destroy()
        {
            Destroy();
        } // Access_Destroy()
    } // End of Class
} // Namespace