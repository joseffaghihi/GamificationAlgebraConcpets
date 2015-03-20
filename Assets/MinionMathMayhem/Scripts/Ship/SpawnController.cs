using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour
{
        /*                    SPAWNER CONTROLLER
         * This class will simply send a singal to the spawner actors to summon the minion actors into the scene.
         *  
         * GOALS:
         *  Determine the next spawn
         *  Send spawn signal to the spawners
         */



    // Declarations and Initializations
    // ---------------------------------
        // Time when the next minion should spawn
            private float nextSpawn;
        // How many minions are to be spawned within 60 seconds of time
            // Can be manipulated within Unity's Inspector
            public float spawnRate;

        // Accessors and Communication
            // GameController
                public GameController scriptGameController;
            // Game Event
                public FinalDestroyer scriptFinalDestroyer;
            // Spawner Broadcast Event
                public delegate void ActivateSpawnPoint();
                public static event ActivateSpawnPoint EnableSpawnPoint;
        // DEPERCATED
        public FinalDestroyer finalDestroyer;
        //public GameState gameState;
    // ----



        // This function is immediately executed once the actor is in the game scene.
    private void Start()
    {
        // Determine the spawn rate
            CalcNextSpawnTime();
        // Spawn Control
            StartCoroutine(SpawnDriver());
    } // Start()



    // This function is always called on each frame.
    private IEnumerator SpawnDriver()
    {
        while (true) // Never ending
        {
            // ----
            // Check to see if the spawner is activated
            if (finalDestroyer.ActivateSpawner == true && scriptGameController.SpawnMinions == !false)
                // Check to see if it is time to spawn another minion
                if (Time.time >= nextSpawn)
                    SpawnSignal();
            // ----

            // Brief wait time to ease the CPU
            yield return new WaitForSeconds(0.1f);
        } // While()
    } // SpawnController()



    // Determine how long the minions will spawn within the given time of 60 seconds.
    private float MinionsASecond()
    {
        return 60 / spawnRate;
    } // MinionASecond()



    // Determine the new time in which a new minion will be spawned in the scene
    void CalcNextSpawnTime()
    {
        float r = Random.Range(0, 2 * MinionsASecond());
        nextSpawn = Time.time + r;
    } // CalcNextSpawnTime()



    // Send a signal to spawn the creature
    void SpawnSignal()
    {
        // Broadcast a signal to the spawners to summon a minion.
            EnableSpawnPoint();
        // Determine the next time to summon a new minion creature
            CalcNextSpawnTime();
    } // SpawnSignal()
} // End of Class