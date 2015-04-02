using UnityEngine;
using System.Collections;

public class SoundEffectsHelper : MonoBehaviour {
	public static SoundEffectsHelper Instance;
	
	public AudioClip attackSound;
	public AudioClip deathSound;
	public AudioClip rightChoiceSound;
	public AudioClip wrongChoiceSound;
	public AudioClip gameOverSound;
	// Use this for initialization
	void Start () {
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of SoundEffectsHelper!");
		}
		Instance = this;
	}
	

	public void MakeAttackSound()
	{
		MakeSound(attackSound);
	}
	
	public void MakeDeathShotSound()
	{
		MakeSound(deathSound);
	}
	
	public void MakeRightChoiceSound()
	{
		MakeSound(rightChoiceSound);
	}

	public void MakeWrongChoiceSound()
	{
		MakeSound(wrongChoiceSound);
	}

	public void MakeGameOverSound()
	{
		MakeSound(gameOverSound);
	}

	private void MakeSound(AudioClip originalClip)
	{
		// As it is not 3D audio clip, position doesn't matter.
		AudioSource.PlayClipAtPoint(originalClip, transform.position);
	}
}
