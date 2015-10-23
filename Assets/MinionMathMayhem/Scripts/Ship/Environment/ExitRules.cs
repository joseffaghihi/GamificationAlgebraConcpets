using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship {
	public class ExitRules : MonoBehaviour {
// ----------------------------------- Data Members Private and Public ------------------------- //
//********************************************************************************************** //

		public AudioSource clickSource; 			// reference to the 'Rules Canvas' audio source


// ----------------------------------- User Defined Functions Private and Public -------------------------- //
// ******************************************************************************************************** //
		
		/// <summary>
		/// This function gives public access to call ClickExit()
		/// </summary>
		public void Access_ClickExit() {
			ClickExit ();
		}

		/// <summary>
		/// Plays the click noise first and then
		/// Deactivates all Components of the rules canvas besides the rules canvas itself
		/// </summary>
		private void ClickExit() {
			clickSource.Play ();
			RulesControl.control = false;
			RulesControl.DisableAll();
		}

	} // end class
} // End namepace

