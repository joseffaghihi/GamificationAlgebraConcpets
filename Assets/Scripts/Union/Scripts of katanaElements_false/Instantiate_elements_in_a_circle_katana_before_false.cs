using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Instantiate_elements_in_a_circle_katana_before_false: MonoBehaviour {
	
	public GameObject center;
	public float radius = 1f;
	
	
	List<GameObject> elements_in_katana_false = new List<GameObject>();
	
	
	//elements_in_katana list contains
	//air
	//fire
	//gold
	//soil
	//water
	void Start() {
		
		GameObject e1 = GameObject.Find ("wind");
		GameObject e2 = GameObject.Find ("wood");
		GameObject e3 = GameObject.Find ("gold");
		GameObject e4 = GameObject.Find ("soil");
		GameObject e5 = GameObject.Find ("water");
		elements_in_katana_false.Add (e1);
		elements_in_katana_false.Add (e2);
		elements_in_katana_false.Add (e3);
		elements_in_katana_false.Add (e4);
		elements_in_katana_false.Add (e5);
		
		for (int i = 0; i < elements_in_katana_false.Count; i++){
			Vector3 pos = new Vector3 (i,i,0);
			
			elements_in_katana_false[i] = (GameObject)Instantiate(elements_in_katana_false[i],pos, transform.rotation);
			
		}
		for (int i = 0; i < elements_in_katana_false.Count; i++) {
			float angle = i * Mathf.PI * 2 / elements_in_katana_false.Count;
			Vector3 pos = (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), -10)* radius)+center.transform.position;
			
			elements_in_katana_false[i].transform.position = pos;
			elements_in_katana_false[i].transform.parent = GameObject.Find ("katanaElements_false").transform;
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		
		//transform.Rotate (new Vector3 (0, 0, 15)*Time.deltaTime*rotate_speed);
	}
}

