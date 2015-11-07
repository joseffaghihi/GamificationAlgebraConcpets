using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class TutorialWindow_GeneralScript : MonoBehaviour
    {
        /*                                          TUTORIAL WINDOW [GENERAL SCRIPT]
         * This class is designed to offer a standard base as to how the dialog windows will work with the tutorial protocol.
         *
         * GOALS:
         *  Initalizations for the specific dialog window object
         */

        // Declarations and Initializations
        // ---------------------------------
            public GameObject rulesCanvas;
            //  Rules Control on the rulesCanvas GameObject  
                private RulesControl rulesControl;

        // Broadcast tutorial ended
            public delegate void TutorialEndedSignal();
            public static event TutorialEndedSignal TutorialEnded;
        // ---------------------------------



        /// <summary>
        /// Unity function
        /// When this object is active, this function will be called automatically.
        /// </summary>
        private void Awake()
        {
            rulesControl = rulesCanvas.GetComponent<RulesControl>();
        } // Awake()


        public void ActivateTutorial()
        {
            Debug.LogWarning("asdf");
        }
        /// <summary>
        ///     Renders the dialog window at the specific index
        /// </summary>
        /// <returns>
        ///     Returns nothing useful
        /// </returns>
        public IEnumerator RenderObject()
        {
            yield return StartCoroutine(rulesControl.Access_WaitForRulesToFinish());
            rulesCanvas.SetActive(false);

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
