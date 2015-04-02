using UnityEngine;
using System.Collections;

public class TutorialButton : MonoBehaviour {

	public void ChangeToScene(string levelToLoad)
	{
		Application.LoadLevel(levelToLoad);
	}
}
