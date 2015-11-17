using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class DisplayDynamite4 : MonoBehaviour 
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
				text.text = StartNumber_script.DisplayedNumbers [3].ToString ();
				StartNumber_script.DynamiteArray[3] = this.gameObject;
				flag=false;
			}
		}
	}
}