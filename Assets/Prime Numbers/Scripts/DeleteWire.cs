using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class DeleteWire : MonoBehaviour {

		public DeleteCube script_DeleteCube;


		void Start () {
		
		}

		void Update () 
		{/*
			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				// Casts the ray and get the first game object hit
				Physics.Raycast (ray, out hit);

					if (hit.collider.name == transform.parent.gameObject.name) {
						Destroy (GameObject.Find (hit.transform.name));
						script_DeleteCube = CubeArray [0].GetComponent<DeleteCube> ();
						script_DeleteCube.Access_MyMethod ();
						deletedCounter++;
						cubesRemaining [0] = false;
						flag0 = false;
					}
				}
			}*/
		}//end of update
	}//end of class
}//end of namespace