﻿using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship {

	public class RulesControl : MonoBehaviour {

// ----------------------------------- Data Members Private and Public ------------------------- //
//********************************************************************************************** //

		public static bool control;				// control variable for the function WaitForRulesToFinish
		public static GameObject pg1;			// reference to 'Rules_pg01' gameObject
		public static GameObject pg2;			// reference to 'Rules_pg02' gameObject
		public static GameObject pg3;			// refernece to 'Rules_pg03' gameObject
		public static GameObject pg4;			// reference to 'Rules_pg04' gameObject
		public static GameObject forward;		// reference to 'GoForward' gameObject
		public static GameObject backward;		// reference to 'GoBack' gameObject
		public static GameObject close;			// refernce to 'ExitButton' gameObject
		public static int iterator = 1;			// control variable for cycling through
												// Rules pages to display
												
		public static RulesControl instance;


// ----------------------------------- Unity Event Functions ----------------------------------- //
//  ******************************************************************************************** //

		void Awake() {
			if(instance == null) { 
				instance = this;
			} else {
				Destroy(gameObject);
			}
			GetRulesReferences();
			control = true;
		}
		
		void Start () {
			RulesPageFlip();
		}

// ----------------------------------- User Defined Functions Private and Public -------------------------- //
// ******************************************************************************************************** //

		/// <summary>
		/// This function will constantly loop and return until the 'control'
		/// variable is set to true
		/// </summary>
		private IEnumerator WaitForRulesToFinish() {
			pg1.SetActive(true);
			forward.SetActive(true);
			backward.SetActive(true);
			close.SetActive(true);
			while(control) {
				yield return null;
			}
		}

		/// <summary>
		/// Allows other scripts to start 'WaitForRulesToFinish()'
		/// </summary>
		public void Access_WaitForRulesToFinish() {
			StartCoroutine (WaitForRulesToFinish());
		}
		
		/// <summary>
		/// Disables all pages that display rules
		/// </summary>
		public static void DisableRules() {
			pg1.SetActive(false);
			pg2.SetActive(false);
			pg3.SetActive(false);
			pg4.SetActive(false);
		}
		
		/// <summary>
		/// Disable all gameObjects associated with Rules display
		/// </summary>
		public static void DisableAll() {
			DisableRules();
			forward.SetActive(false);
			backward.SetActive(false);
			close.SetActive(false);
		}

		/// <summary>
		/// Cycles through which rules page to display
		/// Based on the 'iterator' boolean variable
		/// </summary>
		public static void RulesPageFlip()
		{
			switch (iterator)
			{
				case 1:
				DisableRules();
				pg1.SetActive(true);
				break;
				case 2:
				DisableRules();
				pg2.SetActive(true);
				break;
				case 3:
				DisableRules();
				pg3.SetActive(true);
				break;
				case 4:
				DisableRules();
				pg4.SetActive(true);
				break;
				default:
				Debug.Log("No Rules to display");
				break;
			}
		}
		
		/// <summary>
		/// Initializes all rules canvas references
		/// </summary>
		private void GetRulesReferences() {
			pg1 = GameObject.Find("Rules_pg01");
			if(pg1 != null)
				Debug.Log("pg1 initialized");
			pg2 = GameObject.Find("Rules_pg02");
			if(pg2 != null)
				Debug.Log("pg2 initialized");
			pg3 = GameObject.Find("Rules_pg03");
			if(pg3 != null)
				Debug.Log("pg3 initialized");
			pg4 = GameObject.Find("Rules_pg04");
			if(pg4 != null)
				Debug.Log("pg4 initialized");
			forward = GameObject.Find("GoForward");
			if(forward != null)
				Debug.Log("forward initialized");
			backward = GameObject.Find("GoBack");
			if(backward != null)
				Debug.Log("backward initialized");
			close = GameObject.Find("ExitButton");
			if(close != null)
				Debug.Log("Close initialized");
		}
	} // End of Class
} // End of namespace