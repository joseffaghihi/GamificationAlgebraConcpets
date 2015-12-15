using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class DisplayDynamite1 : MonoBehaviour 
	{
		public TextMesh text; 
		public bool flag = true;
		public int[] Dynamite1Array = new int[25];
		
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
				Dynamite1Array = StartNumber_script.getFacters(StartNumber_script.DisplayedNumbers[0]);	
				flag=false;
			}
		}
	}
}
