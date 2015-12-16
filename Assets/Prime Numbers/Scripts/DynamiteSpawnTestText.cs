using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PrimeNumbers
{
	public class DynamiteSpawnTestText : MonoBehaviour 
	{

		public Text text;
		public int Nr1;

		private void Awake () 
		{
			// Canvas with text needs to be at the top of the hierarchy
			text = gameObject.transform.GetChild (0).gameObject.transform.GetChild (0).GetComponent<Text>();
			Debug.Log (text == null ? "text is null" : "text is not null");
		}

		void Start () 
		{
			StartCoroutine (initNr1());
				
			if (!isPrime (Nr1)) 
			{
			}
		}
	
		IEnumerator initNr1() 
		{
			while(Nr1 == 0)
				yield return null;

			text.text = Nr1.ToString ();
			print ("Nr1 initialized.");
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