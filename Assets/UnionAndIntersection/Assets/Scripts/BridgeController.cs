using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class BridgeController : MonoBehaviour {
	//1st island
	public GameObject southwall_1;
	public GameObject northwall_0;
	public GameObject Bridge_0;

	//2nd island
	public GameObject southwall_2;
	public GameObject northwall_1;
	public GameObject Bridge_1;
	//3rd island
	public GameObject southwall_3;
	public GameObject northwall_2;
	public GameObject Bridge_2;


	public GameObject congrates_text_object;
	public GameObject samurai;
	public GameObject building_effect_0;
	public GameObject building_effect_1;
	public GameObject building_effect_2;
	public GameObject building_effect_center;

	private GameObject building_effect_instance0;
	private GameObject building_effect_instance1;
	private GameObject building_effect_instance2;

	public float myTimer = 3.0f;

	public PlayerSet p;
	public KeySetManager key;

	public bool bridge_0_is_built;
	public bool bridge_1_is_built;
	public bool bridge_2_is_built;

	public Text congrates_text;




	bool play_right_choice_sound_over_0;
	bool play_right_choice_sound_over_1;
	bool play_right_choice_sound_over_2;
	bool correct_effect_0;
	bool correct_effect_1;
	bool correct_effect_2;
	//bool effect_on_island_0_done;
	// Use this for initialization
	void Start () {
		//initialize 1st island
		southwall_1.SetActive (true);
		northwall_0.SetActive (true);
		Bridge_0.SetActive (false);
		bridge_0_is_built = false;

		//initialize 2nd island
		southwall_2.SetActive (true);
		northwall_1.SetActive (true);
		Bridge_1.SetActive (false);
		bridge_1_is_built = false;

		//initialize 3rd island
		southwall_1.SetActive (true);
		northwall_0.SetActive (true);
		Bridge_2.SetActive (false);
		bridge_2_is_built = false;


		//initialize effects
		play_right_choice_sound_over_0 = false;
		play_right_choice_sound_over_1 = false;
		play_right_choice_sound_over_2 = false;
		correct_effect_0 = false;
		correct_effect_1 = false;
		correct_effect_2 = false;
		//effect_on_island_0_done = false;
	}



	// Update is called once per frame
	void Update () {
		//if(UnionAction.katana0isDestroyed == 1||p.playerSet1==key.set3){
		//check if the key has been updated, then build
		building_effect_center.transform.position = samurai.transform.position;

		//NEW ALGORITHM FOR STATIC SETS
		//check result on 1st island
		if(check_key(p.playerSet1,key.key_set_0)==true && p.on_first_island == true){
			//build 1st bridge
			southwall_1.SetActive (false);
			northwall_0.SetActive (false);
			Bridge_0.SetActive (true);
			bridge_0_is_built = true;

			if(play_right_choice_sound_over_0 == false){
				SoundEffectsHelper.Instance.MakeRightChoiceSound();
				play_right_choice_sound_over_0 = true;
				//instantiate the congrates prefab
				
				
			}
			if(correct_effect_0 == false){
				correct_effect_0 = true;
				building_effect_instance0 = (GameObject)Instantiate(building_effect_0,samurai.transform.position,samurai.transform.rotation);
				building_effect_instance0.transform.parent = building_effect_center.transform;
				myTimer = myTimer - Time.deltaTime;
				congrates_text.text = "You Build A Bridge By Using the Elemental Power!";
				Destroy (congrates_text_object,myTimer);
				Destroy (building_effect_instance0,myTimer);
				

			}
			//effect_on_island_0_done = true;
		}

		//check result on 2nd island
		if(check_key(p.playerSet1,key.key_set_1)==true && p.on_second_island == true){
			//build 1st bridge
			southwall_2.SetActive (false);
			northwall_1.SetActive (false);
			Bridge_1.SetActive (true);
			bridge_1_is_built = true;
			
			if(play_right_choice_sound_over_1 == false){
				SoundEffectsHelper.Instance.MakeRightChoiceSound();
				play_right_choice_sound_over_1 = true;
				//instantiate the congrates prefab
				
				
			}
			if(correct_effect_1 == false){
				correct_effect_1 = true;
				building_effect_instance1 = (GameObject)Instantiate(building_effect_1,samurai.transform.position,samurai.transform.rotation);
				building_effect_instance1.transform.parent = building_effect_center.transform;
				myTimer = myTimer - Time.deltaTime;
				congrates_text.text = "You Build A Bridge By Using the Elemental Power!";
				Destroy (congrates_text_object,myTimer);
				Destroy (building_effect_instance1,myTimer);
				
				
			}
			//effect_on_island_0_done = true;
		}



		/* OLD ALGORITHM FOR RANDOM SETS
		if(check_key(p.playerSet1,key.set3)==true && key.get_hint == true && effect_on_island_0_done == false){
			southwall_1.SetActive (false);
			northwall_0.SetActive (false);
			Bridge_0.SetActive (true);
			bridge_0_is_built = true;
			correct_effect = true;
			if(play_right_choice_sound_over == false){
				SoundEffectsHelper.Instance.MakeRightChoiceSound();
				play_right_choice_sound_over = true;
				//instantiate the congrates prefab


			}
			if(correct_effect == true){
				building_effect_instance1 = (GameObject)Instantiate(building_effect,samurai.transform.position,samurai.transform.rotation);
				building_effect_instance1.transform.parent = building_effect_center.transform;
				myTimer = myTimer - Time.deltaTime;
				congrates_text.text = "You Build A Bridge By Using the Elemental Power!";
				Destroy (congrates_text_object,myTimer);
				Destroy (building_effect_instance1,myTimer);

				correct_effect = false;
			}
			effect_on_island_0_done = true;;
		}
		*/
	}

	bool check_key(List<int> list1, List<int> list2)
	{
		if (list1.Count == list2.Count)
		{
			for(int i = 0; i < list1.Count; i++)
			{
				if(list1[i] != list2[i])
				{
					return false;
				}
			}
			return true;
		}
		return false;
	}
}
