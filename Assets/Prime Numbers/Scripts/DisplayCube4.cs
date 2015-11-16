using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class DisplayCube4 : MonoBehaviour 
	{
		public TextMesh text; 
		public bool flag = true;
		
		public StartNumber StartNumber_script;
		
		private void Awake()
		{
			StartNumber_script = GameObject.Find("Start Dynamite").GetComponentInChildren<StartNumber>();
		}
		
		void Update () 
		{
			if (flag) {
				text.text = StartNumber_script.ArrayB [3].ToString ();
				StartNumber_script.CubeArray [3] = this.gameObject;
				flag=false;
			}
		}
	}
}