using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class PrimeNumberScript : MonoBehaviour 
	{
		public TextMesh text;

		public StartNumber StartNumber_script;

		private void Awake()//Nicholas told me to make this function and everything inside of it.
		{
			StartNumber_script = GameObject.Find("StartCube").GetComponentInChildren<StartNumber>();
		}

		void Start ()
		{
			text.text = StartNumber_script.remainingNumbers[0].ToString();
		}
	}
}
