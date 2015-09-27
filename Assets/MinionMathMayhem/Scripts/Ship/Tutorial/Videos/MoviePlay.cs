using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class MoviePlay : MonoBehaviour
    {
        /*                                  TUTORIAL MOVIE
         * This script will manage the movie tutorial sequence of the game.  This script is designed to listen for an event and activate the desired -
         *  on screen tutorial movie.  In addition, this script is also designed to listen an event to abort any movie that is being currently executed.  As well as, send a broad event signal that a movie has ended for active scripts listening.
         * 
         * 
         * Dependencies:
         *      QuickTime
         *          Source Download: http://www.apple.com/quicktime/
         * 
         * Goals:
         *      Play the desired movie sequence
         *      Send an event signal that the movie has ended.
         *      Listen for the 'Skip' button being activate.
         */



        // Declarations and Initializations
        // ---------------------------------
            // Movie Renderer [Mesh Renderer]
                public Renderer movieRenderer;
            // Movie Texture
                public MovieTexture movieTexture;
            // Tutorial skip signal
                public bool tutorialSkip = false;
            // Accessors and Communication
                // Tutorial State: Finished
                    public delegate void TutorialStateEventEnded();
                    public static event TutorialStateEventEnded TutorialStateEnded;
        // ----



        // Signal Listener: Detected
        private void OnEnable()
        {
            // Activate the tutorial movie
                GameController.TutorialStateStart += EnableTutorial;
            // The Skip Button was toggled
                TutorialSkipButton.SkipTutorialDemand += SkipTutorial;
        } // OnEnable()



        // Signal Listener: Deactivate
        private void OnDisable()
        {
            // Tutorial movie
                GameController.TutorialStateStart -= EnableTutorial;
            // Skip Button was toggled
                TutorialSkipButton.SkipTutorialDemand -= SkipTutorial;
        } // OnDisable()



        // Start-Up execution
        private void Awake()
        {
            // Initialization objects for the movie sequence
                movieRenderer = GetComponent<Renderer>();
                movieTexture = (MovieTexture)movieRenderer.material.mainTexture;
        } // Start()



        // When triggered by the event, initiate the movie tutorial sequence.
        private void EnableTutorial()
        {
            StartCoroutine(PlayTutorial());
        } // EnableTutorial()



        // When the signal has been detected, flip the bit 
        private void SkipTutorial()
        {
            tutorialSkip = !tutorialSkip;
        } // SkipTutorial()



        // This function will execute the tutorial movie sequence.
        private IEnumerator PlayTutorial()
        {
            // Play the movie
                Movie_Execute(movieTexture);
            // ----

            // Wait for the movie to end
                yield return (StartCoroutine(Movie_Ended_Check(movieTexture)));
            // ----

            // Stop the movie
                Movie_Stop(movieTexture);
            // ----

            // turn off the tutorial mode
                TutorialStateEnded();
        } // PlayTutorial()



        // Play the movie
        private void Movie_Execute(MovieTexture onScreenMovie)
        {
            onScreenMovie.Play();
        } // ExecuteMovie()



        // Stop the movie
        private void Movie_Stop(MovieTexture onScreenMovie)
        {
            onScreenMovie.Stop();
        } // Movie_Stop()



        // Periodically check if the movie ended or was skipped.
        private IEnumerator Movie_Ended_Check(MovieTexture onScreenMovie)
        {
            do
            {
                // A loop-wait to ease on the CPU without being abundantly excessive on resources.
                    yield return new WaitForSeconds(.5f);
            } while (onScreenMovie.isPlaying == !false && tutorialSkip == !true);

            yield return null;
        } // Movie_Ended_Check()
    } // End of Class
} // Namespace