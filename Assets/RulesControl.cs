using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship {

	public class RulesControl : MonoBehaviour {

// ----------------------------------- Data Members Private and Public ------------------------- //
//********************************************************************************************** //

		public static bool control;		// control variable for the function WaitForRulesToFinish


// ----------------------------------- Unity Event Functions ----------------------------------- //
//  ******************************************************************************************** //

		void Awake() {
			control = true;
		}

// ----------------------------------- User Defined Functions Private and Public -------------------------- //
// ******************************************************************************************************** //

		// This function will constantly loop and return until the 'control'
		// variable is set to true
		private IEnumerator WaitForRulesToFinish() {
			while(control) {
				yield return new WaitForSeconds(5.0f);
				Debug.Log (control);
			}
		}


		// Allows other scripts to start 'WaitForRulesToFinish()'
		public IEnumerator Access_WaitForRulesToFinish() {
			// Time.timeScale = 0.2f;
			StartCoroutine (WaitForRulesToFinish());
			// Time.timeScale = 1.0f;
			yield return null;
		}


		// This method will allow you to set the while loops
		// Control variable inside of the 'WaitForRulesToFinish()'.
		public void SetControl() {
			control = false;
		}
	} // End of Class
} // End of namespace