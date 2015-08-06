using UnityEngine;
using System.Collections;



namespace MinionMathMayhem_Ship
{
    public class UserAI : MonoBehaviour
    {
        /*
         *                                              USER PERFORMANCE ARTIFICIAL INTELLIGENCE
         * This is the forefront of controlling the environment based on the user performances.  This script is a 'always live' monitoring
         *  the user's performances based on accuracy and reaction time.  This daemon or service will routinely check the user's accuracy and determine if the
         *  game itself should be a bit more difficult or easier, and also check the user's reaction time to determine if the user is capable of clicking the actors
         *  at a expeditious rate or adrenaline rush takes in (which is usually short lived, depending on the user and environment they are in).
         * 
         * 
         * NOTES:
         *  Since I am treating this like a daemon, this is going to hog resources.
         *  
         * 
         * STRUCTURAL DEPENDENCY NOTES:
         *      UserAI
         *          |_ SpawnController
         *              |_ Spawners
         *          |_ < Warten... >
         *              |_ < Warten... >
         *
         * 
         * GOALS:
         *      Always Alive; Monitoring service -- daemon service.
         *      Control the game specific environments
         *      Monitor User Performance
         *          * Accuracy; Spawn more minions
         *                              OR
         *                      Change the complexity level of the DEG.
         *          * Speed; minion speed movement is increased or decreased
         *                      Including climbing leaders and walking.
         *  
         */



        // Declarations and Initializations
        // ---------------------------------
            // Daemon Service Update Frequency
                // LOGIC: PERIOD = 1/clock;  200 KHz ~> P = 1/2x10^5 --> P == 5x10^(-6)
                private const float daemonUpdateFreq = 0.000005f;
            // Minion Lifespan Time Entry Counter
                // Entry counter within the array
                    private uint counterMinionTime = 0;
                // Maximum record entries of the minion's life spawn within the array
                    private const uint counterMinionTimeMax = 5;
                // Array for holding the time database
                    // DO NOT SET THE SIZE!  ALLOW THE SIZE TO BE ADJUSTABLE!
                    private float[] minionTimeArray = new float[counterMinionTimeMax];
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
        // ---------------------------------



        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Detected (or heard)
        /// </summary>
        private void OnEnable()
        {
            GameEvent.RequestGraceTime += GraceTimer;
            GameController.RequestGraceTime += GraceTimer;
        } // OnEnable()



        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Deactivated
        /// </summary>
        private void OnDisable()
        {
            GameEvent.RequestGraceTime -= GraceTimer;
            GameController.RequestGraceTime -= GraceTimer;
        } // OnDisable()


 
        /// <summary>
        ///     Unity Function
        ///     This function will immediately and automatically execute when the actor is 'activated' within the virtual world.
        /// </summary>
        private void Start()
        {
            // Check to make sure that everything is properly initialized.
                CheckReferences();
            // Check values; prevent negated values
                CheckValues();

            // Game Environment Service
                // Daemon Servicer; use the AI service.
                    StartCoroutine(DaemonService());
        } // Start()



        /// <summary>
        ///     DAEMON SERVICE SCHEDULER
        ///     This will call the depending function required for the AI.
        /// </summary>
        /// <returns>
        ///     Returns nothing; coroutine function
        /// </returns>
        private IEnumerator DaemonService()
        {
            // Never ending loop
            while(true)
            {
                // Request update on the following functions:
                    // Minion Service
                        StartCoroutine(Daemon_MinionService());
                    // Spawner Service
                        StartCoroutine(Daemon_SpawnerService());
                yield return new WaitForSeconds(daemonUpdateFreq);
            } // While-Loop
        } // DaemonService()




        // =======================================================================
        //                          MINION DATABASE
        // =======================================================================


        /// <summary>
        ///     Check the minion time database; if the array has been filled - determine the minion speed based on the average time from the recorded times.
        /// </summary>
        /// <returns>
        ///     Returns nothing
        /// </returns>
        private IEnumerator Daemon_MinionService()
        {
            // Only execute if the max indexes has been reached to fill the array
            if (counterMinionTime >= (counterMinionTimeMax))
            {
                // Compute the average time
                    float avgRecordTime = Database_MinionLifeSpan_AverageTime();
                // Reset the array index counter
                    counterMinionTime = 0;
            } // If
            yield return null;
        } // Daemon_MinionService()



        /// <summary>
        ///     This calculates the average life span of the minions based on the array database.
        ///     Computation is based on the standard average: Add all entries, divide by the index size.
        /// </summary>
        /// <returns>
        ///     The users average time; float
        /// </returns>
        private float Database_MinionLifeSpan_AverageTime()
        {
            // DEBUG
                Database_MinionLifeSpan_AverageTime_DEBUG();

            // Cached value
                float timeValue = 0f;
            
            // Added all the indexes
            for (int i = 0; i < counterMinionTimeMax; i++)
                timeValue += minionTimeArray[i];

            // Divide by the database (or array) size and return the value
                return (timeValue / counterMinionTimeMax);
        } // Database_MinionLifeSpan_AverageTime()



        /// <summary>
        ///     Record the minion's life span within an array; if the array is full (if we reached the max count) then do not record the entry.
        ///     The array counter will be reset by the Minion Service function.
        /// </summary>
        /// <param name="time">
        ///     Recorded time
        /// </param>
        private void Database_MinionLifeSpan(float time)
        {
            if (counterMinionTime < counterMinionTimeMax)
            {
                minionTimeArray[counterMinionTime] = time;
                counterMinionTime++;
            }
        } // Database_MinionLifeSpan()



        /// <summary>
        ///     This is merely an accessor function that will kindly call the private function.
        /// </summary>
        /// <param name="time">
        ///     Recorded time
        /// </param>
        public void Access_Database_MinionLifeSpan(float time)
        {
            Database_MinionLifeSpan(time);
        } // Access_Database_MinionLifeSpan()



        /// <summary>
        ///     This function will output the results to the console; nothing is changed or returned
        ///   <para />
        ///     __DEBUG_PURPOSES_ONLY__
        /// </summary>
        private void Database_MinionLifeSpan_AverageTime_DEBUG()
        {
            // Cached value
            float timeValue = 0f;

            // Added all the indexes
            for (int i = 0; i < counterMinionTimeMax; i++)
            {
                timeValue += minionTimeArray[i];
                Debug.Log("Index address of [ " + i + " ] has value: " + minionTimeArray[i]);
            }

            Debug.Log("Lifespan database accumulative time: " + timeValue);


            // Divide by the database (or array) size and return the value
            Debug.Log("Average Time is: " + (timeValue / counterMinionTimeMax));
        } // Database_MinionLifeSpan_AverageTime_DEBUG()




        // =======================================================================
        //                          SPAWNER SERVICE
        // =======================================================================


        /// <summary>
        ///     This is a service function for managing how the minion actors will spawn within the scene.
        ///     NOTE: This function is DEPENDENT on the Daemon update frequency!
        /// </summary>
        /// <returns>
        ///     Nothing is returned
        /// </returns>
        private IEnumerator Daemon_SpawnerService()
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

            yield return null;
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



        // =======================================================================
        // =======================================================================
        // =======================================================================





        // =======================================================================
        //                          ERROR CHECKING
        // =======================================================================

        /// <summary>
        ///     Assure that no variables have been initialized with bad values.
        /// </summary>
        private void CheckValues()
        {
            if (nextSpawn < 0)
                nextSpawn = (nextSpawn * -1);
            if (spawnRate < 0)
                spawnRate = (spawnRate * -1);
            if (gracePeriodTimer < 0)
                gracePeriodTimer = (gracePeriodTimer * -1);
        } // CheckValues()



        /// <summary>
        ///     Make sure that the dependent references have been initialized properly.
        /// </summary>
        private void CheckReferences()
        {
            if (scriptGameController == null)
                MissingReferenceError("Game Controller");
            if (scriptGameEvent == null)
                MissingReferenceError("Game Event");
        } // CheckReferences()



        /// <summary>
        ///     When a reference has not been properly initialized, this function will display the message within the console and stop the game.
        /// </summary>
        /// <param name="refLink">
        ///     The name of the reference link that is missing.
        /// </param>
        private void MissingReferenceError(string refLink = "UNKNOWN_REFERENCE_NOT_DEFINED")
        {
            Debug.LogError("Critical Error: Could not find a reference to [ " + refLink + " ]!");
            Debug.LogError("  Can not continue further execution until the internal issues has been resolved!");
            Time.timeScale = 0; // Halt the game
        } // MissingReferenceError()
    } // End of Class
} // Namespace