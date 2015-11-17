using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class TutorialWindow_BridgeScript : MonoBehaviour
    {
        /*                                          TUTORIAL WINDOW [GENERAL SCRIPT]
         * This class is designed to offer a standard base as to how the window dialogs will work with the tutorial protocol.
         *
         * GOALS:
         *  Initalizations for the specific window dialog components
         *  Execute the fundamental tutorials as required
         *  Close the tutorial when finished
         */


        // Declarations and Initializations
        // ---------------------------------
            // Specific window dialog components
                public GameObject rulesCanvas;
                public GameObject viewTutorialAgainCanvas;

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
            // Subscribe to the Window script
                WindowPlay.TutorialStateEnded += WindowTutorial_Destroy;
        } // OnEnable()



        /// <summary>
        ///     Built-In Unity Function
        ///     Automatically executes once the actor has been deactivated within the virtual world
        /// </summary>
        private void OnDisable()
        {
            // Unsubscribe to the Window script
                WindowPlay.TutorialStateEnded -= WindowTutorial_Destroy;
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
            // View Tutorial Again
                //viewTutorialAgainCanvas.SetActive(state);
            // Main Canvas
                rulesCanvas.SetActive(state);
        } // Object_Activation()



        /// <summary>
        ///     Front-End function to activating and controlling the window dialog
        /// </summary>
        private void Activate_Object()
        {
            // Enable the objects
                Object_Activation(true);

            // Finished
                return;
        } // Activate_Object()



        /// <summary>
        ///     Close the tutorial sequence as it was terminated
        /// </summary>
        private void WindowTutorial_Destroy()
        {
            // Turn off the objects
                Object_Activation(false);
            // Broadcast that we're finished
                TutorialEnded();
        } // WindowTutorial_Destroy()




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



        /// <summary>
        ///     A public bridge function to forcibly destroy the tutorial immediately.
        /// </summary>
        public void Access_Destroy()
        {
            WindowTutorial_Destroy();
        } // Access_Destroy()
    } // End of Class
} // Namespace