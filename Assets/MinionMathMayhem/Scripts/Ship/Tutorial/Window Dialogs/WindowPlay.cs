using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class WindowPlay : MonoBehaviour
    {
        /*                                      TUTORIAL WINDOW DIALOG
         * This script will run the window dialog tutorial when activated - automatically
         * 
         * 
         * Important Notes:
         *      Execution Order
         *          http://docs.unity3d.com/Manual/class-ScriptExecution.html
         *
         * 
         * Goals:
         *      Display the desired dialog tutorial
         *      Send an event signal that the tutorial has ended.
         */


        // Declarations and Initializations
        // ---------------------------------
            // Specific window dialog components
                public GameObject rulesCanvas;
            //  Rules Control on the rulesCanvas GameObject
                private RulesControl rulesControl;

            // Tutorial State: Finished
                public delegate void TutorialStateEventEnded();
                public static event TutorialStateEventEnded TutorialStateEnded;
        // ---------------------------------

        
        
        /// <summary>
        ///     Built-In Unity function
        ///     Automatically executes as soon as the actor is activated within the virtual world.
        /// 
        ///     NOTE:
        ///         This is the SECOND function to be called; the first is typically the Awake() function.
        /// </summary>
        private void OnEnable()
        {
            // Display the tutorial
                Tutorial_Play();
        } // OnEnable()



        /// <summary>
        ///     Built-In Unity function
        ///     Automatically executes as soon as the actor is activated within the virtual world
        /// 
        ///     NOTE:
        ///         This is the FIRST function to be called; although this time span might be a few nanoseconds before the next sequential function is also called.
        /// </summary>
        private void Awake()
        {
            // Initialization objects for the movie sequence
                rulesControl = rulesCanvas.GetComponent<RulesControl>();
        } // Awake()



        /// <summary>
        ///     Displays the tutorial on the screen
        /// 
        ///     NOTE: This calls an coroutine as the window dialog manages itself.
        /// </summary>
        private void Tutorial_Play()
        {
            StartCoroutine(Tutorial_Play_Yield());
        } // Tutorial_Play()



        /// <summary>
        ///     Starts the window dialog tutorial
        /// </summary>
        /// <returns>
        ///     Nothing useful
        /// </returns>
        private IEnumerator Tutorial_Play_Yield()
        {
            // Call the tutorial
                yield return StartCoroutine(rulesControl.Access_WaitForRulesToFinish());

            // Finished tutorial
                TutorialStateEnded();

            // Close
                yield break;
        } // Tutorial_Play_Yield()
    } // End of Class
} // Namespace