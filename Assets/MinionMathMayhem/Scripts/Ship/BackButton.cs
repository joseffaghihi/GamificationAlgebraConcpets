using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
	public class BackButton : MonoBehaviour {
		
		// GameObject References
		private GameObject forwardButton;	// reference to the gameObject this script is on
		// private GameObject backButton;	// refernece to the backButton gameObject
		private GameObject rulesPgOne;		// reference to the Rules_pg01 gameObject	
		private GameObject rulesPgTwo;		// reference to the Rules_pg02 gameObject

		public AudioSource clickSource;		// reference to an audio sound that isn't in the game yet ------ 9/20/15 --------
		
		// Use this for initialization
		void Start () {
			// Initializing GameObject references and checking if they are filled
			forwardButton = GameObject.Find ("GoForward");
			if(forwardButton == null)
			Debug.Log ("forwardButton was not initialized");
			// backButton = GameObject.Find ("GoBack");
			// if(backButton == null)
				// Debug.Log ("backButton was not initialized");
			rulesPgOne = GameObject.Find("Rules_pg01");
			if(rulesPgOne == null)
				Debug.Log ("rulesPgOne was not initialized");
			rulesPgTwo = GameObject.Find ("Rules_pg02");
			if(rulesPgTwo == null)
				Debug.Log ("rulesPgTwo was not initialized");
		}


		void OnTriggerEnter()
		{
			PressBack();
		}
		
		private void PressBack()
		{
			clickSource.Play();				// plays sound on the audio source
			rulesPgOne.SetActive(true);		// sets rulesPgOne to inactive
			rulesPgTwo.SetActive (false);	// sets rulesPgTwo to active 
		}
		

	} // end class
} // end namespace