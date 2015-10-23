using System.Collections;
using UnityEngine;

public class StartNumber : MonoBehaviour 
{
	public Transform prefabWires;//to spawn wire prefab
	public GameObject MainCamera;//access to mainCamera

	public GameObject[] CubeArray; //holds the cubes
	public TextMesh[] FArray; //Holds the textMeshes in the inspector in an array

	public static int RN; //our starting number
	
	public int[] FactorArray; //an array to hold all of the factors of starting number
	public int[,] Choices; //A 2d array to hold all the combinations that go into start number.

	public int[] ArrayB;//Array that holds the selected numbers to display. 

	public int counter= 0; //keeps track of number of factors
	public int num1=0;//used to access 2d array

	public int num2=0;//used to access 2d array
	public int choiceCounter=0;//used to put the last number with the first number when making pairs

	public int RandomChoice1=0;//chooses a random pair to display
	public int RandomChoice2=0;//chooses a second random pair to display

	public int lastBox =0;//used to display a random number that isn't already displayed
	public int start = 0;//used when making pairs of factors.

	public int displayCounter=0;

	public int deletedCounter=0; //keeps track of objects deleted

	void Start () 
	{
		ArrayB = new int[5];
		FactorArray = new int[20];
		Choices = new int[6,2];

		RN = Random.Range(1,100);
		//RN = 96;
		//Check to see if the random number is prime.
		if (isPrime (RN) == true) 
		{
			do {
				RN = Random.Range (1, 100);
				} while(isPrime (RN)==true);
		}

		//Display the starting number on starting cube.
		TextMesh t = (TextMesh)gameObject.GetComponent(typeof(TextMesh));
		t.text = RN.ToString();

		//puts all of the factors in an array
		for(int i=1;i<=RN;i++)
		{
			if(RN%i==0)
			{
				FactorArray[counter] = i;
				counter++;
			}
		}

		//puts the choices of factors that go together in a 2d array holding the numbers.
		choiceCounter = counter-1;
		for (int i=1; i<=((counter)/2); i++) 
		{
			Choices[num1,num2] = FactorArray[start];
			num2++;
			Choices[num1,num2] = FactorArray[choiceCounter];
			choiceCounter--;
			num1++;
			num2=0;
			start++;
		}

		//two random numbers to choose which 2 combinations will be selected. 
		if (((counter + 1) / 2) % 2 == 0) 
		{
			RandomChoice1 = Random.Range (0, ((counter + 1) / 2));
			do {
				RandomChoice2 = Random.Range (0, ((counter + 1) / 2));
			} while(RandomChoice2==RandomChoice1);
		}
		else if(((counter + 1) / 2) % 2 != 0) 
		{
			RandomChoice1 = Random.Range (0, ((counter) / 2));
			do {
				RandomChoice2 = Random.Range (0, ((counter) / 2));
			} while(RandomChoice2==RandomChoice1);
		}

		//put the chosen numbers to display in an array of 5
		ArrayB [0] = Choices [RandomChoice1, 0];
		ArrayB [1] = Choices [RandomChoice1, 1];
		ArrayB [2] = Choices [RandomChoice2, 0];
		ArrayB [3] = Choices [RandomChoice2, 1];

		lastBox = Random.Range (1,RN);
		for(int i=0;i<counter+1;i++)
		{
			if (lastBox == FactorArray [i]) 
			{
				lastBox = Random.Range (1,RN);
				i=0;
			}
		}
		ArrayB [4] = lastBox;

		//Mixes up the numbres in the Array
		ShuffleArray(ArrayB);

		//displays the random choices in the two dimentional array.
		foreach(TextMesh text in FArray)
		{
			text.text  = ArrayB[displayCounter].ToString();
			displayCounter++;
		}


	}//end of Start

	void Update () 
	{
		float distance = -10;
		//This captures left clicking. if its on an object it will delete it.
		if (Input.GetMouseButtonDown(0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			// Casts the ray and get the first game object hit
			Physics.Raycast (ray, out hit);
			Destroy (GameObject.Find (hit.transform.name));
			deletedCounter++;

			if(deletedCounter==3)
			{

				Instantiate(prefabWires, CubeArray[2].transform.position + CubeArray[2].transform.position * distance,
				            transform.rotation);
				MainCamera.transform.position = new Vector3(0,-3,-8);
			}
		}
	}

	public void acess_updateArray(int factorNumber)
	{
		updateAraay (factorNumber);
	}

	//Sets the number in the array of the deleted cube to 0
	private void updateAraay(int number)
	{
		for(int i =0;i<5;i++)
		{
			if(number == ArrayB[i])
			{
				ArrayB[i] = 0;
			}
		}
	}

	public static void ShuffleArray<T>(T[] array) 
	{
		for (int i = array.Length - 1; i > 0; i--) 
		{
			int r = Random.Range(0, i + 1);
			T tmp = array[i];
			array[i] = array[r];
			array[r] = tmp;
		}
	}

	bool isPrime(int Nr1)
	{
		for(int i=2; i<Nr1;i++)
		{
			if(Nr1%i==0)
				return false;
		}
		return true;
	}
}