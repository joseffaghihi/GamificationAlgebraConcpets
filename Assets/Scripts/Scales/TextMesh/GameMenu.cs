using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour 
{
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(KeyCode.Escape))
            GetComponent<CanvasGroup>().alpha = 1.0F;
	}

    public void LoadMainMenu()
    {
        Application.LoadLevel("Menu");
    }
}
