using System.Collections;
using UnityEngine;
using System.Collections.Generic; //needed for lists

namespace PrimeNumbers
{
	//Add an array that holds the numbers 1-5. When the cube that corrisodes to that number is 
	//deleted that is set to 0. if the number for the cubes != 0 then check to see if that number
	//is prime, and if the 2 remaining numbers multiplied = start number then spawn a prefab that corrisonds
	//with the correct number of remaining factors. 

	public class StartNumber : MonoBehaviour 
	{
		public DeleteCube script_DeleteCube;

		public Transform prefabWires;//to spawn wire prefab

		public GameObject MainCamera;//access to mainCamera

		public List<GameObject> Wires = new List<GameObject>();
		//Holds the 5 wires in a list to keep track of which ones have been deleted

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

		public static int deletedCounter=0; //keeps track of objects deleted

		public int displayCounter=0;

		public bool[] cubesRemaining;//true = cube not deleted false = cube deleted.

		public int total=0; // Check to see two remaining numbers = start number
		public int[] remainingNumbers;

		public int dummyCounter=0;//running out of names for counters

		public int setNumber =5;
		public bool flag0 = true;
		public bool flag1 = true;
		public bool flag2 = true;
		public bool flag3 = true;
		public bool flag4 = true;


		void Start () 
		{
			ArrayB = new int[5];
			FactorArray = new int[20];
			Choices = new int[6,2];
			cubesRemaining = new bool[5];
			for (int i =0; i<5; i++) 
			{
				cubesRemaining [i] = true;
			}
			remainingNumbers = new int[2];

			RN = Random.Range(1,100);
			//RN = 81;
			//Check to see if the random number is prime.
			if (isPrime (RN) == true) 
			{
				do {
					RN = Random.Range (1, 100);
					} while(isPrime (RN)==true);
			}
			Debug.Log ("SN=" + RN);

			//Display the starting number on starting cube.
			TextMesh t = (TextMesh)gameObject.GetComponent(typeof(TextMesh));
			t.text = RN.ToString();

			//puts all of the factors in an array
			for(int i=1;i<=RN;i++)
			{
				if(RN%i==0)
				{
					if(i*i==RN)
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

			//puts the choices of factors that go together in a 2d array holding the numbers.
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

			//two random numbers to choose which 2 combinations will be selected. 
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

			//put the chosen numbers to display in an array of 5
			ArrayB [0] = Choices [RandomChoice1, 0];
			ArrayB [1] = Choices [RandomChoice1, 1];
			ArrayB [2] = Choices [RandomChoice2, 0];
			ArrayB [3] = Choices [RandomChoice2, 1];

			ArrayB[4] = Random.Range (1,RN);
			for(int i=0;i<counter+1;i++)
			{
				if (ArrayB[4] == FactorArray [i]) 
				{
					ArrayB[4] = Random.Range (7,RN);
					i=0;
				}
			}

			//Mixes up the numbres in the Array
			ShuffleArray(ArrayB);

			//displays the random choices in the two dimentional array.
			foreach(TextMesh text in FArray)
			{
				Debug.Log (ArrayB[displayCounter]);
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

				if(flag0==true)
				{
					if(hit.collider.name == Wires[0].name)
					{
						Destroy(GameObject.Find(hit.transform.name));
						script_DeleteCube = CubeArray[0].GetComponent<DeleteCube>();
						script_DeleteCube.Access_MyMethod();
						deletedCounter++;
						cubesRemaining[0]=false;
						flag0=false;
					}
				}

				if(flag1==true)
				{
					if(hit.collider.name == Wires[1].name)
					{
						Destroy(GameObject.Find(hit.transform.name));
						script_DeleteCube = CubeArray[1].GetComponent<DeleteCube>();
						script_DeleteCube.Access_MyMethod();
						deletedCounter++;
						cubesRemaining[1]=false;
						flag1=false;
					}
				}

				if(flag2==true)
				{
					if(hit.collider.name == Wires[2].name)
					{
						Destroy(GameObject.Find(hit.transform.name));
						script_DeleteCube = CubeArray[2].GetComponent<DeleteCube>();
						script_DeleteCube.Access_MyMethod();
						deletedCounter++;
						cubesRemaining[2]=false;
						flag2=false;
					}
				}

				if(flag3==true)
				{
					if(hit.collider.name == Wires[3].name)
					{
						Destroy(GameObject.Find(hit.transform.name));
						script_DeleteCube = CubeArray[3].GetComponent<DeleteCube>();
						script_DeleteCube.Access_MyMethod();
						deletedCounter++;
						cubesRemaining[3]=false;
						flag3=false;
					}
				}

				if(flag4==true)
				{
					if(hit.collider.name == Wires[4].name)
					{
						Destroy(GameObject.Find(hit.transform.name));
						script_DeleteCube = CubeArray[4].GetComponent<DeleteCube>();
						script_DeleteCube.Access_MyMethod();
						deletedCounter++;
						cubesRemaining[4]=false;
						flag4=false;
					}
				}


				/*for(int i=0;i<setNumber;i++)
				{
					if(hit.collider.name == Wires[i].name)
					{
						Debug.Log (i);
						Destroy(GameObject.Find(hit.transform.name));
						//run corountine that deleted cube.
						script_DeleteCube = CubeArray[i].GetComponent<DeleteCube>();
						script_DeleteCube.Access_MyMethod();
						deletedCounter++;
						cubesRemaining[i]=false;
						Wires.RemoveAt(i);
						setNumber--;
					}
				}*/

				if(deletedCounter==3)
				{
					for(int i=0;i<5;i++)
					{
						if(cubesRemaining[i]==true)
						{
							remainingNumbers[dummyCounter] = ArrayB[i];
							dummyCounter++;
						}
					}

					total = remainingNumbers[0] * remainingNumbers[1];

					if(total != RN )
					{
						Debug.Log ("Total != RN");
					}
					else if(total==RN)
					{

						/*Instantiate(prefabWires, CubeArray[2].transform.position + CubeArray[2].transform.position * distance,
					         	   transform.rotation);*/
						MainCamera.transform.position = new Vector3(0,-3,-8);
						Debug.Log ("Total == RN");
					}
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
	}//end of StartNumber
}//end namespace