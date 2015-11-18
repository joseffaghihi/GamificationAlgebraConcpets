using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class DisplayDynamite1 : MonoBehaviour 
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
				text.text = StartNumber_script.DisplayedNumbers [0].ToString ();
				StartNumber_script.DynamiteArray[0] = this.gameObject;
				flag=false;
			}
		}
	}
}
