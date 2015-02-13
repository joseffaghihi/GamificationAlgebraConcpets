using UnityEngine;
using System.Collections;

public class Attach_to_test : MonoBehaviour {

	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		 //katanaCheck = GetComponent<Player_touching_katana> ();

		//Debug.Log ("isDestroyed is: " + Player_touching_katana.isDestroyed);
		if (Player_touching_katana.trueKatanaIsDestroyed == 1) {
			foreach(Transform child in gameObject.transform){
				Destroy(child.gameObject);
			}
		}
		
	}
}
