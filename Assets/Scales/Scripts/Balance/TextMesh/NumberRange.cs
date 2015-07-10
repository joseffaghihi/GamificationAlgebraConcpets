using UnityEngine;
using System.Collections;

public class NumberRange : MonoBehaviour 
{
	private int num = 0;
	private const int maxLimit = 9;
	private const int minLimit = -9;

	//***Game Loop***
	//===============
	void Update () 
    {
		//Right - Move scale to the right by one
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
			UpdateText('+');
        }

		//Left - Move scale to the left by one
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
			UpdateText('-');
        }
	}
	
	//***Private Functions***
	//=======================
	private void UpdateText(char type)
	{
		switch (type) 
		{
			case '+':
			if(num < maxLimit)
			{
				foreach(TextMesh child in GetComponentsInChildren<TextMesh>())
				{
					child.text = (int.Parse(child.text) + 1).ToString();

					if(int.Parse(child.text) > num) 
						num = int.Parse(child.text);
				}
			}
			break;

			case '-':
			if(num > minLimit)
			{
				foreach(TextMesh child in GetComponentsInChildren<TextMesh>())
				{
					child.text = (int.Parse(child.text) - 1).ToString();

					if(int.Parse(child.text) < num) 
						num = int.Parse(child.text);
				}
			}
			break;

			default:
			Debug.Log("Error: Type of UpdateText does not exist. (Illegal operation for given function)");
			break;
		}
	}
}