using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship {

	public class RulesControl : MonoBehaviour {

// ----------------------------------- Data Members Private and Public ------------------------- //
//********************************************************************************************** //

		public static bool control;		// control variable for the function WaitForRulesToFinish
		
		public GameObject pg1;
		public GameObject pg2;
		public GameObject forward;
		public GameObject backward;
		public GameObject close;


// ----------------------------------- Unity Event Functions ----------------------------------- //
//  ******************************************************************************************** //

		void Awake() {
			control = true;
		}

		void Start() {
			pg1.SetActive(false);
			pg2.SetActive(false);
			forward.SetActive(false);
			backward.SetActive(false);
			close.SetActive(false);
		}
// ----------------------------------- User Defined Functions Private and Public -------------------------- //
// ******************************************************************************************************** //

		// This function will constantly loop and return until the 'control'
		// variable is set to true
		private IEnumerator WaitForRulesToFinish() {
			pg1.SetActive(true);
			pg2.SetActive(true);
			forward.SetActive(true);
			backward.SetActive(true);
			close.SetActive(true);
			while(control) {
				yield return new WaitForSeconds(5.0f);
				Debug.Log (control);
                // horrible gross hack
                    yield return new WaitForSeconds(3);
                    Debug.Log("LIVING ON THE EDGE!");
                    control = false;
			}
		}


		// Allows other scripts to start 'WaitForRulesToFinish()'
		public IEnumerator Access_WaitForRulesToFinish() {
			yield return (StartCoroutine (WaitForRulesToFinish()));
			yield return null;
		}


		// This method will allow you to set the while loops
		// Control variable inside of the 'WaitForRulesToFinish()'.
		public void SetControl() {
			control = false;
		}
	} // End of Class
} // End of namespace
