using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
	public class ForwardButton : MonoBehaviour {

		// GameObject References
		// private GameObject forwardButton;	// reference to the gameObject this script is on
		private GameObject backButton;		// refernece to the backButton gameObject
		private GameObject rulesPgOne;		// reference to the Rules_pg01 gameObject	
		private GameObject rulesPgTwo;		// reference to the Rules_pg02 gameObject

		public AudioSource clickSource;		// refernce to audio source with click sound

		// Use this for initialization
		void Awake () {
			// Initializing GameObject references and checking if they are filled
			// forwardButton = GameObject.Find ("GoForward");
			// if(forwardButton == null)
				// Debug.Log ("forwardButton was not initialized");
			backButton = GameObject.Find ("GoBack");
			if(backButton == null)
				Debug.Log ("backButton was not initialized");
			rulesPgOne = GameObject.Find("Rules_pg01");
			if(rulesPgOne == null)
				Debug.Log ("rulesPgOne was not initialized");
			rulesPgTwo = GameObject.Find ("Rules_pg02");
			if(rulesPgTwo == null)
				Debug.Log ("rulesPgTwo was not initialized");
            if (rulesPgOne != null)
                Debug.Log("Ok, the initializations are working!");
		}

        public void Press()
        {
            PressForward();
        }

		public void PressForward()
        {
			clickSource.Play();
            if(BackButton.iterator >= 1 && BackButton.iterator < 4)
			{
				BackButton.iterator++;
				BackButton.RulesPageFlip();
			}
        }

	} // end class
} // end namespace