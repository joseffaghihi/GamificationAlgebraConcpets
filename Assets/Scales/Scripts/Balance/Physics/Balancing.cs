using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Balancing : MonoBehaviour
{
    //***Declaration***
	//=================

	//*Physics*
	//---------
	//Checks if physics is enabled/disabled
	private bool physics;

	//Store the total number of tries and the number already tried by the User
	private int totalTries, tried;

	//Store the rotational speed and it's maximum and minimum limits
	private float rotationSpeed;
	private float maxSpeed;
	private float minSpeed;
	
	//*Particles*
	//-----------
	//Set the particles for the balance and check if they have already been instantiated
	public GameObject equalParticles, equalParticlesTarget;
	private bool particlesInstantiated;

	//*Equation*
	//----------
	private Text equationGameObject; //Stores the location of the equation
	private Equation equation;//Stores a reference to the equation class
	public TextMesh userInput; //Gets the User Input from selected TextMesh

	//*GUI Elements*
	//--------------
	//Buttons
	private GameObject showHint, showSolution, dropButton;
	
	//Weights
	private ChangeForce rightWeight, leftWeight;

	//*Animations*
	//------------
    //References to the "Little Guys" for animation
    public Animator[] littleGuys;

    //*Stats*
    //-------
    Stats stats = new Stats();
    bool addedStat = false; //Check if Stat has been added

    //*Audio*
    //-------
    AudioManager audioManager;

    //***Definition***
	//================
	void Start () 
    {
        //*Audio*
        //-------
        audioManager = new AudioManager("MyAudio/");

		//*Physics*
		//---------
		//Disable Physics components until the User has clicked the Button
		physics = false;

		//Set Default Values
		maxSpeed = 30;
		minSpeed = 5;
		totalTries = 4;
		tried = 0;

		//*Particles*
		//-----------
		particlesInstantiated = false;

		//*Equation*
		//----------
		//Add the location where the equation is to be displayed
		equationGameObject = GameObject.Find("Equation").GetComponent<Text>();

		//Set the type of Equation
		equation = new Equation ("linear");
		rotationSpeed = Mathf.Abs(equation.GetSolution() - equation.GetNumA());
		equationGameObject = equation.Output(equationGameObject);

		//*GUI Elements*
		//--------------
		//Weights
		rightWeight = gameObject.AddComponent<ChangeForce>();
		leftWeight = gameObject.AddComponent<ChangeForce>();
		rightWeight.setPosition(GameObject.Find("RightWeight"));
		leftWeight.setPosition(GameObject.Find("LeftWeight"));

        //Buttons
        showHint = GameObject.Find("Hint");
        showSolution = GameObject.Find("Solution");
        dropButton = GameObject.Find("DropWeights");
        showHint.SetActive(false);
        showSolution.SetActive(false);
        dropButton.SetActive(false);
	}

    //***Game Loop***
	//===============
	void FixedUpdate() //Frame Independent Loop 
    {
        if (physics)
        {
            //Tilt left
            if (int.Parse(userInput.text) > equation.GetSolution())
            {
                //*Physics*
				//---------
                GetComponent<Rigidbody>().AddRelativeTorque(rotationSpeed, 0, 0);

                //*Animations*
				//------------
                playAnimation(2, "Cry");

                //*GUI Elements*
				//--------------
                GiveHelp();

                if (!addedStat)
                {
                    //*Audio*
                    //-------
                    GetComponent<AudioSource>().PlayOneShot(audioManager.GetClip("Shriek"));

                    //*Stats*
                    //-------
                    stats.AddLoss();
                    addedStat = true;
                }

                //*Reset*
				//-------
                Invoke("ResetWeight", 4);
            } 

            //Tilt right
			else if (int.Parse(userInput.text) < equation.GetSolution())
            {
                //*Physics*
				//---------
                GetComponent<Rigidbody>().AddRelativeTorque(-rotationSpeed, 0, 0);
                
                //*Animations*
				//------------
                playAnimation(1, "Cry");

                //*GUI Elements*
				//--------------
                GiveHelp();

                if (!addedStat)
                {
                    //*Audio*
                    //-------
                    GetComponent<AudioSource>().PlayOneShot(audioManager.GetClip("Shriek"));

                    //*Stats*
                    //-------
                    stats.AddLoss();
                    addedStat = true;
                }

                //*Reset*
				//-------
                Invoke("ResetWeight", 4);
            }

            //Balance
			else if (int.Parse(userInput.text) == equation.GetSolution())
            {
				//*Physics*
				//---------
                //Update physics for the balance (rotation - balanced state)
                GetComponent<Rigidbody>().AddRelativeTorque(0, 0, 0);

				//*Animations*
				//------------
                //Play Happy animation for both guys
                playAnimation(1, "Happy");
                playAnimation(2, "Happy");

				//*Particles & Sound*
				//-------------------
                //Instantiate victory particles and play victory sound clip
                if (!particlesInstantiated)
                {
                    GetComponent<AudioSource>().PlayOneShot(audioManager.GetClip("ProblemSolved")); //Victory Sound
                    particlesInstantiated = true; //Prevent multiple particles
                    Instantiate(equalParticles, equalParticlesTarget.transform.position, Quaternion.identity);
                }

                //*Stats*
                //-------
                if (!addedStat)
                {
                    stats.AddWin();
                    addedStat = true;
                }

				//*Reset*
				//-------
                Invoke("ResetLevel", 5); //Reset the Level after 5 seconds to display a new problem
            }

			//*Physics*
			//---------
            //Compute new rotation speed based on the precentage the value is off by the actual solution
            rotationSpeed = Mathf.Abs(equation.GetSolution() - int.Parse(userInput.text));

            //Set min/max values for rotation speed
            rotationSpeed = (rotationSpeed > maxSpeed) ? maxSpeed : rotationSpeed;
            rotationSpeed = (rotationSpeed < minSpeed) ? minSpeed : rotationSpeed;
        }
        
		else if (!physics)
		{
	        //Track how many guesses the user has made
	        GameObject[] objects = GameObject.FindGameObjectsWithTag("Number");

	        if (objects.Length != totalTries - tried)
	            dropButton.SetActive(true);
	        else
	            dropButton.SetActive(false);
		}
	}
    
	//***Public Functions***
	//======================
	//Call the physics initiation function after 0.8 seconds upon user button click
	public void ReleaseWeights()
	{
		Invoke("InvokeInitiatePhysics", 0.8F);
	}
	
	//Function to complete the given equation if the user does not know the answer
	public void SolveEquation()
	{
		equationGameObject.text = "x = " + equation.GetSolution();
	}
	
	//Resets the current Level
	public void ResetLevel()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	//***Private Functions***
	//=======================
    //Play Animation based on parameter input
    void playAnimation(int num, string animation)
    {
        //Subtract one from num for proper array access
        num--;

        //Start the animation for the little guy
        littleGuys[num].GetComponent<Animation>().Play(animation);
    }

    //Reset the Weights' Position, Reset animation, and Turn physics off
    void ResetWeight()
    {
		//*Physics*
		//---------
        physics = false;

		//*Weights*
		//---------
        rightWeight.Reset(GameObject.Find("RightWeight"));
        leftWeight.Reset(GameObject.Find("LeftWeight"));

		//*Animations*
		//------------
        playAnimation(1, "Idle");
        playAnimation(2, "Idle");

        //*Stats*
        addedStat = false;
    }

    //Actual Physics Function with parameters
    void InitiatePhysics(ref int num)
    {
        physics = true;
        num++;
    }

    //Invoke physics for the balance
    void InvokeInitiatePhysics()
    {
        InitiatePhysics(ref tried);
    }

    //Makes options available according to the number of wrong answers
    void GiveHelp()
    {
       //Get the number of GameObjects with the tag Numbers to track how many guesses the user has made
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Number");

        if(objects.Length <= 3)
        {
            showHint.SetActive(true);
        }
        if (objects.Length <= 2)
        {
            showSolution.SetActive(true);
        }
    }
}
