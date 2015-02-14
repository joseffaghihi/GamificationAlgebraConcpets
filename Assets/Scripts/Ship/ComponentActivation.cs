using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComponentActivation : MonoBehaviour 
{
    private float climbSpeed;
    private float walkSpeed;
    public float force = 1000f;
    private bool isClimbing = false;
    private bool isWalking = true;
    private Vector3 forceDirection;
    private CreatureIdentity id;

    public AudioClip screechOne;
    public AudioClip screechTwo;
    public AudioClip screechThree;
    public AudioClip screechFour;
	public AudioClip screechFive;
	public AudioClip screechSix;
	public AudioClip celebrationOne;
	public AudioClip celebrationTwo;

    private Animator minionAnim;
    private CapsuleCollider capsuleCollider;
	
	void Awake() 
	{
        minionAnim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
	}

	void Start()
	{
        forceDirection = new Vector3(1f, 1f, 0);
        climbSpeed = Random.Range(9.89f, 13.12f);
        walkSpeed = Random.Range(3.98f, 6.5f);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Ladder")
		{
            //id = gameObject.GetComponent<CreatureIdentity>();
            //Debug.Log("Hit Ladder: " + id.Number.ToString());
            // ----
            isClimbing = true;
            isWalking = false;
			minionAnim.SetTrigger ("Climb");
		}	
        else if (other.tag == "ForwardEnabler")
		{
            //id = gameObject.GetComponent<CreatureIdentity>();
            //Debug.Log("Hit Forward Enabler: " + id.Number.ToString());
            // ----
            isClimbing = false;
            isWalking = true;
			minionAnim.SetTrigger ("Walk");
		} 
        else if (other.tag == "exit")
		{
            //id = gameObject.GetComponent<CreatureIdentity>();
            //Debug.Log("Hit Exit: " + id.Number.ToString());
            // ----
			// exit code
		}
        else if (other.tag == "Minion")
        {
            // Temp Debug Messages [NG]
            Debug.Log("Hit with minion detected");
        }

        // Temp Debug Messages [NG]
        Debug.Log("Minion has hit tag: " + other.tag);
	}

    void Walk()
    {
        transform.Translate(new Vector3(0, 0, 1) * walkSpeed * Time.deltaTime);
    }

    void Climb()
    {
        transform.Translate(new Vector3(0, 1, 0) * climbSpeed * Time.deltaTime);
    }
    
    void Update()
    {
        if(isWalking == true)
        {
            Walk();
        }
        else if (isClimbing == true)
        {
            Climb();
        }
    }

    void OnMouseDown()
    {
		// Created a method for all of these below______________________DAVID
		/*
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
        rigidbody.AddForce(forceDirection * force);
        Debug.Log("Clicked!");
        Destroy(gameObject, 1f);
        minionAnim.SetBool ("isFlicked", true);
		Destroy(capsuleCollider);
		*/
		Flick ();
		MinionSqueal();
    }

	public void Flick()
	{
		rigidbody.useGravity = true;
		rigidbody.isKinematic = false;
		isClimbing = false;
		rigidbody.AddForce(forceDirection * force);
		Debug.Log("Clicked!");
		Destroy(gameObject, 1f);
		minionAnim.SetBool ("isFlicked", true);
		Destroy(capsuleCollider);
	}

    public void MinionSqueal()
	{	
        int sound = Random.Range (1, 7);
		switch (sound)
		{
		case 1:
			audio.clip = screechOne;
			print("Audio 1 was played.");
			break;
		case 2:
			audio.clip = screechTwo;
			print("Audio 2 was played.");
			break;
		case 3:
			audio.clip = screechThree;
			print("Audio 3 was played.");
			break;
		case 5: 
			audio.clip = screechFive;
			print ("Audio five was played.");
			break;
		case 6:
			audio.clip = screechSix;
			print("Audio six was played.");
			break;
		default:
			audio.clip = screechFour;
			print("Audio 4 was played.");
			break;
		}
        audio.Play();
    }
}
