﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic; //needed for lists

namespace PrimeNumbers
{
	public class TestScript : MonoBehaviour 
	{
	//-****************************************************************************************-//
	/*public Transform LoseScreen;//to spawn losing screen
	public Transform CubePreFab;//Acess to cube prefab
	public Transform FiveDynamitePreFab;//Acess to Dynamite PreFab
	public GameObject MainCamera;//access to mainCamera
	public GameObject[] DynamiteArray; //holds the 5 dynamites in an array to keep 
	//track of which ones have been deleted
	public TextMesh[] TextMeshArray; //Holds the textMeshes in the inspector in an array*/
	//-****************************************************************************************-//
	
	
	//-****************************************************************************************-//
	public int[] FactorArray; //an array to hold all of the factors of starting number
	public int[,] Choices; //A 2d array to hold all the combinations that go into start number
	public int[] remainingNumbers;//Holds the 2 numbers the user didnt delete
	public int[] DisplayedNumbers;//Array that holds the selected numbers to display
	public bool[] DynamitesRemaining;//Array of bools || true = dynamite not deleted || false = dynamite deleted
	//-****************************************************************************************-//
	
	
	//-****************************************************************************************-//
	public int num1=0;//used to access 2d array
	public int num2=0;//used to access 2d array
	public int RandomChoice1=0;//chooses a random pair to display
	public int RandomChoice2=0;//chooses a second random pair to display
	public int start;//used when making pairs of factors
	public int spacing =0;//used to space the prime numbers
	public int FactorIsPrime =0;//Tells prime number script which of the two remaining dynamites are prime
	//-****************************************************************************************-//
	
	
	//-****************************************************************************************-//
	public Vector3 CameraPosition = new Vector3(0,0,0);//Used to keep track of the position of camera
	public Vector3[] cubePosition = new Vector3[2];//Array of 2 to keep the position
	//of the 2 cubes the user doesn't delete
	//-****************************************************************************************-//
	
	
	//-****************************************************************************************-//
	public int deletedCounter=0; //keeps track of objects deleted
	public int displayCounter=0;//counter to display the Facors on the cubes
	public int choiceCounter=0;//used to put the last number with the first number when making pairs
	public int counter; //keeps track of the number of factors
	public int usersChoiceCounter=0;//goes from 0->1 and back to 0 to keep track of the 2
	//dynamites the user did not delete
	//-****************************************************************************************-//

		public int startNumber;
		public TestScriptText TestScriptText_script;
		public Transform FiveDynamitePreFab;//Acess to Dynamite PreFab
		public TextMesh[] TextMeshArray;
		public StartNumber StartNumber_script;
		public int[] ArrayOfFactors = new int[25];
		// TODO: created this array
		GameObject[] gs = new GameObject[5];
		// TODO: created this class refernce
		GameObject bombs;



		private void Awake()
		{
			TestScriptText_script = GameObject.Find("TestDynamite").GetComponentInChildren<TestScriptText>();
			StartNumber_script = GameObject.Find("Start Dynamite").GetComponentInChildren<StartNumber>();
		}


		void Start () 
		{
			Vector3 tempLocation = new Vector3(0,0,0);
			tempLocation = this.transform.position;
			tempLocation.y -= 5f;

			startNumber = TestScriptText.RN;

			ArrayOfFactors = getFacters (startNumber);
			for (int i = 0; i < ArrayOfFactors.Length; i++) 
			{
				Debug.Log (ArrayOfFactors[i]);
			}

			makeChoices (ArrayOfFactors);

			getRandomChoices ();

			setDisplayedNumbers ();

			ShuffleArray(DisplayedNumbers);
			// TODO: set the reference equal to the instantiation and cast the instantiation as a game object
			// TODO: since 'FiveDynamitePrefab' is a transform variable, i get the gameObject of the transform for the first argument
			// of the instantiation
			bombs = Instantiate(FiveDynamitePreFab.gameObject,tempLocation,FiveDynamitePreFab.transform.rotation) as GameObject;//cast instantiation as gameObject
			//TextMeshArray[0]= 
			//TODO: Check if bomb is null
			if(bombs == null) {
				print ("bombs is null");
			} else {
				print ("bombs is not null");
				print ("The number of children in bombs is:\t" + bombs.transform.childCount);
				for (int i = 0; i < bombs.transform.childCount; i++) {
					gs[i] = bombs.transform.GetChild (i).gameObject;
					print ("child " + i + " is: " + gs[i].name);
				}
				for(int i = 0; i < bombs.transform.childCount; i++) {
					// TODO: I think the null reference exception is here, all of the children need DynamiteSpawnTestTest on them
					// TODO: also, you spelled the class name TestTest but the file is TextTest for DynamiteSpawnTestTest.
					gs[i].GetComponent<DynamiteSpawnTestTest>().Nr1 = /*DisplayedNumbers[i]*/ 1;
				}
			}

			//displayTextmeshes ();
		}
//		
//		void Update () 
//		{
//		
//		}

		public int[] getFacters(int nr1)
		{
			int[] localArray = new int[30];
			//puts all of the factors in an array
			counter = 0;
			for(int i=1;i<=nr1;i++)
			{
				if(nr1%i==0)
				{
					if(i*i==nr1)
					{
						localArray[counter] = i;
						counter++;
						localArray[counter] = i;
						counter++;;
					}
					else
					{
						localArray[counter] = i;
						counter++;
					}
				}
			}
			
			DynamitesRemaining = new bool[5];
			for (int i =0; i<5; i++) 
			{
				DynamitesRemaining [i] = true;
			}
			
			return localArray;
		}

		public void makeChoices(int[] localArray)
		{
			Choices = new int[10,2];
			num1 = 0;
			num2 = 0;
			start = 0;
			
			Debug.Log ("Choice:");
			choiceCounter = counter-1;
			for (int i=1; i<=((counter)/2); i++) 
			{
				Choices[num1,num2] = localArray[start];
				num2++;
				Choices[num1,num2] = localArray[choiceCounter];
				Debug.Log (Choices[num1,num2-1] + " " + Choices[num1,num2]);
				choiceCounter--;
				num1++;
				num2=0;
				start++;
			}
		}

		public void getRandomChoices()
		{
			if (((counter + 1) / 2) % 2 == 0) //check to see if the number of choices is odd(here would be even)
			{
				RandomChoice1 = Random.Range (0, ((counter + 1) / 2));
				do {
					RandomChoice2 = Random.Range (0, ((counter + 1) / 2));
				} while(RandomChoice2==RandomChoice1);
			}
			
			else if(((counter + 1) / 2) % 2 != 0)  //Here it would be odd
			{
				RandomChoice1 = Random.Range (0, ((counter) / 2));
				do {
					RandomChoice2 = Random.Range (0, ((counter) / 2));
				} while(RandomChoice2==RandomChoice1);
			}
			Debug.Log ("RandomChoice1= " + RandomChoice1 + " RandomChoice2= " + RandomChoice2);
		}

		public void setDisplayedNumbers()
		{
			DisplayedNumbers = new int[5];
			DisplayedNumbers [0] = Choices [RandomChoice1, 0];
			DisplayedNumbers [1] = Choices [RandomChoice1, 1];
			DisplayedNumbers [2] = Choices [RandomChoice2, 0];
			DisplayedNumbers [3] = Choices [RandomChoice2, 1];
			
			DisplayedNumbers[4] = Random.Range (1,startNumber);
			for(int i=0;i<counter+1;i++)
			{
				if (DisplayedNumbers[4] == ArrayOfFactors [i]) 
				{
					DisplayedNumbers[4] = Random.Range (1,startNumber);
					i=0;
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

		public void displayTextmeshes()
		{
			foreach(TextMesh text in TextMeshArray)
			{
				Debug.Log (DisplayedNumbers[displayCounter]);
				text.text  = DisplayedNumbers[displayCounter].ToString();
				displayCounter++;
			}

		}

	}//end of main
}