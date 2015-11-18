using System.Collections;
using UnityEngine;
using System.Collections.Generic; //needed for lists

namespace PrimeNumbers
{
	public class StartNumber : MonoBehaviour 
	{
		//-****************************************************************************************-//
		public Transform LoseScreen;//to spawn losing screen
		public Transform CubePreFab;//Acess to cube prefab
		public Transform FiveDynamitePreFab;//Acess to Dynamite PreFab
		public GameObject MainCamera;//access to mainCamera
		public GameObject[] DynamiteArray; //holds the 5 dynamites in an array to keep 
		//track of which ones have been deleted
		public TextMesh[] TextMeshArray; //Holds the textMeshes in the inspector in an array
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

		public static int RN; //our starting number

		void Start () 
		{			
			CameraPosition = MainCamera.transform.position;
			RN = Random.Range(1,100);

			//Check to see if the random number is prime.
			if (isPrime (RN) == true) 
			{
				do {
					RN = Random.Range (1, 100);
				} while(isPrime (RN)==true);
			}
			RN = 56;
			Debug.Log ("SN=" + RN);
			
			//Display the starting number on starting cube.
			TextMesh t = (TextMesh)gameObject.GetComponent(typeof(TextMesh));
			t.text = RN.ToString();

			getFacters (RN); //puts all of the factors in an array

			makeChoices ();//puts the choices of factors that go together in a 2d array holding the numbers.

			getRandomChoices ();//two random numbers to choose which 2 combinations will be selected. 

			setDisplayedNumbers ();//put the chosen numbers to display in an array of 5

			ShuffleArray(DisplayedNumbers);//Mixes up the numbres in the Array
			
		}//end of Start
		
		void Update () 
		{
			if(deletedCounter==3)
			{
				for(int i=0;i<5;i++)
				{
					if(DynamitesRemaining[i]==true)
					{
						Debug.Log(usersChoiceCounter);
						remainingNumbers[usersChoiceCounter] = DisplayedNumbers[i];
						cubePosition[usersChoiceCounter] = DynamiteArray[i].transform.position;
						//adujusting where next row of dynamites will be spawned.
						cubePosition[usersChoiceCounter].y -= 6f;
						cubePosition[usersChoiceCounter].z =7f;
						cubePosition[usersChoiceCounter].x+=.8f;
						usersChoiceCounter++;
					}
				}
				
				if(checkProduct(remainingNumbers[0],remainingNumbers[1])==true)//if the two factors == the startnumber
				{
					CameraPosition = MainCamera.transform.position;

					//check to see if the left number is prime and the right is not
					if(isPrime(remainingNumbers[0])==true)
					{
						FactorIsPrime=0;
						CameraPosition = MainCamera.transform.position;
						CameraPosition.x = CameraPosition.x -13;
						CameraPosition.y = CameraPosition.y +5-spacing;;
						CameraPosition.z = CameraPosition.z +11;
						Instantiate(CubePreFab,CameraPosition,CubePreFab.transform.rotation);
						spacing=spacing + 2;
					}
					else if(isPrime(remainingNumbers[0])==false)
					{
						RN=remainingNumbers[0];
						getFacters(remainingNumbers[0]);
						makeChoices(); 
						getRandomChoices();
						setDisplayedNumbers();
						ShuffleArray(DisplayedNumbers);
						Instantiate(FiveDynamitePreFab,cubePosition[0],FiveDynamitePreFab.transform.rotation);
					}

					//check to see if the left number is prime and the right is not
					if(isPrime(remainingNumbers[1])==true)
					{
						FactorIsPrime=1;
						CameraPosition = MainCamera.transform.position;
						CameraPosition.x = CameraPosition.x -13;
						CameraPosition.y = CameraPosition.y +5-spacing;
						CameraPosition.z = CameraPosition.z +11;
						Instantiate(CubePreFab,CameraPosition,CubePreFab.transform.rotation);
						spacing=spacing + 2;
					}
					else if(isPrime(remainingNumbers[1])==false)
					{
						RN=remainingNumbers[1];
						getFacters(remainingNumbers[1]);
						makeChoices (); 
						getRandomChoices ();
						setDisplayedNumbers ();
						ShuffleArray(DisplayedNumbers);
						Instantiate(FiveDynamitePreFab,cubePosition[1],FiveDynamitePreFab.transform.rotation);
					}
				}	

				
				else
					SpawnLoseScreen();
				
				deletedCounter=0;
				usersChoiceCounter=0;
			}//end of if counter==3
		}//end of update
		
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

		public bool isPrime(int Nr1)
		{
			for(int i=2; i<Nr1;i++)
			{
				if(Nr1%i==0)
					return false;
			}
			return true;
		}
		
		public bool checkProduct(int factor1, int factor2)
		{
			int total = 0;
			
			total = factor1 * factor2;
			
			if (total != RN) 
			{
				Debug.Log ("Total != RN");
				return false;
			} 
			else if (total == RN) 
			{
				Debug.Log ("Total == RN");
				return true;
			} 
			else
				return true;
		}
		
		public void SpawnLoseScreen()
		{
			float temp;
			CameraPosition = MainCamera.transform.position;
			temp = CameraPosition.z;
			temp = temp +5;
			CameraPosition.z = temp;
			Instantiate(LoseScreen, CameraPosition,LoseScreen.transform.rotation);
		}
		
		public void getFacters(int nr1)
		{
			FactorArray = new int[30];
			//puts all of the factors in an array
			counter = 0;
			for(int i=1;i<=nr1;i++)
			{
				if(nr1%i==0)
				{
					if(i*i==nr1)
					{
						FactorArray[counter] = i;
						Debug.Log (FactorArray[counter]);
						counter++;
						FactorArray[counter] = i;
						Debug.Log (FactorArray[counter]);
						counter++;;
					}
					else
					{
						FactorArray[counter] = i;
						Debug.Log (FactorArray[counter]);
						counter++;
					}
				}
			}
			DynamitesRemaining = new bool[5];
			remainingNumbers = new int[2];
			
			for (int i =0; i<5; i++) 
			{
				DynamitesRemaining [i] = true;
			}
		}
		
		public void makeChoices()
		{
			Choices = new int[10,2];
			num1 = 0;
			num2 = 0;
			start = 0;
			
			Debug.Log ("Choice:");
			choiceCounter = counter-1;
			for (int i=1; i<=((counter)/2); i++) 
			{
				Choices[num1,num2] = FactorArray[start];
				num2++;
				Choices[num1,num2] = FactorArray[choiceCounter];
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
			
			DisplayedNumbers[4] = Random.Range (1,RN);
			for(int i=0;i<counter+1;i++)
			{
				if (DisplayedNumbers[4] == FactorArray [i]) 
				{
					DisplayedNumbers[4] = Random.Range (7,RN);
					i=0;
				}
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
	}//end of StartNumber
}//end namespace