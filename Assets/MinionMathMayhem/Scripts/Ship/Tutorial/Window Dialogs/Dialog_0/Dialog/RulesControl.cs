using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship {

	public class RulesControl : MonoBehaviour {

        // ----------------------------------- Data Members Private and Public ------------------------- //
        //********************************************************************************************** //

        public static bool control;		// control variable for the function WaitForRulesToFinish
		
		public static GameObject pg1;
		public static GameObject pg2;
        public static GameObject pg3;
        public static GameObject pg4;
		public static GameObject forward;
		public static GameObject backward;
		public static GameObject close;
        public static int iterator = 1;

        // ----------------------------------- Unity Event Functions ----------------------------------- //
        //  ******************************************************************************************** //

        void Awake() {
			control = true;
		}


        // ----------------------------------- User Defined Functions Private and Public -------------------------- //
        // ******************************************************************************************************** //

        /// <summary>
        /// This function will constantly loop and return until the 'control'
        /// variable is set to true
        /// </summary>
        private IEnumerator WaitForRulesToFinish()
        {
            pg1.SetActive(true);
            forward.SetActive(true);
            backward.SetActive(true);
            close.SetActive(true);
            while (control)
            {
                yield return null;
            }
        }


        // Allows other scripts to start 'WaitForRulesToFinish()'
        public IEnumerator Access_WaitForRulesToFinish() {
			yield return StartCoroutine (WaitForRulesToFinish());
			yield return null;
		}


		// This method will allow you to set the while loops
		// Control variable inside of the 'WaitForRulesToFinish()'.
		public void SetControl() {
			control = false;
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
        /// Disable all gameObjects associated with Rules display
        /// </summary>
        public static void DisableAll()
        {
            DisableRules();
            forward.SetActive(false);
            backward.SetActive(false);
            close.SetActive(false);
        }


        /// <summary>
        /// Disables all pages that display rules
        /// </summary>
        public static void DisableRules()
        {
            pg1.SetActive(false);
            pg2.SetActive(false);
            pg3.SetActive(false);
            pg4.SetActive(false);
        }
    } // End of Class
} // End of namespace
