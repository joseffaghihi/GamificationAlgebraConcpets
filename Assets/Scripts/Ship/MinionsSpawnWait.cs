using UnityEngine;
using System.Collections;

public class MinionsSpawnWait : MonoBehaviour
{
    // A variable for determining if the minions cleared the scene
    private bool minionsClearedScene;
    // A variable for determining if there exists a timeout of the current scene
    // This should avoid scene hangups
    private bool timeExpired;


	// Use this for initialization
	void Start ()
    {
        minionsClearedScene = false;
        timeExpired = false;
	}


    // For debuging; accessible through other scripts if needed.
    public void DebugCheckMinion()
    {
        // Does there exists creatures within the scene that has the 'minion' tag?
        if (CheckMinions() == true)
            Debug.Log("There is Minions in this scene!!! OMGOMG");
        else
            Debug.Log("There is /NO/ minions in the scene!!!  YA!!!");
    }




    // Check the scene and find any minions within the scene
    public bool CheckMinions()
    {
        if (GameObject.FindGameObjectWithTag("Minion") == null)
            // If there is no minions in the scene
            return false;
        else
            // There is minions in the scene
            return true;
    }



    public IEnumerator WaitForMinionsToClear(int waitTime)
    {
        yield return new WaitForSeconds (waitTime);
    }

}
