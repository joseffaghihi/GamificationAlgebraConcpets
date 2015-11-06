using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

namespace MinionMathMayhem_Ship
{
    public class TutorialMain : MonoBehaviour
    {
        /*                                                      TUTORIAL MAIN
         * This class is designed to manage and render the requested tutorial or tutorials -- as it is possible to run atleast one movie tutorial and one window tutorial.  As designed, -
         *  this will manage how the movie and window sequences work and make sure that they are set-up properly for an easy transition from the virtual world environment.  This class -
         *  can only be called by broadcasted events, and will broadcast an event signal when finished.
         *
         *  GOALS:
         *   Setup the environment for the requested Movie or Window.
         *   Select (or randomly select) the movie or window from the array list
         *   Play the tutorial(s)
         *   When finished, let listening classes\scripts know.
         */



        // Declarations and Initializations
        // ---------------------------------
        // Tutorial Arrays
            // Window
                public List<GameObject> tutorialWindowArray = new List<GameObject>();
            // Movie
                public List<GameObject> tutorialMovieArray = new List<GameObject>();
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
        private void TutorialMain_Driver(bool tutorialMovie = false,
                                        bool tutorialWindow = false,
                                        int PlayIndex = 0,
                                        bool randomIndex = false)
        {
            // Make sure there is no errors
            if (TutorialMain_CheckErrors(tutorialMovie, tutorialWindow, PlayIndex))
                return;
            // ----

            // Play the tutorials as requested
            if (tutorialMovie)
                StartCoroutine(TutorialMain_Play_Movie(PlayIndex, randomIndex));
            if (tutorialWindow)
                StartCoroutine(TutorialMain_Play_Window(PlayIndex, randomIndex));

            // Finished tutorial
                //TutorialMain_FinishedSignal();
        } // TutorialMain_Driver()



        /// <summary>
        ///     Select or randomly select the movie, and then play the desired movie tutorial
        /// </summary>
        /// <param name="playIndex">
        ///     Select the index inwhich to play the movie
        /// </param>
        /// <param name="randomIndex">
        ///     Randomly select an index
        /// </param>
        /// <returns>
        ///     Returns nothing useful
        /// </returns>
        private IEnumerator TutorialMain_Play_Movie(int playIndex, bool randomIndex)
        {
            yield return null;

            tutorialMovieArray[playIndex].GetComponent<TutorialMovie_GeneralScript>().asdf();
        } // TutorialMain_Play_Movie()



        /// <summary>
        ///     Select or randomly select the dialog window, and then render the desired window tutorial
        /// </summary>
        /// <param name="playIndex">
        ///     Select the index inwhich to display the dialog window
        /// </param>
        /// <param name="randomIndex">
        ///     Randomly select an index
        /// </param>
        /// <returns>
        ///     Returns nothing useful
        /// </returns>
        private IEnumerator TutorialMain_Play_Window(int playIndex, bool randomIndex)
        {
            yield return null;
            tutorialWindowArray[playIndex].GetComponent<TutorialWindow_GeneralScript>().asdf();
        } // TutorialMain_Play_Window()



        /// <summary>
        ///     This function is used to check for possible errors that could commonly occur if the setup is incorrect.
        /// </summary>
        /// <param name="tutorialMovie">
        ///     When true, this will display a movie [can be concurrent with tutorialWindow].
        /// </param>
        /// <param name="tutorialWindow">
        ///     When true, this will display a window [can be concurrent with tutorialMovie].
        /// </param>
        /// <param name="PlayIndex">
        ///     Forcibly play or display the window within the exact index.
        /// </param>
        /// <returns>
        ///     true = Failure or there was error.
        ///     false = No errors detected.
        /// </returns>
        private bool TutorialMain_CheckErrors(bool tutorialMovie,
                                                bool tutorialWindow,
                                                int PlayIndex)
        {
            // Make sure that either the window or movie has been selected
            if (TutorialMain_CheckCallErrors(tutorialMovie, tutorialWindow))
            {
                TutorialMain_Error(1);
                TutorialMain_FinishedSignal();
                return true;
            }

            // Make sure that there is a tutorial to be rendered
            if (tutorialMovie && tutorialMovieArray[PlayIndex] == null)
            {
                // The requested index doesn't exist
                TutorialMain_Error(2, PlayIndex.ToString());
                TutorialMain_FinishedSignal();
                return true;
            }

            else if (tutorialWindow && tutorialWindowArray[PlayIndex] == null)
            {
                // The requested index doesn't exist
                TutorialMain_Error(3, PlayIndex.ToString());
                TutorialMain_FinishedSignal();
                return true;
            }


            // No errors detected
                return false;
        } // TutorialMain_CheckErrors()



        /// <summary>
        ///     Once the tutorial sequence is finished, notify all classes\scripts that the tutorial ended.
        /// </summary>
        private void TutorialMain_FinishedSignal()
        {
            TutorialFinished();
        } // TutorialMain_FinishedSignal()



        /// <summary>
        ///     Output an error message to the console
        /// </summary>
        /// <param name="errType">
        ///     Type of error:
        ///         0 = no error (default)
        ///         1 = No tutorial type was selected (movie nor window)
        ///         2 = The movie tutorial index is not valid or was never initialized within the Unity's Inspector.
        ///         3 = The dialog window tutorial index is not valid or was never initialized within the Unity's Inspector.
        /// </param>
        /// <param name="message">
        ///     Specific message used for the console along with the initial generic message.  Default is "".
        /// </param>
        private void TutorialMain_Error(ushort errType = 0, string message = "")
        {
            // Initalize a cache string var; we will use this for constructing the message.
                string consoleMessage = "SOMETHING_HAPPENED!"; // I decided to add Microsoft's famous error message!  I think this is verbose enough and is very clear as to how we can address the problems! :D [NG]
            
            // Construct the error message by scanning through the error library
                switch (errType)
                {
                    case 0:
                        consoleMessage = "Well this is odd; this shouldn't happen!  There is _NO_ error";
                        break;
                    case 1:
                        consoleMessage = "Tutorial protocol was called without playing any tutorials";
                        break;
                    case 2:
                        consoleMessage = "The movie tutorial index [" + message + "] does not exist!";
                        break;
                    case 3:
                        consoleMessage = "The dialog window tutorial index [" + message + "] does not exist!";
                        break;
                } // switch()

            // Output the error message
                Debug.LogError("<!> ERROR <!> \n" + consoleMessage);
        } // TutorialMain_Error()
    } // End of Class
} // Namespace