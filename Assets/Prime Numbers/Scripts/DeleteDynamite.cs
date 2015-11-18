using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class DeleteDynamite : MonoBehaviour 
	{

		public StartNumber StartNumber_script;
		
		public void Awake () 
		{
			StartNumber_script = GameObject.Find("Start Dynamite").GetComponentInChildren<StartNumber>();
		}

		public void OnMouseDown() 
		{
			if(gameObject.name == "Dynamite1")
			{
				StartNumber_script.DynamitesRemaining [0] = false;
				Destroy (gameObject);
				StartNumber_script.deletedCounter++;
			}
			else if(gameObject.name == "Dynamite2")
			{
				StartNumber_script.DynamitesRemaining [1] = false;
				Destroy (gameObject);
				StartNumber_script.deletedCounter++;
			}
			else if(gameObject.name == "Dynamite3")
			{
				StartNumber_script.DynamitesRemaining [2] = false;
				Destroy (gameObject);
				StartNumber_script.deletedCounter++;
			}
			else if(gameObject.name == "Dynamite4")
			{
				StartNumber_script.DynamitesRemaining [3] = false;
				Destroy (gameObject);
				StartNumber_script.deletedCounter++;
			}
			else if(gameObject.name == "Dynamite5")
			{
				StartNumber_script.DynamitesRemaining [4] = false;
				Destroy (gameObject);
				StartNumber_script.deletedCounter++;
			}
		}//end of OnMouseDown
	}//end of class 
}//end of namespace
