using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class TutorialMain : MonoBehaviour
    {

        // Declarations and Initializations
        // ---------------------------------
        // Accessors and Communication
            // Finished tutorial sequence signal
                public delegate void TutorialSequenceFinishedSig();
                public static event TutorialSequenceFinishedSig TutorialFinished;
        // ---------------------------------




        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Detected (or heard)
        /// </summary>
        private void OnEnable()
        {
            GameController.TutorialSequence += TutorialMain_Driver;
        } // OnEnable()



        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Deactivated
        /// </summary>
        private void OnDisable()
        {
            GameController.TutorialSequence -= TutorialMain_Driver;
        } // OnDisable()



        /// <summary>
        ///     Make sure that either (or both) the tutorial movie or window has been selected.
        /// </summary>
        /// <param name="tutorialNovie">
        ///     Tutorial movie switch
        /// </param>
        /// <param name="tutorialWindow">
        ///     Tutorial window switch
        /// </param>
        /// <returns>
        ///     False; if there was no error
        ///     True; if there was an error.  Nothing was selected.
        /// </returns>
        private bool TutorialMain_CheckCallErrors(bool tutorialNovie, bool tutorialWindow)
        {
            if (!tutorialNovie && !tutorialWindow)
                return true;
            else
                return false;
        } // TutorialMain_CheckCallErrors()



        /// <summary>
        ///     Main tutorial sequence driver that manages how the tutorials are to be displayed and what type.
        /// </summary>
        /// <param name="tutorialMovie">
        ///     When true, this will display a movie [can be concurrent with tutorialWindow].  Default is false.
        /// </param>
        /// <param name="tutorialWindow">
        ///     When true, this will display a window [can be concurrent with tutorialMovie].  Default is false.
        /// </param>
        /// <param name="PlayIndex">
        ///     Forcibly play or display the window within the exact index.  Default is 0.
        /// </param>
        /// <param name="randomIndex">
        ///     When true, this will randomize what tutorials (movie and/or window) is to be played; if part of the index array.  Default is false.
        /// </param>
        /// <param name="tutorialCategory">
        ///     Select an index category for which to play or display the tutorial.  Default is 0.
        /// </param>
        private void TutorialMain_Driver(bool tutorialMovie = false,
                                        bool tutorialWindow = false,
                                        int PlayIndex = 0,
                                        bool randomIndex = false,
                                        uint tutorialCategory = 0)
        {
            // Make sure there is no errors
                // Make sure that either the window or movie has been selected
                     TutorialMain_CheckCallErrors(tutorialMovie, tutorialWindow);
            // ----

            // Finished tutorial
                TutorialFinished();
        } // TutorialMain_Driver()


    } // End of Class
} // Namespace