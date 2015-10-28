using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnionAlgorithm : MonoBehaviour {
	public List<int> set1;
	public List<int> set2;
	//UnionAlgorithm k0;
	// Use this for initialization
	void Start () {
		set1 = new List<int>();
		set2 = new List<int>();
		
		
		print ("before function");
		foreach(int value in set1)
			print (value);
		
		myUnion (ref set1,ref set2);
		
		print ("after function");
		foreach(int value in set1)
			print (value);
	}
	
	// Update is called once per frame
	void Update () {
		
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
}