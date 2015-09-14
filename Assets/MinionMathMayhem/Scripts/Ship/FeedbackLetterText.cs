using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace MinionMathMayhem_Ship
{
	public class FeedbackLetterText : MonoBehaviour {

		// text component on the fb_letter game object
		private Text texty;

		void Start()
		{
			texty = GetComponent<Text>();
		}

		// Changes the fb_letter's text
		private void FeedbackLetterChange(char lett)
		{
			texty.text = lett.ToString ();
			Debug.Log ("Char captured: " + lett.ToString ());
		}

		public void Access_FeedbackLetterChange(char letty)
		{
			FeedbackLetterChange (letty);
		}

	} // class
} // namespace