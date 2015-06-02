using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class Spawner : MonoBehaviour
    {
        /*                    SPAWNER
         * This class merely listens for a signal from the 'Spawn Controller' to instantiate a minion actor.
         *  
         * GOALS:
         *  Spawn the minion when a signal has been detected.
         */

        // Declarations and Initializations
        // ---------------------------------
            // Minion Actor
                public GameObject actor;
        // ----



        // This function is immediately executed once the actor is in the game scene.
        private void Start()
        {
            // First make sure that all the scripts and actors are properly linked
                CheckReferences();
        } // Start()



        // Signal Listener: Detected
        private void OnEnable()
        {
            SpawnController.EnableSpawnPoint += SpawnChoice;
        } // OnEnable()



        // Signal Listener: Deactivate
        private void OnDisable()
        {
            SpawnController.EnableSpawnPoint -= SpawnChoice;
        } // OnDisable()



        // 50% of spawning the actor
        private void SpawnChoice()
        {
            if (System.Convert.ToBoolean(UnityEngine.Random.Range(0, 2)))
                SpawnActor();
        } // SpawnChoice()



        // Spawn the creature
        private void SpawnActor()
        {            
            // spawn the minion actor
                Instantiate(actor, gameObject.transform.position, Quaternion.identity);
        } // Spawn()



        // Allow calling scripts\objects to trigger this function; the final destination is a private function, this method will call the desired function.
        public void SpawnForcibly_public()
        {
            // Forcibly spawn an actor
                SpawnActor();
        } // SpawnForcibly_public()



        // This function will check to make sure that all the references has been initialized properly.
        private void CheckReferences()
        {
            if (actor == null)
                MissingReferenceError("Minion Actor");
        } // CheckReferences()



        // When a reference has not been properly initialized, this function will display the message within the console and stop the game.
        private void MissingReferenceError(string refLink = "UNKNOWN_REFERENCE_NOT_DEFINED")
        {
            Debug.LogError("Critical Error: Could not find a reference to [ " + refLink + " ]!");
            Debug.LogError("  Can not continue further execution until the internal issues has been resolved!");
            Time.timeScale = 0; // Halt the game
        } // MissingReferenceError()
    } // End of Class
} // Namespace