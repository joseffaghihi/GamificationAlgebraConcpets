using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
//TYPES OF EQUATIONS
// 1 - linear
// 2 - quadratic (to be implemented)
 */

public class Equation
{
	//***Declaration***
	//=================

	private int NumA; //Stores all randomly generated numbers for the equations
	private int solution; //Stores the solution to the equation
	private string type; //Stores the type of equation (ex. linear or quadratic)

	//***Constructor***
	//=================

	public Equation(string type)
	{
		this.type = type; //Set the type of equation

		//Initialize Equation based on the type
		InitializeEquation ();
	}

	//***Public Functions***
	//======================

	//Output the Equation
	public Text Output(Text equation)
	{
		//Output based on the type of equation
		switch (type) 
		{
		case "linear":
			if (NumA > 0)
				equation.text = "x + " + NumA + " = 0";
			else if (NumA < 0)
				equation.text = "x - " + Mathf.Abs(NumA) + " = 0";
			break;
			
		case "quadratic":
			SetQuadratic();
			break;
			
		default:
			Debug.Log("Error: Type of Equation does not exist. (Outputting the Equation)");
			break;
		}

		return equation;
	}

	//Returns the solution
	public int GetSolution()
	{
		return solution;
	}

	//Returns the First Random Number
	public int GetNumA()
	{
		return NumA;
	}

	//***Private Functions***
	//=======================

	//Compute the Solution based on the type of equation
	private int ComputeSolution()
	{
		return solution;
	}

	//Set all the neccessary information for a linear equation
	private void SetLinear()
	{
		NumA = Random.Range(-9, 9); //Generate random number
		if (NumA == 0) 
		{
			NumA = Random.Range (1, 9);
		}

		solution = -NumA; //Set solution
	}

	//Set all the neccessary information for a quadratic equation
	private void SetQuadratic()
	{
	}

	//Get the necessary information for the type of equation
	private void InitializeEquation()
	{
		switch (type) 
		{
		case "linear":
			SetLinear();
			break;

		case "quadratic":
			SetQuadratic();
			break;

		default:
			Debug.Log("Error: Type of Equation does not exist (Initializing the Equation)");
			break;
		}
	}
}
