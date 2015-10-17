using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
	public class BackButton : MonoBehaviour {
		
		// GameObject References
		private GameObject forwardButton;	// reference to the gameObject this script is on
	    private GameObject backButton;		// refernece to the backButton gameObject
		private static GameObject rulesPgOne;		// reference to the Rules_pg01 gameObject	
		private static GameObject rulesPgTwo;		// reference to the Rules_pg02 gameObject
		private static GameObject rulesPgThree;	// reference to the Rules_pg03 gameObject
		private static GameObject rulesPgFour;		// reference to the Rules_pg04 gameObject


		public static int iterator = 1;				// control for displaying different rules canvas
		public AudioSource clickSource;		// reference to an audio sound that isn't in the game yet ------ 9/20/15 --------
		
		// Use this for initialization
		void Awake () {
			// Initializing GameObject references and checking if they are filled
			forwardButton = GameObject.Find ("GoForward");
			if(forwardButton == null)
			Debug.Log ("forwardButton was not initialized");
			backButton = GameObject.Find ("GoBack");
			if(backButton == null)
				Debug.Log ("backButton was not initialized");
			rulesPgOne = GameObject.Find("Rules_pg01");
			if(rulesPgOne == null)
				Debug.Log ("rulesPgOne was not initialized");
			rulesPgTwo = GameObject.Find ("Rules_pg02");
			if(rulesPgTwo == null)
				Debug.Log ("rulesPgTwo was not initialized");
			rulesPgThree = GameObject.Find ("Rules_pg03");
			if(rulesPgThree == null)
				Debug.Log ("rulesPgThree was not initialized");
			rulesPgFour = GameObject.Find ("Rules_pg04");
			if(rulesPgFour == null)
				Debug.Log ("rulesPgFour was not initialized");
		}

        void Start()
        {
            RulesControl.DisableRules();	// disables all rules pages
			RulesPageFlip();
        }

		public void PressBack()
        {
			clickSource.Play();
            if(iterator > 1 && iterator <= 4)
			{
				iterator--;
				RulesPageFlip();
			}
        }
		
		
		public static void RulesPageFlip()
		{
			switch (iterator)
			{
				case 1:
				RulesControl.DisableRules();
				rulesPgOne.SetActive(true);
				break;
				case 2:
				RulesControl.DisableRules();
				rulesPgTwo.SetActive(true);
				break;
				case 3:
				RulesControl.DisableRules();
				rulesPgThree.SetActive(true);
				break;
				case 4:
				RulesControl.DisableRules();
				rulesPgFour.SetActive(true);
				break;
				default:
				Debug.Log("No Rules to display");
				break;
			}
		}
		
		
		
		

	} // end class
} // end namespace