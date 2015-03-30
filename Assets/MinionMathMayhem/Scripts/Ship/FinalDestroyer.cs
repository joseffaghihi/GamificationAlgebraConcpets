using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class FinalDestroyer : MonoBehaviour
    {

        /*                      FINAL DESTROYER
         * When the actor triggers the exit object, this class will simply cache the actor's identity number and kill the actor from the scene.
         *   The cached identity will be used from another
         *  
         * GOALS:
         *  Fetch the actor's uniquly self-assigned number and cache it.
         *  Destroy the actor
         */


        // Declarations and Initializations
        // ---------------------------------
        // Cached integer from the actor
        private int cacheNumber;
        // Spawner Broadcast Event
        // Game Event Broadcast 
        public delegate void ToggleGameEventSignal();
        public static event ToggleGameEventSignal GameEventSignal;
        // ----



        // During the collision, cache the actor's uniquely assigned 
        private void OnTriggerEnter(Collider actor)
        {
            // Fetch the number from the actor
            cacheNumber = RetrieveActorIdentity(actor);
            // Send a signal to GameEvent to execute
            GameEventSignal();
            // Destroy the actor
            Destroy(actor.gameObject);
        } // OnTriggerEnter()



        // This function is designed to fetch the uniquely self-assigned number.
        private int RetrieveActorIdentity(Collider actorObject)
        {
            // Fetch the minion's unique script.
            Minion_Identity tempData = actorObject.gameObject.GetComponent<Minion_Identity>();
            // Fetch and return the minion's uniquely assigned number.
            return (tempData.MinionNumber);
        } // RetrieveActorIdentity()



        // This accessor will allow outside classes to retrieve the actor's number.
        public int ActorIdentity
        {
            get { return cacheNumber; }
        } // ActorIdentity
    } // End of Class
} // Namespace