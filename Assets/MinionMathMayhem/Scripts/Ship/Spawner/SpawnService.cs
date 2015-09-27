﻿using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class SpawnService : MonoBehaviour
    {
        /*
         *                                                   SPAWN SERVICE
         * This script is designed to control the main central line of the spawners and to control their functionality unifyingly.
         *  As part of the main tasks provided withing this class, this will check the game environment and enable the spawners if possible by using a frequency
         *  time schedule.
         *
         * NOTES:
         *  This script only broadcasts a message to the SpawnController to do events as necessary; from there - the spawners will have to cooperate
         *   with the SpawnController.
         *
         * STRUCTURAL DEPENDENCY NOTES:
         *      SpawnService
         *          |_ <warten...>
         *
         * GOALS:
         *      Checks if the game is ready to fully start
         *      Determines what spawner to activate; individually or all.
         *      Pauses between next spawn for the frequency delay rate.
         */



        // Declarations and Initializations
        // ---------------------------------
            // Time when the next minion should spawn
                private float nextSpawn;
            // How many minions are to be spawned within 60 seconds of time
                // Can be manipulated within Unity's Inspector
                public float spawnRate;
            // Grace-Timer for when the spawners should be activated
                // Lock variable; this will avoid the gracePeriod to be reset in an endless loop.
                    private bool gracePeriodLockOut = false;
                // Grace Timer Duration
                    public float gracePeriodTimer = 2.5f;
            // Accessors and Communication
                // GameController
                    public GameController scriptGameController;
                // Game Event
                    public GameEvent scriptGameEvent;
                // Spawn Controller Specifics
                // Send Signal: Instantiate only one minion
                    public delegate void SummonMinionSignal();
                    public static event SummonMinionSignal SummonMinion_OnlyOne;
                // Send Signal: Instantiate minions
                    public delegate void SummonMinionBatchSignal();
                    public static event SummonMinionBatchSignal SummonMinion_Batches;
                // Spawner Frequency
                    public delegate void SpawnerFequencyDelegate(int frequencyLevel);
                    public static event SpawnerFequencyDelegate SpawnerFrequency;
        // ---------------------------------




        /// <summary>
        ///     This is a service function for managing how the minion actors will spawn within the scene.
        ///     NOTE: This function is DEPENDENT on the Daemon update frequency!
        /// </summary>
        /// <returns>
        ///     Nothing is returned
        /// </returns>
        private void Main()
        {
            // Is it safe to spawn the minions within the map?
            if (scriptGameController.SpawnMinions == !false &&
                scriptGameController.GameOver != true &&
                scriptGameEvent.AccessSpawnMinions != true &&
                gracePeriodLockOut != true)
            {
                // Safe to spawn minions within the virtual world
                SpawnController_Service();
            }
            else
            {
                // Not safe to spawn the minions within the virtual world
            }
        } // Daemon_SpawnerServicer()



        /// <summary>
        ///     Spawn a minion depending upon the next spawn time.
        ///     This function will first check to see if the next batch of minions can be spawned yet.
        ///      When true, the batch full of minions will be spawned and the next spawn time will be recalculated.
        /// </summary>
        private void SpawnController_Service()
        {
            if (Time.time >= nextSpawn)
            {
                // Batch signal to summon minion actors.
                SpawnController_SummonActor_Batch();
                // Determine the next time to summon a new minion creature
                SpawnController_GetNextSpawnTime();
            }
        } // SpawnController_Service()



        /// <summary>
        ///     Send a event message to the spawners, specifically, to spawn the minions.  (NOT Forcefully)
        /// </summary>
        private void SpawnController_SummonActor_Batch()
        {
            // Broadcast event
            SummonMinion_Batches();
        } // SpawnController_SummonActor_Batch()




        // Determine the new time in which a new minion will be spawned in the scene
        /// <summary>
        ///     This function is designed to calculate the next spawn time in which that next actor should be instantiated.
        ///     The calculation:
        ///         Using the game run time, get a random number from null to the spawn rate (per second) times two.
        ///         This should determine the next spawn ratio in which the minions should be summoned within the environment.
        ///         Credit to Bob for this code, that still exists today ;)
        /// </summary>
        private void SpawnController_GetNextSpawnTime()
        {
            nextSpawn = (Time.time + Random.Range(0, 2 * (60 / spawnRate)));
        } // SpawnController_GetNextSpawnTime()



        /// <summary>
        ///     This function will initiate the grace timer, which will momentarily delay another event from being broadcasted.
        /// </summary>
        private void GraceTimer()
        {
            // A simple timer
            StartCoroutine(GraceTimer_InitiateTimer());
        } // GraceTimer()



        /// <summary>
        ///     The grace timer function will enforce a wait-delay before another event can being broadcasted.
        /// </summary>
        /// <returns>
        ///     Returns nothing; coroutine function.
        /// </returns>
        private IEnumerator GraceTimer_InitiateTimer()
        {
            gracePeriodLockOut = true;
            yield return new WaitForSeconds(gracePeriodTimer);
            gracePeriodLockOut = false;
        } //GraceTimer_InitiateTimer()



        // This function will kindly tell delay the signal to start instantiating the minions.

        /// <summary>
        ///     This function will push a delay before another event can be broadcasted.  This is a public set function, use carefully.
        ///     NOTE: Deprecated?
        /// </summary>
        public void GracePeriodTimeOut_Request()
        {
            gracePeriodLockOut = true;
        } // GracePeriodTimeOut_Request()
    } // End of Class
} // Namespace