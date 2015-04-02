using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BridgeController : MonoBehaviour {
	public GameObject southwall_1;
	public GameObject northwall_0;
	public GameObject Bridge_0;
	public GameObject congrates_text_object;
	public GameObject samurai;
	public GameObject building_effect;
	public GameObject building_effect_center;

	private GameObject building_effect_instance1;

	public double myTimer = 3.0f;

	public PlayerSet p;
	public KeySetManager key;

	public bool bridge_0_is_built;

	public Text congrates_text;


	bool play_right_choice_sound_over;
	// Use this for initialization
	void Start () {
		southwall_1.SetActive (true);
		northwall_0.SetActive (true);
		Bridge_0.SetActive (false);
		bridge_0_is_built = false;
		play_right_choice_sound_over = false;

	}
	
	// Update is called once per frame
	void Update () {
		//if(UnionAction.katana0isDestroyed == 1||p.playerSet1==key.set3){
		//check if the key has been updated, then build
		building_effect_center.transform.position = samurai.transform.position;
		if(check_key(p.playerSet1,key.set3)==true && key.get_hint == true){
			southwall_1.SetActive (false);
			northwall_0.SetActive (false);
			Bridge_0.SetActive (true);
			bridge_0_is_built = true;
			if(play_right_choice_sound_over == false){
				SoundEffectsHelper.Instance.MakeRightChoiceSound();
				//instantiate the congrates prefab
				building_effect_instance1 = (GameObject)Instantiate(building_effect,samurai.transform.position,samurai.transform.rotation);
				building_effect_instance1.transform.parent = building_effect_center.transform;
				play_right_choice_sound_over = true;
			}
			myTimer = myTimer - Time.deltaTime;
			if(myTimer>0){
				congrates_text.text = "You Build A Bridge By Using the Elemental Power!";

			}else{
				Destroy (congrates_text_object);
				Destroy (building_effect_instance1);
			}
		}
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
