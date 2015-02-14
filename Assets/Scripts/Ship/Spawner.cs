using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    // Declarations and Initializations
    // ---------------------------------
    private float nextSpawn;
    public GameObject MinionPrefab;
    public float spawnRate;
    public FinalDestroyer finalDestroyer;
    public GameState gameState;
    // ----


    // Use this for initialization
    void Start()
    {
        CalcNextSpawnTime();
    } // End of Start



    // Update is called once per frame
    void Update()
    {
        if (finalDestroyer.ActivateSpawner == true && gameState.ActivateSpawner == true)
        {
            if (Time.time >= nextSpawn)
            {
                Spawn();
            }
        }
    } // End of Update



    // Kind of a waste to call this every update.
    // Fix coming soon.
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
