using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Instantiate_elements_in_a_circle_player_before: MonoBehaviour {


	public GameObject center;
	public float radius = 1f;
	//public float rotate_speed = 5.0f;

	List<GameObject> elements_in_samurai = new List<GameObject>();

	//elements_in_samurai contains
	//soil
	//water
	//wind
	//wood
	void Start() {
		GameObject e1 = GameObject.Find ("soil");
		GameObject e2 = GameObject.Find ("water");
		GameObject e3 = GameObject.Find ("wind");
		GameObject e4 = GameObject.Find ("wood");
		elements_in_samurai.Add (e1);
		elements_in_samurai.Add (e2);
		elements_in_samurai.Add (e3);
		elements_in_samurai.Add (e4);

		for (int i = 0; i < elements_in_samurai.Count; i++){
			Vector3 pos = new Vector3 (i,i,0);
			
			elements_in_samurai[i] = (GameObject)Instantiate(elements_in_samurai[i],pos, transform.rotation);
			
		}

		for (int i = 0; i < elements_in_samurai.Count; i++) {
			float angle = i * Mathf.PI * 2 / elements_in_samurai.Count;
			Vector3 pos = (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), -10)* radius)+center.transform.position;
			elements_in_samurai[i].transform.position = pos;
			elements_in_samurai[i].transform.parent = GameObject.Find ("playerElements").transform;
			//Instantiate(elements[i], pos, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {

		//transform.Rotate (new Vector3 (0, 0, 15)*Time.deltaTime*rotate_speed);
	}
}
