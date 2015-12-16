using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class DynamiteSpawnTestTest : MonoBehaviour 
	{

		public TextMesh text;
		public int Nr1;

		private void Awake () 
		{
			// TODO: don't need this fixed below
			//Nr1 = int.Parse(text.text);

			//Check to see if the random number is prime.
			/*if (isPrime (Nr1) == true) 
			{

			}*/

			// TODO: fix to above problem
			text = GetComponent<TextMesh>();
			// TODO: Nr1 is set from test script just like you asked
			text.text = Nr1.ToString ();
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