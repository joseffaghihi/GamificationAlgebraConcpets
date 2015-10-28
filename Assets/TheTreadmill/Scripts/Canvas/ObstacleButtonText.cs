using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObstacleButtonText : MonoBehaviour 
{
	//Change Text of the Button when clicked
	public void ChangeText()
	{
		if(GetComponent<Text>().text == "Obstacles: ON")
		{
			GetComponent<Text>().text = "Obstacles: OFF";
		}
		else
		{
			GetComponent<Text>().text = "Obstacles: ON";
		}
	}
}
