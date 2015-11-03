using System.Collections;
using UnityEngine;
using System.Collections.Generic; //needed for lists

namespace PrimeNumbers
{
	public class StartNumber : MonoBehaviour 
	{
		public DeleteCube script_DeleteCube;

		public Transform prefabWires;//to spawn wire prefab
		public Transform LoseScreen;//to spawn losing screen
		public Transform CubePreFab;//Acess to cube prefab

		public GameObject MainCamera;//access to mainCamera

		public List<GameObject> Wires = new List<GameObject>();
		//Holds the 5 wires in a list to keep track of which ones have been deleted

		public GameObject[] CubeArray; //holds the cubes
		public TextMesh[] FArray; //Holds the textMeshes in the inspector in an array

		public static int RN; //our starting number
		
		public int[] FactorArray; //an array to hold all of the factors of starting number
		public int[,] Choices; //A 2d array to hold all the combinations that go into start number.

		public int[] ArrayB;//Array that holds the selected numbers to display. 

		public int counter; //keeps track of number of factors
		public int num1=0;//used to access 2d array

		public int num2=0;//used to access 2d array
		public int choiceCounter=0;//used to put the last number with the first number when making pairs

		public int RandomChoice1=0;//chooses a random pair to display
		public int RandomChoice2=0;//chooses a second random pair to display

		public int lastBox =0;//used to display a random number that isn't already displayed
		public int start;//used when making pairs of factors.

		public static int deletedCounter=0; //keeps track of objects deleted

		public int displayCounter=0;//counter to display the Facors on the cubes.

		public bool[] cubesRemaining;//true = cube not deleted false = cube deleted.
		public int spacing =0;//used to space the prime numbers

		public int[] remainingNumbers;

		public int dummyCounter=0;//running out of names for counters

		public bool flag0 = true;
		public bool flag1 = true;
		public bool flag2 = true;
		public bool flag3 = true;
		public bool flag4 = true;

		public Vector3 CameraPosition = new Vector3(0,0,0);

		void Start () 
		{
			//set array and starting variables to starting values ****************************************************
			CameraPosition = MainCamera.transform.position;
			ArrayB = new int[5];
			FactorArray = new int[30];
			Choices = new int[10,2];
			cubesRemaining = new bool[5];
			for (int i =0; i<5; i++) 
			{
				cubesRemaining [i] = true;
			}
			remainingNumbers = new int[2];

			RN = Random.Range(1,100);
			//********************************************************************************************************

			//RN = 4;

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
			getFacters (RN);

			//puts the choices of factors that go together in a 2d array holding the numbers.
			makeChoices ();

			//two random numbers to choose which 2 combinations will be selected. 
			getRandomChoices ();

			//put the chosen numbers to display in an array of 5
			setArrayB ();

			//Mixes up the numbres in the Array
			ShuffleArray(ArrayB);

			//displays the random choices in the two dimentional array.
			displayTextmeshes ();

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


				if(deletedCounter==3)
				{
					for(int i=0;i<5;i++)
					{
						if(cubesRemaining[i]==true)
						{
							remainingNumbers[dummyCounter] = int.Parse(FArray[i].text);
							dummyCounter++;
						}
					}

					if(checkProduct(remainingNumbers[0],remainingNumbers[1])==true)//if the two factors == the startnumber
					{
						MainCamera.transform.position = new Vector3(0,-3,-8);
						CameraPosition = MainCamera.transform.position;

						//check to see if the remaining numbers are prime
						if(isPrime(remainingNumbers[0])==true)
						{
							CameraPosition = MainCamera.transform.position;
							CameraPosition.x = CameraPosition.x -13;
							CameraPosition.y = CameraPosition.y +5-spacing;;
							CameraPosition.z = CameraPosition.z +11;
							Instantiate(CubePreFab,CameraPosition,CubePreFab.transform.rotation);
							spacing=spacing + 2;
						}

						else if(isPrime(remainingNumbers[0])==false)
						{
							getFacters(remainingNumbers[0]);
							makeChoices (); 
							getRandomChoices ();
							setArrayB ();
							ShuffleArray(ArrayB);
							//displayTextmeshes ();
						}

						//check to see if the other remaining number is prime
						if(isPrime(remainingNumbers[1])==true)
						{
							CameraPosition = MainCamera.transform.position;
							CameraPosition.x = CameraPosition.x -13;
							CameraPosition.y = CameraPosition.y +5-spacing;
							CameraPosition.z = CameraPosition.z +11;
							Instantiate(CubePreFab,CameraPosition,CubePreFab.transform.rotation);
							spacing=spacing + 2;
						}

						else if(isPrime(remainingNumbers[1])==false)
						{
							/*getFacters(remainingNumbers[0]);
							makeChoices (); 
							getRandomChoices ();
							setArrayB ();
							ShuffleArray(ArrayB);*/
						}

					}

					else
					{
						SpawnLoseScreen();
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

		bool checkProduct(int factor1, int factor2)
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
			temp = CameraPosition.z;
			temp = temp +5;
			CameraPosition.z = temp;
			Instantiate(LoseScreen, CameraPosition,LoseScreen.transform.rotation);
		}

		public void getFacters(int nr1)
		{
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
		}

		public void makeChoices()
		{
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

		public void setArrayB()
		{
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
		}

		public void displayTextmeshes()
		{
			foreach(TextMesh text in FArray)
			{
				Debug.Log (ArrayB[displayCounter]);
				text.text  = ArrayB[displayCounter].ToString();
				displayCounter++;
			}

		}


	}//end of StartNumber
}//end namespace