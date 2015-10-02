using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour 
{
    /** 
     * Generate Obstacles
     * Place number on each obstacle
     */

    public GameObject obstacle;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
	private float initWaveWait;

    private int tempRound; //Store the round the user is on

	GameControl gc = new GameControl();

    //Start spawning the waves
    void Start()
    {
        tempRound = gc.GetCurrentRound(); //Set the tempRound to the current round
		initWaveWait = waveWait; //Set the initial spawnWait
        StartCoroutine(SpawnWaves());
    }

    //Function for specific events during spawn phase
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            gc.DelayWave(false); //Reset Delay
			waveWait = initWaveWait; //Reset waveWait
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(obstacle, spawnPosition, spawnRotation);
				if(gc.getDelayWave())
				{
                    //Reset Current Round Time whenever the player hits any obstacle
                    gc.setCurrentRoundTime(-1);

					i = hazardCount;
					waveWait = 8;

                    //Find all Obstacles on the screen
                    GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Obstacle");

                    //Destroy all Obstacles on the screen
                    for (int j = 0; j < gameObjects.Length; j++)
                        Destroy(gameObjects[j]);
				}
                yield return new WaitForSeconds(spawnWait);
            }

            //Add Current Round
            gc.addCurrentRoundTime(); //Add another wave the user has been in the same round

            yield return new WaitForSeconds(waveWait);
        }
    }
}
