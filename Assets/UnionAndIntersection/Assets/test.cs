using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class test: MonoBehaviour{
	void Start(){
		float x = 2;
		int y = 3;
		y= Addition (x,y);
		Debug.Log ("The addition is:" + y);
	}

	int Addition(float x, int y){

		y += (int)x;
		return y;
	}
}
