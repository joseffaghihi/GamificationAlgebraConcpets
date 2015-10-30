using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Feedback : MonoBehaviour
{
    public Sprite Original, LessThan, GreaterThan, EqualTo;
    GameControl gc = new GameControl();
    EquationGenerator eq = new EquationGenerator();

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(gc.getDelayWave()) //If the user hit an obstacle ***(exclude barrels still needed)***
        {
            transform.GetChild(0).gameObject.SetActive(false); //Disable Text

            if(eq.getAnswer() == 2)
            GetComponent<Image>().sprite = EqualTo; //Change sprite
            
        }
	}
}
