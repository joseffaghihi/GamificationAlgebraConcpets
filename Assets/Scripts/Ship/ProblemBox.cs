using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProblemBox : MonoBehaviour {

    int A;
    int B;
    int C;
    Text problemBox;

    public int minValue;
    public int maxValue;

    public int a
    {
        get { return A; }
    }

    public int b
    {
        get { return B; }
    }

    public int c
    {
        get { return C; }
    }

	// Use this for initialization
    void Awake()
    {
        problemBox = GetComponent<Text>();
    }

	void Start () {
        Generate();
	}

    public void Generate()
    {
        A = GetRandomNumber();
        B = GetRandomNumber();
        C = GetRandomNumber();

        problemBox.text = A.ToString() + "x+" + B.ToString() + "x+" + C.ToString();
    }

    public int GetRandomNumber()
    {
        return Random.Range(minValue, maxValue);
    }
}
