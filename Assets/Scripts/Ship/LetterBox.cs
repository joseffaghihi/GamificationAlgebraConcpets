using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LetterBox : MonoBehaviour {
    Text letterBox;

    void Awake()
    {
        letterBox = GetComponent<Text>();
    }

	// Use this for initialization
	void Start () {
        Generate();
	}

    public void Generate()
    {
        int r = Random.Range(1, 4);
        switch(r)
        {
            case 1:
                letterBox.text = "A";
                break;
            case 2:
                letterBox.text = "B";
                break;
            case 3:
                letterBox.text = "C";
                break;
            default:
                letterBox.text = "ZZ";
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
