using UnityEngine;
using System.Collections;

public class GenerateCogWheel : MonoBehaviour 
{
    public GameObject obstacle; //Stores the obstacle to be instantiated
	public GameObject dangerSign; //Stores the zone to avoid because of the barrel
    public float spawnTime = 15; //Stores the amount of time an obstacle spawns
	public bool generateObstacles = true; //Check to see if generating obstacles is enabled (default = enabled)

	private GameControl gameControl = new GameControl();
    private Vector3 playerPosition; //Stores the player's position

	//Start generating Obstacles
	void Start()
	{
		GenerateObstacles ();
	}

    void FixedUpdate()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position; //Update with new Player position
    }

    void SpawnObstacle()
    {
		if (!gameControl.getDelayWave ()) //Spawn Obstacles only during the waves
		{
			Instantiate (obstacle, new Vector3 (playerPosition.x + 40, playerPosition.y + 7, playerPosition.z), Quaternion.identity);
			Instantiate (dangerSign, new Vector3 (0.0f, 1.55f, playerPosition.z), Quaternion.identity);
		}
    }

	public void GenerateObstacles()
	{
		if(generateObstacles)//If Generating obstacles is enabled
		{
			InvokeRepeating("SpawnObstacle", spawnTime, spawnTime);
		}
		else
			CancelInvoke(); //Cancel the Invoke

		generateObstacles = generateObstacles ? false : true; //Switch between True/False
	}
}
