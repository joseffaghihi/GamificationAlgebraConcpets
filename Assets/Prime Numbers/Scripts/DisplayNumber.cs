using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class DisplayNumber : MonoBehaviour 
	{
		//public bool flag0;
		public TextMesh[] textFArray; //Holds the textMeshes in the inspector in an array
			
		public StartNumber StartNumber_script;
			
		private void Awake()
		{
				StartNumber_script = GameObject.Find("StartCube").GetComponentInChildren<StartNumber>();
		}

		void Start()
		{

			//flag0=true;

		}
			
		void Update ()
		{
			textFArray[0].text=StartNumber_script.ArrayB [0].ToString ();
			textFArray[1].text=StartNumber_script.ArrayB [1].ToString ();
			textFArray[2].text=StartNumber_script.ArrayB [2].ToString ();
			textFArray[3].text=StartNumber_script.ArrayB [3].ToString ();
			textFArray[4].text=StartNumber_script.ArrayB [4].ToString ();
	}
}
}


/*		void Update ()
		{
			if (flag0) 
			{
				foreach (TextMesh text in textFArray) 
				{
					text.text = StartNumber_script.ArrayB [StartNumber_script.displayCounter].ToString ();
					StartNumber_script.displayCounter++;
				}
				flag0=false;
				StartNumber_script.displayCounter=0;
			}
		}
		*/