using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class DeleteWire : MonoBehaviour 
	{
		
		public DeleteCube script_DeleteCube;
		public StartNumber StartNumber_script;
		
		private void Awake()
		{
			StartNumber_script = GameObject.Find("StartCube").GetComponentInChildren<StartNumber>();
		}
		
		void Update () 
		{
			if (Input.GetMouseButtonDown (0)) 
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				// Casts the ray and get the first game object hit
				Physics.Raycast (ray, out hit);
				
				if (hit.collider.name == this.name) 
				{
					if(this.name == "Wire1")
					{
						StartNumber_script.cubesRemaining [0] = false;
						Destroy (GameObject.Find (hit.transform.name));
						script_DeleteCube = StartNumber_script.CubeArray[0].GetComponent<DeleteCube> ();
						script_DeleteCube.Access_MyMethod ();
						StartNumber_script.deletedCounter++;
					}
					else if(this.name == "Wire2")
					{
						StartNumber_script.cubesRemaining [1] = false;
						Destroy (GameObject.Find (hit.transform.name));
						script_DeleteCube = StartNumber_script.CubeArray[1].GetComponent<DeleteCube> ();
						script_DeleteCube.Access_MyMethod ();
						StartNumber_script.deletedCounter++;
					}
					else if(this.name == "Wire3")
					{
						StartNumber_script.cubesRemaining [2] = false;
						Destroy (GameObject.Find (hit.transform.name));
						script_DeleteCube = StartNumber_script.CubeArray[2].GetComponent<DeleteCube> ();
						script_DeleteCube.Access_MyMethod ();
						StartNumber_script.deletedCounter++;
					}
					else if(this.name == "Wire4")
					{
						StartNumber_script.cubesRemaining [3] = false;
						Destroy (GameObject.Find (hit.transform.name));
						script_DeleteCube = StartNumber_script.CubeArray[3].GetComponent<DeleteCube> ();
						script_DeleteCube.Access_MyMethod ();
						StartNumber_script.deletedCounter++;
					}
					else if(this.name == "Wire5")
					{
						StartNumber_script.cubesRemaining [4] = false;
						Destroy (GameObject.Find (hit.transform.name));
						script_DeleteCube = StartNumber_script.CubeArray[4].GetComponent<DeleteCube> ();
						script_DeleteCube.Access_MyMethod ();
						StartNumber_script.deletedCounter++;
					}
				}
			}
		}//end of update
		
	}//end of class
}//end of namespace