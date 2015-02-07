using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    private float nextSpawn;

    public GameObject MinionPrefab;
    public float spawnRate;
    public GameObject spawn;


	// Use this for initialization
	void Start () {
        CalcNextSpawnTime();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Time.time >= nextSpawn)
        {
            // spawn the Minion
            Instantiate(MinionPrefab, spawn.transform.position, Quaternion.identity);
            CalcNextSpawnTime();
        }
	}

    // Kind of a waste to call this every update.
    // Fix coming soon.
    float MinionsASecond()
    {
        return 60 / spawnRate ;
    }

    void CalcNextSpawnTime()
    {
        float r = Random.Range(0, 2 * MinionsASecond());
        nextSpawn = Time.time + r;
    }
}
