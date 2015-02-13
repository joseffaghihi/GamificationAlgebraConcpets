using UnityEngine;
using System.Collections;

public class Attach_to_player : MonoBehaviour {
	public GameObject target;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = target.transform.position;
		transform.position = pos;
		/*
		if (GameObject.Find("katana") == null) {
			Destroy(gameObject);
		}
		*/
		if (Player_touching_katana.trueKatanaIsDestroyed == 1 || Player_touching_katana.falseKatanaIsDestroyed == 1) {
			/*
			foreach(Transform child in gameObject.transform){
				Destroy(child.gameObject);
			}
			*/
			foreach (Transform child in gameObject.transform) {
				child.renderer.enabled = false;
			}
			
		} else {
			foreach (Transform child in gameObject.transform) {
				child.renderer.enabled = true;
			}
		}
	}
}
