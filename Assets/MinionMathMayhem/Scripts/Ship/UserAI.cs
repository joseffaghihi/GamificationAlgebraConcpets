using UnityEngine;
using System.Collections;



namespace MinionMathMayhem_Ship
{
    public class UserAI : MonoBehaviour
    {
        /*
         *                                              USER PERFORMANCE ARTIFICIAL INTELLIGENCE
         * This is the forefront of controlling the environment based on the user performances.  This script is a 'always live' monitoring
         *  the user's performances based on accuracy and reaction time.  This daemon or service will routinly check the user's accuracy and determine if the
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
                    private uint counterMinionTime;
                // Maximum entries within the array; default is '4' within the whole number structure.
                    // This is customizable within the inspector; this will be used to set the size of the Database array.
                    public uint counterMinionTimeMax = 4;
                // Array for holding the time database
                    // DO NOT SET THE SIZE!  ALLOW THE SIZE TO BE ADJUSTABLE!
                    private float[] minionTimeArray;
            // Time when the next minion should spawn
                private float nextSpawn;
            // How many minions are to be spawned within 60 seconds of time
                // Can be manipulated within Unity's Inspector
                public float spawnRate;
            // Legacy Mode; this puts the game back to its original state without using any AI.
                public bool legacyMode = true;
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



        // Signal Listener: Detected
        private void OnEnable()
        {
            GameEvent.RequestGraceTime += GraceTimer;
            GameController.RequestGraceTime += GraceTimer;
        } // OnEnable()



        // Signal Listener: Deactivate
        private void OnDisable()
        {
            GameEvent.RequestGraceTime -= GraceTimer;
            GameController.RequestGraceTime -= GraceTimer;
        } // OnDisable()



        // When the attached game object is available; this function will immediately execute. 
        private void Start()
        {
            // Check to make sure that everything is properly initialized.
                CheckReferences();
            // Check values; prevert negated values
                CheckValues();

            // Game Environment Service
            if (legacyMode == !false)
                // Legacy, no AI service.
                StartCoroutine("WaveManager");
            else
                // AI daemon not present, use legacy anyways.
                StartCoroutine("WaveManager");


        } // Start()



        // This function will check the public variables and prevent any negated values.
        private void CheckValues()
        {
            if (nextSpawn < 0)
                nextSpawn = (nextSpawn * -1);
            if (spawnRate < 0)
                spawnRate = (spawnRate * -1);
            if (gracePeriodTimer < 0)
                gracePeriodTimer = (gracePeriodTimer * -1);
        } // CheckValues()




        // =======================================================================
        //                          DAEMON SERVICE
        //                            SCHEDULER
        // This will call the depending functions required for the AI.
        // =======================================================================
        private IEnumerator DaemonService()
        {
            // Never ending loop
            while(true)
            {
                // Request update on the following functions:
                    // Minion Service
                        StartCoroutine(Daemon_MinionService());
                yield return new WaitForSeconds(daemonUpdateFreq);
            } // While-Loop
        } //DaemonService()



        // =======================================================================
        //                          MINION DATABASE
        // =======================================================================

        // DAEMON SERVICER
        // Check the minion time database; if filled - determine the minion speed based on the average time from the records.
        private IEnumerator Daemon_MinionService()
        {
            // Only execute if the max indexs has been reached to fill the array
            if (counterMinionTime < counterMinionTimeMax)
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
            for (int i = 0; i < counterMinionTime ; i++)
                timeValue += minionTimeArray[i];

            // Divide by the database (or array) size and return the value
            return (timeValue / counterMinionTime);
        } // Database_MinionLifeSpan_AverageTime()



        // Record the minion's life span within an array; if the array is full (if we reached the max count) then do not record the entry.
        // The array counter will be reset by the Minion Service.
        private void Database_MinionLifeSpan(float time)
        {
            if (counterMinionTime > counterMinionTimeMax)
            {
                minionTimeArray[counterMinionTime] = time;
                counterMinionTime++;
            }
        } // Database_MinionLifeSpan()



        // This is a accessor function that will call the correct function
        public void Access_Database_MinionLifeSpan(float time)
        {
            Database_MinionLifeSpan(time);
        } // Access_Database_MinionLifeSpan()



        /// <summary>
        ///     This function will output the results to the console; nothing is changed or returned
        ///     DEBUG PURPOSES ONLY
        /// </summary>
        private void Database_MinionLifeSpan_AverageTime_DEBUG()
        {
            // Cached value
            float timeValue = 0f;

            // Added all the indexes
            for (int i = 0; i < counterMinionTime; i++)
            {
                timeValue += minionTimeArray[i];
                Debug.Log("Index address of [ " + i + " ] has value: " + minionTimeArray[i]);
            }

            Debug.Log("Lifespan database accumulative time: " + timeValue);


            // Divide by the database (or array) size and return the value
            Debug.Log("Average Time is: " + (timeValue / counterMinionTime));
        } // Database_MinionLifeSpan_AverageTime_DEBUG()





        // =======================================================================
        //                             LEGACY MODE
        // =======================================================================
        // The Wave Manager
        private IEnumerator WaveManager()
        {
            yield return null;
            while (true) // Never ending
            {
                // ----
                // Check to see if the spawner is activated
                if (scriptGameController.SpawnMinions == !false && scriptGameController.GameOver != true && scriptGameEvent.AccessSpawnMinions != true && gracePeriodLockOut != true)
                    // Check to see if it is time to spawn another minion
                    if (Time.time >= nextSpawn)
                        SpawnSignal();
                // ----

                // Brief wait time to ease the CPU
                yield return new WaitForSeconds(0.1f);
            } // While()
        } // WaveManager()



        // This function initiate the grace timer - that will momentarily delay the spawners from ever being activated.
        private void GraceTimer()
        {
            // A simple timer
                StartCoroutine(GraceTimer_InitiateTimer());
        } // GraceTimer()



        // The Timer function will disallow the spawners from becoming active until the wait-delay has passed.
        private IEnumerator GraceTimer_InitiateTimer()
        {
            gracePeriodLockOut = true;
            yield return new WaitForSeconds(gracePeriodTimer);
            gracePeriodLockOut = false;
        } //GraceTimer_InitiateTimer()



        // Determine how long the minions will spawn within the given time of 60 seconds.
        private float MinionsASecond()
        {
            return 60 / spawnRate;
        } // MinionASecond()



        // Determine the new time in which a new minion will be spawned in the scene
        private void CalcNextSpawnTime()
        {
            float r = Random.Range(0, 2 * MinionsASecond());
            nextSpawn = Time.time + r;
        } // CalcNextSpawnTime()



        // Send a signal to spawn the creature
        private void SpawnSignal()
        {
            // Broadcast a signal to the spawners to summon a minion.
                SummonMinion_Batches();
            // Determine the next time to summon a new minion creature
                CalcNextSpawnTime();
        } // SpawnSignal()



        // This function will kindly tell delay the signal to start instantiating the minions.
        public void GracePeriodTimeOut_Request()
        {
            gracePeriodLockOut = true;
        } // GracePeriodTimeOut_Request()
        // =======================================================================
        // =======================================================================
        // =======================================================================





        // ----
        // ERROR CHECKING
        // ====

        // This function will check to make sure that all the references has been initialized properly.
        private void CheckReferences()
        {
            if (scriptGameController == null)
                MissingReferenceError("Game Controller");
            if (scriptGameEvent == null)
                MissingReferenceError("Game Event");
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