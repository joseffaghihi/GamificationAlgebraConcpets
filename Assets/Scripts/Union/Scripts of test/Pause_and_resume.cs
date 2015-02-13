using UnityEngine;
using System.Collections;

public class Pause_and_resume : MonoBehaviour {
	public GameObject results_false;
	public GameObject results_true;
	public GameObject pause_screen;
	//private float myTimer = 0.3f;
	// Use this for initialization
	void Start () {
		results_false.renderer.enabled = false;
		results_true.renderer.enabled = false;
		pause_screen.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Player_touching_katana.trueKatanaIsDestroyed == 1|| Player_touching_katana.falseKatanaIsDestroyed == 1) {
			StartCoroutine(Pause());

		}
	}

	IEnumerator Pause(){

		Time.timeScale = 0.00001f;
		if(Player_touching_katana.trueKatanaIsDestroyed == 1){
			results_true.renderer.enabled = true;
			pause_screen.renderer.enabled = true;
			yield return new WaitForSeconds(3*Time.timeScale);
			Time.timeScale = 1;
			results_true.renderer.enabled = false;
			pause_screen.renderer.enabled = false;
		}else if(Player_touching_katana.falseKatanaIsDestroyed == 1){
			results_false.renderer.enabled = true;
			pause_screen.renderer.enabled = true;
			yield return new WaitForSeconds(3*Time.timeScale);
			Time.timeScale = 1;
			results_false.renderer.enabled = false;
			pause_screen.renderer.enabled = false;
		}
	}


}

