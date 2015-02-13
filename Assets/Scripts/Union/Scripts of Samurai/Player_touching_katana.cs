using UnityEngine;
using System.Collections;

public class Player_touching_katana : MonoBehaviour {

	public static int trueKatanaIsDestroyed;
	public static int falseKatanaIsDestroyed;
	public static int hasKatana;

	void Start(){
		trueKatanaIsDestroyed = 0;
		falseKatanaIsDestroyed = 0;
		hasKatana = 0;
	}


	void OnTriggerEnter2D(Collider2D otherCollider){
		if(otherCollider.gameObject.CompareTag("katana_true") && hasKatana == 0){
			otherCollider.renderer.enabled = false;
			trueKatanaIsDestroyed = 1;
			hasKatana = 1;
		}else if(otherCollider.gameObject.CompareTag("katana_false") && hasKatana == 0){
			otherCollider.renderer.enabled = false;
			falseKatanaIsDestroyed = 1;
			hasKatana = 1;
		}


	}

	// Update is called once per frame
	void Update () {
		//Debug.Log ("isDestroyed is: " + isDestroyed);
	}
}
