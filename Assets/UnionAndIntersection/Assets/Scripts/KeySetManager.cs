using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class KeySetManager : MonoBehaviour {
	public Text ele_hint;

	public PlayerSet p;
	public Katana_0Set k0;

	public bool get_hint;
	public List<int> set3;
	public List<string> set4;
	public List<int> key_set_0;
	public List<int> key_set_1;
	public List<int> key_set_2;
	//public key
	// Use this for initialization
	void Start () {
		get_hint = false;
		//get_ele_hint_set (ref set3,ref set4);
		//ele_hint.text += set3 [0].ToString ();

		//initialize key_set_0;
		key_set_0.Add (1);
		key_set_0.Add (2);
		key_set_0.Add (3);
		key_set_0.Add (5);
		key_set_0.Add (6);

		//initialize key_set_1;
		key_set_1.Add (3);
		key_set_1.Add (6);

		//initialize key_set_2;
		key_set_2.Add (0);
		key_set_2.Add (4);
		key_set_2.Add (5);

	}

	/*ALGORITHM FOR RANDOM
	// Update is called once per frame
	void Update () {
		while(get_hint == false)
		{
			get_ele_hint_set (ref set3,ref set4);
			get_hint = true;
		}
	}


	public void get_ele_hint_set(ref List<int> set3, ref List<string> set4)
	{
		set3 = new List<int> ();
		set4 = new List<string> ();
		set3 = find_ele_hint_set (ref p.playerSet1,ref k0.katana_0Set1,ref set3);
		//foreach(int n in set3)
			//Debug.Log (n);
		for(int i = 0;i<set3.Count;i++)
		{
			int choice = set3[i];
			switch(choice)
			{
			case 0:
				set4.Add ("1 ");
				break;
			case 1:
				set4.Add ("2 ");
				break;
			case 2:
				set4.Add ("3 ");
				break;
			case 3:
				set4.Add ("4 ");
				break;
			case 4:
				set4.Add ("5 ");
				break;
			case 5:
				set4.Add ("6 ");
				break;
			case 6:
				set4.Add ("7 ");
				break;
				
			}
			
		}
		//foreach(string n in set4)
			//Debug.Log (n);
		for(int i = 0; i<set4.Count; i++)
		{
			ele_hint.text += set4 [i].ToString ();
		}
	}

	public List<int> find_ele_hint_set(ref List<int> set1, ref List<int> set2,ref List<int> set3)
	{
		foreach (int value1 in set1)
			set3.Add (value1);
		foreach (int value2 in set2)
			set3.Add (value2);
		set3.Sort ();
		
		for(int i = 0; i<set3.Count-1; i++)
		{
			if(set3[i] == set3[i+1])
				set3.RemoveAt(i);
		}
		return set3;
	}
	*/
}
