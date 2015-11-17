using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class DisplayCube5 : MonoBehaviour 
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
				text.text = StartNumber_script.ArrayB [4].ToString ();
				StartNumber_script.CubeArray [4] = this.gameObject;
				flag=false;
			}
		}
	}
}