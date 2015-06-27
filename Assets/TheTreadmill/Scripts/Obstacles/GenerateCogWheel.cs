using UnityEngine;
using System.Collections;

public class GenerateCogWheel : MonoBehaviour 
{
    public GameObject obstacle; //Stores the obstacle to be instantiated
	public GameObject dangerSign; //Stores the zone to avoid because of the barrel
    public float spawnTime = 15; //Stores the amount of time an obstacle spawns

    private Vector3 playerPosition; //Stores the player's position

    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        InvokeRepeating("SpawnObstacle", spawnTime, spawnTime);
    }

    void FixedUpdate()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position; //Update with new Player position
    }

    void SpawnObstacle()
    {
        Instantiate(obstacle, new Vector3(playerPosition.x + 40, playerPosition.y + 7, playerPosition.z), Quaternion.identity);
		Instantiate (dangerSign, new Vector3 (0.0f, 1.55f, playerPosition.z), Quaternion.identity);
    }
}
