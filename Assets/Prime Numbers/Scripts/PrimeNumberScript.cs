using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class PrimeNumberScript : MonoBehaviour 
	{
		public TextMesh text;
		
		public StartNumber StartNumber_script;
		
		private void Awake()
		{
			StartNumber_script = GameObject.Find("Start Dynamite").GetComponentInChildren<StartNumber>();
		}
		
		void Update ()
		{
			text.text = StartNumber_script.remainingNumbers[StartNumber_script.FactorIsPrime].ToString();
		}
	}
}