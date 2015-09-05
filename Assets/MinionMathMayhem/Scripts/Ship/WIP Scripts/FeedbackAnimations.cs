using UnityEngine;
using System.Collections;

// This class is responsible for controlling the playback on the feedback animations
// 

public class FeedbackAnimations : MonoBehaviour {
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Plays the animation on the selected game object
	private void PlayAnimation()
	{
		anim.SetTrigger ("GrowShrink");
	}

	// Calls PlayAnimation
	public void AccessPlayAnimation()
	{
		PlayAnimation ();
	}
}
