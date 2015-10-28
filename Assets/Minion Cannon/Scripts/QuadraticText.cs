using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuadraticText : MonoBehaviour 
{
	public int min = -100;
	public int max = 100;

	Text textBox;
	QuadraticProblemGenerator qpg = new QuadraticProblemGenerator();
	// Use this for initialization
	void Start () {
		textBox = GetComponent<Text>();
		textBox.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		qpg.GenerateFactorableQuadratic();
		qpg.max = max;
		qpg.min = min;
		textBox.text = 	qpg.ToString();
	}
}
