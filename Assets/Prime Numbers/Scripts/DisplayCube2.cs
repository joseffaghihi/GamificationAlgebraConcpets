using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class DisplayCube2 : MonoBehaviour 
	{
		public TextMesh text; 
		public bool flag = true;

		public StartNumber StartNumber_script;
		
		private void Awake()
		{
			StartNumber_script = GameObject.Find("StartCube").GetComponentInChildren<StartNumber>();
		}
		
		void Update () 
		{
			if (flag) {
				text.text = StartNumber_script.ArrayB [1].ToString ();
				StartNumber_script.CubeArray [1] = this.gameObject;
				flag=false;
			}
		}
	}
}