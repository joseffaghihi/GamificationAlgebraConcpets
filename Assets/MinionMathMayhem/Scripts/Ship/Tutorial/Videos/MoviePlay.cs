using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class MoviePlay : MonoBehaviour
    {
        /*                                  TUTORIAL MOVIE
         * This script will run the movie tutorial when activated - automatically
         * 
         * 
         * Important Notes:
         *      Execution Order
         *          http://docs.unity3d.com/Manual/class-ScriptExecution.html
         *
         *
         * Dependencies:
         *      QuickTime
         *          Source Download: http://www.apple.com/quicktime/
         * 
         * Goals:
         *      Play the desired movie tutorial
         *      Send an event signal that the movie has ended.
         */


        // Declarations and Initializations
        // ---------------------------------
            // Movie Renderer [Mesh Renderer]
                public Renderer movieRenderer;
            // Movie Texture
                public MovieTexture movieTexture;

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
            // Execute the routine check-up; assure that the tutorial is playing
                StartCoroutine(Movie_RoutineCheckup());
            
            // Play the move
                Movie_Play();
        } // OnEnable()



        /// <summary>
        ///     Built-In Unity function
        ///     Automatically executes when the actor is about to be deactivated.
        /// </summary>
        private void OnDisable()
        {
            // Stop the movie
                Movie_Stop();
        } // OnDisable()



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
                movieRenderer = GetComponent<Renderer>();
                movieTexture = (MovieTexture)movieRenderer.material.mainTexture;
        } // Awake()



        /// <summary>
        ///     Plays the tutorial movie
        /// </summary>
        private void Movie_Play()
        {
            movieTexture.Play();
        } // Movie_Play()



        /// <summary>
        ///     Stops the tutorial movie
        /// </summary>
        private void Movie_Stop()
        {
            movieTexture.Stop();
        } // Movie_Stop()



        /// <summary>
        ///     Monitors the status of the movie; once the movie has finished (hits End of Line), close the tutorial.
        /// </summary>
        /// <returns>
        ///     Nothing useful
        /// </returns>
        private IEnumerator Movie_RoutineCheckup()
        {
            do
            {
                // Brief pause
                yield return new WaitForSeconds(.3f);
            } while (movieTexture.isPlaying);

            // Close the tutorial
                TutorialStateEnded();
        } // Movie_RoutineCheckup()
    } // End of Class
} // Namespace