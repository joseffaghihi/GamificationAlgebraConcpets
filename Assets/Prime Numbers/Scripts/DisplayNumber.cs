using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class DisplayNumber : MonoBehaviour 
	{
		public bool flag0;
		public TextMesh[] textFArray; //Holds the textMeshes in the inspector in an array
			
		public StartNumber StartNumber_script;
			
		private void Awake()
		{
				StartNumber_script = GameObject.Find("StartCube").GetComponentInChildren<StartNumber>();
		}

		void Start()
		{
			flag0=true;
		}
			
		void Update ()
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
	}
}
