using UnityEngine;
using System.Collections;



namespace MinionMathMayhem_Ship
{
    public class Waves : MonoBehaviour
    {
        /*
         *                          Waves
         * This is the forefront as to how many minions will be in the scene within a given time.
         * Once activated, the very first starting wave will be easy and slow -- only a few creatures will be spawned.
         * However, as the later waves continue, the more minions will be in the virtual world.  This will be accomplished by changing the frequency
         * of the spawn rate.
         * 
         * STRUCTURAL DEPENDENCY NOTES:
         *      Waves
         *         |_ SpawnController
         *              |_ Spawners
         *              
         * GOALS:
         *  Specifically and forcibly spawn a minion from a spawner
         *  Spawn an abundance of minions given the wave count
         *      WAVE 00 <=> Tutorialish
         *      WAVE 01 <=> Several minions spawning
         *      WAVE 05 <=> An abundance of minions spawning
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
            // Execute the Wave Manager
                StartCoroutine("WaveManager");
        } // Start()



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