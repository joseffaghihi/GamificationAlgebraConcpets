using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class ResetButton : MonoBehaviour {

	public void ChangeToScene(string levelToLoad)
	{
		Application.LoadLevel(levelToLoad);
	}
}
