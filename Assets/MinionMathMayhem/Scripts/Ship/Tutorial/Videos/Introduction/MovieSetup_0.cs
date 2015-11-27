using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class MovieSetup_0 : MonoBehaviour
    {
        /*                      TUTORIAL MOVIE SETUP [INTRODUCTION]
         * This class is designed to monitor the nested components and follow with the Tutorial Algorithm compliances
         *
         *  NESTED COMPONENTS:
         *      TutorialSkipButton
         *      TutorialMovie_0
         */


        /// <summary>
        ///     Built-In Unity Function
        ///     Automatically executes as soon as the actor is activated within the virtual world.
        /// 
        ///     Subscribe to events (or listen to)
        /// </summary>
        private void OnEnable()
        {
            TutorialSkipButton.SkipTutorialDemand += Kill;
            TutorialMovie_0.TutorialStateEnded += Kill;
        } // OnEnable()



        /// <summary>
        ///     Built-In Unity Function
        ///     Automatically executes as soon as the actor is deactivated from the virtual world
        /// 
        ///     Unsubscribe to events
        /// </summary>
        private void OnDisable()
        {
            TutorialSkipButton.SkipTutorialDemand -= Kill;
            TutorialMovie_0.TutorialStateEnded -= Kill;
        } // OnDisable()



        /// <summary>
        ///     As compliance with the Tutorial Algorithm Protocol, disable self to signify that -
        ///         this entire tutorial has finished.
        /// </summary>
        private void Kill()
        {
            gameObject.SetActive(false);
        } // Kill()
    } // End of Class
} // Namespace