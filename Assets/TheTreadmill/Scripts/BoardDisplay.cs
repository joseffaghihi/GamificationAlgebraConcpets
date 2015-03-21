using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoardDisplay : MonoBehaviour 
{

    GameControl gameController = new GameControl();

    /**
     * Updating text will later on be moved to a public function to avoid constant updating thereby saving cpu power making it more efficient
     */
	
	// Update is called once per frame
	void Update () 
    {
        GetComponent<Text>().text = "Lives Left: " + gameController.lives  +
                                    "\nSpeed: " + gameController.treadmillSpeed;
	}
}
