using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class DisplayDynamite4 : MonoBehaviour 
	{
		public TextMesh text; 
		public bool flag = true;
		public int[] Dynamite4Array = new int[25];
		
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
				Dynamite4Array = StartNumber_script.getFacters (StartNumber_script.DisplayedNumbers [3]);
				flag=false;
			}
		}
	}
}