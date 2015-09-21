using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship {
	public class ExitRules : MonoBehaviour {

		// Game Object References
		private GameObject next;			// reference to 'Rules Canvas/GoForward'
		private GameObject back;			// reference to 'Rules Canvas/GoBack'
		private GameObject pgOne;			// reference to 'Rules Canvas/Rules_pg01'
		private GameObject pgTwo;			// reference to 'Rules Canvas/Rules_pg02'
		private GameObject exit;

		public AudioSource clickSource; 	// reference to the 'Rules Canvas' audio source

		// Initializes references and checks for initialization
		void Awake() {
			next = GameObject.Find ("GoForward");
			if(next == null)
				print("next was not initialized");
			back = GameObject.Find ("GoBack");
			if(back == null)
				print("back was not initialized");
			pgOne = GameObject.Find ("Rules_pg01");
			if(pgOne == null)
				print("pgOne was not initialized");
			pgTwo = GameObject.Find ("Rules_pg02");
			if(pgTwo == null)
				print("pgTwo was not initialized");
			exit = GameObject.Find ("ExitButton");
				if(exit == null)
				print ("exitbutton was not initialized");
		}


		public void Access_ClickExit() {
			ClickExit ();
		}

		// Plays the click noise first and then
		// Deactivates all Components of the rules canvas besides the rules canvas itself
		private void ClickExit() {
			clickSource.Play ();
			next.SetActive (false);
			back.SetActive (false);
			pgOne.SetActive(false);
			pgTwo.SetActive(false);
			exit.SetActive (false);
		}

	} // end class
} // End namepace

