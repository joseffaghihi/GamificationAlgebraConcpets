using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class DisplayDynamite5 : MonoBehaviour 
	{
		public TextMesh text; 
		public bool flag = true;
		public int[] Dynamite5Array = new int[25];
		
		public StartNumber StartNumber_script;
		
		private void Awake()
		{
			StartNumber_script = GameObject.Find("Start Dynamite").GetComponentInChildren<StartNumber>();
		}
		
		void Update () 
		{
			if (flag) {
				text.text = StartNumber_script.DisplayedNumbers [4].ToString ();
				StartNumber_script.DynamiteArray[4] = this.gameObject;
				Dynamite5Array = StartNumber_script.getFacters (StartNumber_script.DisplayedNumbers [4]);
				flag=false;
			}
		}
	}
}