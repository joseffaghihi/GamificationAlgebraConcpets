using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class DisplayDynamite3 : MonoBehaviour 
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
				text.text = StartNumber_script.DisplayedNumbers [2].ToString ();
				StartNumber_script.DynamiteArray[2] = this.gameObject;
				flag=false;
			}
		}
	}
}