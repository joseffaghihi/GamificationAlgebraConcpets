using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;

public class UnionAction : MonoBehaviour {
	public static int katana0isDestroyed;
	public static int katana1isDestroyed;
	public static int katana2isDestroyed;

	public BridgeController b;
	public PlayerSet p;
	public Katana_0Set k0;
	public Katana_1Set k1;
	public Katana_2Set k2;

	public GameObject playerCenter;
	public GameObject katanaCenter_0;
	public GameObject katanaCenter_1;
	public GameObject katanaCenter_2;

	GameObject player;
	PlayerHealth playerHealth;
	public int attackDamage = 50;
	bool touched1 = false;
	bool touched2 = false;

	//audio clips



	// Use this for initialization
	void Start () {

		Time.timeScale = 1;
		katana0isDestroyed = 0;
		katana1isDestroyed = 0;
		katana2isDestroyed = 0;

		player = GameObject.FindGameObjectWithTag ("samurai");
		playerHealth = player.GetComponent<PlayerHealth>();

	}

	void OnTriggerEnter (Collider other)
	{


		if(other.tag == "katana_0")
		{
			other.GetComponent<Renderer>().enabled = false;
			katana0isDestroyed = 1;
			Debug.Log(other.tag+" is destroyed");
		
			foreach (Transform child in playerCenter.transform) {
				GameObject.Destroy(child.gameObject);
			}

			if(k0.katana_0Set2==null)
				print ("k0.katana_0Set2 is null");
			else{

				//call myUnion function
				myUnion(ref p.playerSet1,ref k0.katana_0Set1);

				//clear playerset2
				p.playerSet2.Clear ();

				//call makeSet2 function
				makeSet2(ref p.playerSet1, ref p.playerSet2);
			}
		}
		if(other.tag == "katana_1")
		{
			if(touched1 == false && b.bridge_0_is_built==false)
			{
				
				touched2 = true;
			}
			other.GetComponent<Renderer>().enabled = false;
			katana1isDestroyed = 1;
			Debug.Log(other.tag+" is destroyed");
			
			foreach (Transform child in playerCenter.transform) {
				GameObject.Destroy(child.gameObject);
			}
			
			if(k1.katana_1Set2==null)
				print ("k1.katana_1Set2 is null");
			else{
				//call myUnion function
				myUnion(ref p.playerSet1,ref k1.katana_1Set1);
				
				//clear playerset2
				p.playerSet2.Clear ();
				
				//call makeSet2 function
				makeSet2(ref p.playerSet1, ref p.playerSet2);
			}

		}
		if(other.tag == "katana_2")
		{
			if(touched2 == false && b.bridge_0_is_built==false)
			{

				touched2 = true;
			}
			other.GetComponent<Renderer>().enabled = false;
			katana2isDestroyed = 1;
			Debug.Log(other.tag+" is destroyed");
			
			foreach (Transform child in playerCenter.transform) {
				GameObject.Destroy(child.gameObject);
			}
			
			if(k2.katana_2Set2==null)
				print ("k2.katana_2Set2 is null");
			else{
				//call myUnion function
				myUnion(ref p.playerSet1,ref k2.katana_2Set1);
				
				//clear playerset2
				p.playerSet2.Clear ();
				
				//call makeSet2 function
				makeSet2(ref p.playerSet1, ref p.playerSet2);
			}
		}


	}


	void myUnion(ref List<int> set1,ref List<int> set2 )
	{
		foreach (int value2 in set2)
			set1.Add (value2);
		set1.Sort ();
		
		for(int i = 0; i<set1.Count-1; i++)
		{
			if(set1[i] == set1[i+1])
				set1.RemoveAt(i);
		}
		
	}

	void makeSet2(ref List<int> set1, ref List<GameObject> set2)
	{
		GameObject e0 = GameObject.Find ("ball_01");
		GameObject e1 = GameObject.Find ("ball_02");
		GameObject e2 = GameObject.Find ("ball_03");
		GameObject e3 = GameObject.Find ("ball_04");
		GameObject e4 = GameObject.Find ("ball_05");
		GameObject e5 = GameObject.Find ("ball_06");
		GameObject e6 = GameObject.Find ("ball_07");
		
		for(int i = 0;i<set1.Count;i++)
		{
			int choice = set1[i];
			switch(choice)
			{
			case 0:
				set2.Add (e0);
				break;
			case 1:
				set2.Add (e1);
				break;
			case 2:
				set2.Add (e2);
				break;
			case 3:
				set2.Add (e3);
				break;
			case 4:
				set2.Add (e4);
				break;
			case 5:
				set2.Add (e5);
				break;
			case 6:
				set2.Add (e6);
				break;
				
			}
			
		}

		for (int i = 0; i < set2.Count; i++) {
			//instantiate elements in set2 at some random position
			
			set2[i] = (GameObject) Instantiate (set2[i],new Vector3 (i,i,0),Quaternion.Euler(new Vector3(45, 0, 0)));
			
			float angle = i * Mathf.PI * 2 / set2.Count;
			Vector3 pos = new Vector3(Mathf.Cos(angle), 0.5f, Mathf.Sin(angle)) * 1f + playerCenter.transform.position ;
			set2[i].transform.position = pos;
			set2[i].transform.parent = playerCenter.transform;
			
		}
	}

	void Update()
	{
		Debug.Log ("The bridge is build:" + b.bridge_0_is_built);
		if(touched1 == true && b.bridge_0_is_built==false)
		{
			playerHealth.TakeDamage(attackDamage);
			touched1 = false;
		}
		if(touched2 == true && b.bridge_0_is_built==false)
		{
			playerHealth.TakeDamage(attackDamage);
			touched2 = false;
		}
	}
}
