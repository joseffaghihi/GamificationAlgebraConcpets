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
         *  Execute the fundamental tutorials as required
         *  Close the tutorial if user skips it
         *  Close the tutorial if the movie ended
         */


        // Declarations and Initializations
        // ---------------------------------
            // Specific movie objects
                public GameObject objectTutorial_Movie;
                public GameObject objectTutorial_Canvas;
                public GameObject objectTutorial_SkipButton;
        
            // Delegate Event's: Tutorial ended
                public delegate void TutorialEndedSignal();
                public static event TutorialEndedSignal TutorialEnded;
        // ---------------------------------


        
        /// <summary>
        ///     Built-In Unity Function
        ///     Automatically executes once the actor has been activated within the virtual world
        /// </summary>
        private void OnEnable()
        {
            // Subscribe to TutorialSkipButton event
                TutorialSkipButton.SkipTutorialDemand += MovieTutorial_Destroy;
            // Subscribe to the Movie script
                MoviePlay.TutorialStateEnded += MovieTutorial_Destroy;
        } // OnEnable()



        /// <summary>
        ///     Built-In Unity Function
        ///     Automatically executes once the actor has been deactivated within the virtual world
        /// </summary>
        private void OnDisable()
        {
            // Unsubscribe to the TutorialSkipButton event
                TutorialSkipButton.SkipTutorialDemand -= MovieTutorial_Destroy;
            // Unsubscribe to the Movie script
                MoviePlay.TutorialStateEnded -= MovieTutorial_Destroy;
        } // OnDisable()



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
        ///     Front-End function to activating and controlling the movie
        /// </summary>
        /// <returns>
        ///     Nothing useful
        /// </returns>
        private void Activate_Object()
        {
            // Enable the objects
                Object_Activation(true);

            // Finished
                return;
        } // RenderObject()



        /// <summary>
        ///     Close the tutorial sequence as it was terminated (or skipped)
        /// </summary>
        private void MovieTutorial_Destroy()
        {
            // Turn off the objects
            Object_Activation(false);
            // Broadcast that we're finished
            TutorialEnded();
        } // MovieTutorial_Finished()




        // -------------------------------------------------
        //                 PUBLIC BRIDGES
        // -------------------------------------------------


        /// <summary>
        ///     Front-End Function to activate the movie tutorial
        /// </summary>
        public void ActivateTutorial()
        {
            Activate_Object();
        } // ActivateTutorial()
    } // End of Class
} // Namespace