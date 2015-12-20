using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PrimeNumbers
{
	public class TestScriptText : MonoBehaviour 
	{

		public TextMesh text;
		public static int RN;

		private void Awake () 
		{
			text = GetComponent<TextMesh> ();
			RN = Random.Range(1,100);
			
			//Check to see if the random number is prime.
			if (isPrime (RN) == true) 
			{
				do {
					RN = Random.Range (1, 100);
				} while(isPrime (RN)==true);
			}

			RN = 96;
			Debug.Log ("SN=" + RN);

			text.text = RN.ToString();
		}

		public bool isPrime(int Nr1)
		{
			for(int i=2; i<Nr1;i++)
			{
				if(Nr1%i==0)
					return false;
			}
			return true;
		}
	}
}