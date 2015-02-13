using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Instantiate_elements_in_a_circle_test : MonoBehaviour {

	public GameObject center;

	public float radius = 1f;

	
	List<GameObject> elements_in_test = new List<GameObject>();
	
	
	void Start() {

		GameObject e1 = GameObject.Find ("fire_1");
		GameObject e2 = GameObject.Find ("air_1");
		GameObject e3 = GameObject.Find ("gold_1");
		GameObject e4 = GameObject.Find ("water_1");


		elements_in_test.Add (e1);
		elements_in_test.Add (e2);
		elements_in_test.Add (e3);
		elements_in_test.Add (e4);


		for (int i = 0; i < elements_in_test.Count; i++){
			Vector3 pos = new Vector3 (i,i,0);

			elements_in_test[i] = (GameObject)Instantiate(elements_in_test[i],pos, transform.rotation);

		}



		for (int i = 0; i < elements_in_test.Count; i++) {
			float angle = i * Mathf.PI * 2 / elements_in_test.Count;
			Vector3 pos = (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), -10)* radius)+center.transform.position;
			elements_in_test[i].transform.position = pos;
			elements_in_test[i].transform.parent = GameObject.Find ("test").transform;
			
		}

	}
	
	// Update is called once per frame
	void Update () {

	}
}