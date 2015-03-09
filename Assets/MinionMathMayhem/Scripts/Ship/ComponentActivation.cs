using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComponentActivation : MonoBehaviour 
{

        /*                      COMPONENT ACTIVATION
         * Designed for the minions, specifically for actor activity and attributes.
         * 
         * GOALS:
         *  Minion walks forward, depending on the given condition
         *  Minion climbs ladder, depending on the given condition
         *  Screech when selected
         *  Specialized activity when selected (or flicked)
         */



    // Declarations and Initializations
    // ---------------------------------
    // Speed that is used when the minions are climbing the ladder
        private float climbSpeed;
    // Speed that is used when the minions are walking (or running) forward
        private float walkSpeed;
    // Force 'thrust' that is used when the minions have been selected.
        public float force = 1000f;
    // Minion actions:
        private bool isClimbing = false;
        private bool isWalking = true;
    // Direction in which the Minions are thrusted
        private Vector3 forceDirection;
    // Script reference link
        private CreatureIdentity id;

    // Multimedia
    // --
        // Screeches
            public AudioClip screechOne;
            public AudioClip screechTwo;
            public AudioClip screechThree;
            public AudioClip screechFour;
	        public AudioClip screechFive;
	        public AudioClip screechSix;
        // Won
	        public AudioClip celebrationOne;
	        public AudioClip celebrationTwo;

    // Animations and physics
        private Animator minionAnim;
        private CapsuleCollider capsuleCollider;
    // ----
	


    // Initialization of specialized variables
	void Awake() 
	{
        // References
            minionAnim = GetComponent<Animator>();
            capsuleCollider = GetComponent<CapsuleCollider>();
	} // End of Awake



    // This function will be called once the actor has been summoned within the scene
	void Start()
	{
        // Force direction that will be used for eliminating the minion
            forceDirection = new Vector3(1f, 1f, 0);
        // Self-Randomize the speed
            climbSpeed = Random.Range(3.98f, 6.5f);
            walkSpeed = Random.Range(9.89f, 13.12f);
	} // End of Start
	


    // When the minions 'hit' with other objects, this function is going to be called.
	void OnTriggerEnter(Collider other)
	{
        // Debug Purposes:
        //id = gameObject.GetComponent<CreatureIdentity>();

        // Ladder collision:
		if(other.tag == "Ladder")
		{
            // Debug Purposes:
            //Debug.Log("Hit Ladder: " + id.Number.ToString());
            // ----
            isClimbing = true;
            isWalking = false;
			minionAnim.SetTrigger ("Climb");
		} // End if (ladder)

        // Forward Enabler (game object) collision:
        else if (other.tag == "ForwardEnabler")
		{

            // Debug Purposes:
            // ----
            isClimbing = false;
            isWalking = true;
			minionAnim.SetTrigger ("Walk");
        } // End if (ForwardEnabler)

        // Exit collision:
        else if (other.tag == "exit")
		{
            // Debug Purposes:
            //Debug.Log("Hit Exit: " + id.Number.ToString());
            // ----
			// exit code
        } // End if (exit)

        // Minion collision (colliding with another minion):
        else if (other.tag == "Minion")
        {
            // Temp Debug Messages [NG]
            Debug.Log("Hit with minion detected");
        } // End if (Minion)

        // Temp Debug Messages [NG]
        //Debug.Log("Minion has hit tag: " + other.tag);

	} // End of OnTriggerEnter



    // This function is called when the minions are going to walk.
    void Walk()
    {
        transform.Translate(new Vector3(0, 0, 1) * walkSpeed * Time.deltaTime);
    } // End of Walk



    // This functions is called when the minions are going to climb
    void Climb()
    {
        transform.Translate(new Vector3(0, 1, 0) * climbSpeed * Time.deltaTime);
    } // End of Climb
    


    // Update on every frame
    void Update()
    {
        // Check to see if the minion needs to walk or climb
        if(isWalking == true)
        {
            Walk();
        }
        else if (isClimbing == true)
        {
            Climb();
        }

    } // End of Update



    // When the creature has been 'selected', this function will be called
    void OnMouseDown()
    {
        // Selected action
		Flick ();
        // Play sound
		MinionSqueal();
    } // End of OnMouseDown



    // Actions to take place when the minion has been selected
	public void Flick()
	{
		GetComponent<Rigidbody>().useGravity = true;
		GetComponent<Rigidbody>().isKinematic = false;
		isClimbing = false;
		isWalking = false;
		GetComponent<Rigidbody>().AddForce(forceDirection * force);
		Debug.Log("Clicked!");
		Destroy(gameObject, 1f);
		minionAnim.SetBool ("isFlicked", true);
		Destroy(capsuleCollider);
	} // End of Flick



    // Sounds from the minion when selected
    public void MinionSqueal()
	{	
        int sound = Random.Range (1, 7);
		switch (sound)
		{
		case 1:
			GetComponent<AudioSource>().clip = screechOne;
			print("Audio 1 was played.");
			break;
		case 2:
			GetComponent<AudioSource>().clip = screechTwo;
			print("Audio 2 was played.");
			break;
		case 3:
			GetComponent<AudioSource>().clip = screechThree;
			print("Audio 3 was played.");
			break;
		case 5: 
			GetComponent<AudioSource>().clip = screechFive;
			print ("Audio five was played.");
			break;
		case 6:
			GetComponent<AudioSource>().clip = screechSix;
			print("Audio six was played.");
			break;
		default:
			GetComponent<AudioSource>().clip = screechFour;
			print("Audio 4 was played.");
			break;
		} // End switch
        
        GetComponent<AudioSource>().Play();

    } // End of MinionSqueal

} // End of Class
