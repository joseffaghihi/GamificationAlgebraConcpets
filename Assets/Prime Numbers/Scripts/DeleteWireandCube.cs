using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class DeleteWireandCube : MonoBehaviour 
	{
		HingeJoint _hingeJoint;
		public StartNumber StartNumber_script;
		
		public void Awake () 
		{
			_hingeJoint = GetComponent<HingeJoint>();
			StartNumber_script = GameObject.Find("StartCube").GetComponentInChildren<StartNumber>();
		}
		
		public void OnMouseDown() 
		{

			if(_hingeJoint.connectedBody.gameObject.name == "Wire1")
			{
				StartNumber_script.cubesRemaining [0] = false;
				Destroy (_hingeJoint.connectedBody.gameObject);
				Destroy (gameObject);
				StartNumber_script.deletedCounter++;
			}
			else if(_hingeJoint.connectedBody.gameObject.name == "Wire2")
			{
				StartNumber_script.cubesRemaining [1] = false;
				Destroy (_hingeJoint.connectedBody.gameObject);
				Destroy (gameObject);
				StartNumber_script.deletedCounter++;
			}
			else if(_hingeJoint.connectedBody.gameObject.name == "Wire3")
			{
				StartNumber_script.cubesRemaining [2] = false;
				Destroy (_hingeJoint.connectedBody.gameObject);
				Destroy (gameObject);
				StartNumber_script.deletedCounter++;
			}
			else if(_hingeJoint.connectedBody.gameObject.name == "Wire4")
			{
				StartNumber_script.cubesRemaining [3] = false;
				Destroy (_hingeJoint.connectedBody.gameObject);
				Destroy (gameObject);
				StartNumber_script.deletedCounter++;
			}
			else if(_hingeJoint.connectedBody.gameObject.name == "Wire5")
			{
				StartNumber_script.cubesRemaining [4] = false;
				Destroy (_hingeJoint.connectedBody.gameObject);
				Destroy (gameObject);
				StartNumber_script.deletedCounter++;
			}
		}

	}//end of class
}//end of namespace
