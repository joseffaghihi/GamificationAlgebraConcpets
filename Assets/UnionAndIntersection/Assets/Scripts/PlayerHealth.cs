using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int startingHealth = 100;                            
	public int currentHealth;                                   
	public Slider healthSlider;                                 


	PlayerController playerController;

	bool isDead;                                                
	//bool damaged;
	bool play_wrong_choice_sound_over;

	void Awake()
	{
		currentHealth = startingHealth;
		playerController = GetComponent<PlayerController> ();

	}

	void Updata()
	{
		//damaged = false;
	}

	public void TakeDamage(int amount)
	{
		play_wrong_choice_sound_over = false;
		//damaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;
		if(play_wrong_choice_sound_over == false){
			SoundEffectsHelper.Instance.MakeWrongChoiceSound ();
			play_wrong_choice_sound_over = true;
		}
		if(currentHealth <= 0&&!isDead)
		{
			Death();
		}
	}

	void Death()
	{
		isDead = true;
		playerController.enabled = false;
	}
}
