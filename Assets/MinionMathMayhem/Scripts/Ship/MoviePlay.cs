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



        // Start-Up execution
        private IEnumerator Start()
        {
            // Initialization Field
                Renderer r = GetComponent<Renderer>();
                MovieTexture movie = (MovieTexture)r.material.mainTexture;
            // ----

            // Play the movie
                Movie_Execute(movie);
            // ----

            // Wait for the movie to end
                yield return (StartCoroutine(Movie_Ended_Check(movie)));
            // ----                
        } // Start()


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
            } while (onScreenMovie.isPlaying == !false);

            yield return null;
        } // Movie_Ended_Check();
    }
}