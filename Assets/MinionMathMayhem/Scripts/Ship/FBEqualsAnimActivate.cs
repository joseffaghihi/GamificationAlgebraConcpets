// This script provides a public function to activate the animation
// from the FeedbackAnimations script on the 'fb_equals' gameObject

using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
	public class FBEqualsAnimActivate : MonoBehaviour {

		private Animator anim;

		// initialize anim variable to the gameObjects animator
		void Awake()
		{
			anim = GetComponent<Animator>();
		}

		// Plays the equals animation by setting the trigger in its animator
		private void PlayEqualsAnimation()
		{
			anim.SetTrigger ("Push");
		}

		// For FeedbackAnimations script to call PlayEqualsAnimation()
		// on the '=' sign on the feedback canvas
		public void PlayEqualsAnim()
		{
			PlayEqualsAnimation ();
		}
	} // End FBEqualsAnimActivate class
} // End namespace