using UnityEngine;
using System.Collections;


public class QuitButton : MonoBehaviour {
	
	public void QuitGame(string levelToLoad)
	{
		Application.Quit ();
	}
}
