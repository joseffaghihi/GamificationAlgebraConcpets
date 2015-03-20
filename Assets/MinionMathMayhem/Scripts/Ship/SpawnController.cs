using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour
{
        /*                    SPAWNER
         * This class will simply manage how the spawner's are going to summon the minion actors into the scene.
         *  
         * GOALS:
         *  Spawn the minions when the spawner is activated
         */



    // Declarations and Initializations
    // ---------------------------------
    // Script References
        public FinalDestroyer finalDestroyer;
        public GameState gameState;
    // Time when the next minion should spawn
        private float nextSpawn;
    // How many minions are to be spawned within 60 seconds of time
        public float spawnRate;
    // Summon actor:
        public GameObject MinionPrefab;
    // ----



    // Use this for initialization
    void Start()
    {
        // Determine the spawn rate
        CalcNextSpawnTime();
    } // End of Start



    // Update is called once per frame
    void Update()
    {

        // Check to see if the spawner is activated
        if (finalDestroyer.ActivateSpawner == true && gameState.ActivateSpawner == true)
        {
            // Check to see if it is time to spawn another minion
            if (Time.time >= nextSpawn)
            {
                Spawn();
            } // End if

        } // End parent-if

    } // End of Update



    // Determine how long the minions will spawn within the given time of 60 seconds.
    float MinionsASecond()
    {
        return 60 / spawnRate;
    } // End of MinionASecond



    // Determine the new time in which a new minion will be spawned in the scene
    void CalcNextSpawnTime()
    {
        float r = Random.Range(0, 2 * MinionsASecond());
        nextSpawn = Time.time + r;
    } // End of CalcNextSpawnTime



    // Spawn the creature
    void Spawn()
    {
        // spawn the Minion
        Instantiate(MinionPrefab, gameObject.transform.position, Quaternion.identity);

        // Determine the next time to summon a new minion creature
        CalcNextSpawnTime();
    }
} // End of Class

}
