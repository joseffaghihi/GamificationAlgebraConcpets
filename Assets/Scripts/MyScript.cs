using UnityEngine;
using System.Collections;

public class MyScript : MonoBehaviour {

    Vector3 defaultScale;

	// Use this for initialization
	void Start () {

        Debug.Log ("Hi");
        PrintOut();

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        
                defaultScale = transform.localScale;


                  Vector3 scale = defaultScale;


                 scale.x = scale.x *0.9f;
                 scale.y = scale.y * 0.9f;
                 scale.z = scale.z * 0.9f;

                 transform.localScale = scale;


    }

   void  PrintOut(){
       Debug.Log ("Yes firing");


    }
}
