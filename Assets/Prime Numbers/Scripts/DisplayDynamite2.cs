using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class DisplayDynamite2 : MonoBehaviour 
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
				text.text = StartNumber_script.DisplayedNumbers [1].ToString ();
				StartNumber_script.DynamiteArray[1] = this.gameObject;
				flag=false;
			}
		}
	}
}