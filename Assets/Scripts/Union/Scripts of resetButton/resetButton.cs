using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class resetButton : MonoBehaviour {
	public Rect button = new Rect(455,25,49,36);
	public string buttonLabel = "Reset";
	public string levelToLoad = "Union_Prototype";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnGUI(){
		if (GUI.Button (button, buttonLabel))
			Application.LoadLevel (levelToLoad);
	}
}
