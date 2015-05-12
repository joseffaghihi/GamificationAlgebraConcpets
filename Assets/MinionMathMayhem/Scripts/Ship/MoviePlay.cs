using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class MoviePlay : MonoBehaviour
    {
        /*              Tutorial Movie
         * Plays the tutorial movie specifically for the map.  This script will play the desired movie and send a signal that the movie has ended.
         * 
         * 
         * Dependencies:
         *      QuickTime
         * 
         * Goals:
         *      Play the tutorial movie
         *      Report back when the tutorial has finished.
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
            GameController.TutorialStateStart += EnableTutorial;
            TutorialSkipButton.SkipTutorialDemand += SkipTutorial;
        } // OnEnable()



        // Signal Listener: Deactivate
        private void OnDisable()
        {
            GameController.TutorialStateStart -= EnableTutorial;
            TutorialSkipButton.SkipTutorialDemand -= SkipTutorial;
        } // OnDisable()



        // When the signal has been detected, start the tutorial algorithm.
        private void EnableTutorial()
        {
            StartCoroutine(PlayTutorial());
        } // EnableTutorial()



        // When the signal has been detected, flip the bit 
        private void SkipTutorial()
        {
            tutorialSkip = !tutorialSkip;
        } // SkipTutorial()



        // Start-Up execution
        private void Awake()
        {
            // Initialization Field
                movieRenderer = GetComponent<Renderer>();
                movieTexture = (MovieTexture)movieRenderer.material.mainTexture;
            // ----
        } // Start()



        // This function will execute the Movie Texture
        private IEnumerator PlayTutorial()
        {
            // Play the movie
                Movie_Execute(movieTexture);
            // ----

            // Wait for the movie to end
                yield return (StartCoroutine(Movie_Ended_Check(movieTexture)));
            // ----

            // turn off the tutorial mode
                TutorialStateEnded();
        } // PlayTutorial()



        // Play the movie
        private void Movie_Execute(MovieTexture onScreenMovie)
        {
            onScreenMovie.Play();
        } // ExecuteMovie()



        private IEnumerator Movie_Ended_Check(MovieTexture onScreenMovie)
        {
            do
            {
                yield return new WaitForSeconds(.5f);
            } while (onScreenMovie.isPlaying == !false && tutorialSkip == false);

            yield return null;
        } // Movie_Ended_Check();
    }
}