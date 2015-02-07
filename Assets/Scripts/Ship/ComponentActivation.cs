using UnityEngine;
using System.Collections;

public class ComponentActivation : MonoBehaviour 
{
    public float climbSpeed = 10.21f;
    public float walkSpeed = 4.28f;
    public float force = 1000f;
    private bool isClimbing = false;
    private bool isWalking = true;
    private Vector3 forceDirection;
    private CreatureIdentity id;

    public AudioClip screechOne;
    public AudioClip screechTwo;
    public AudioClip screechThree;
    public AudioClip screechFour;
    private Animator minionAnim;
    private CapsuleCollider capsuleCollider;
	
	void Awake() 
	{
        minionAnim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
	}

	void Start()
	{
        forceDirection = new Vector3(1f, 0.3f, 0);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Ladder")
		{
            isClimbing = true;
            isWalking = false;
		}	
        else if (other.tag == "ForwardEnabler")
		{
            id = gameObject.GetComponent<CreatureIdentity>();
            Debug.Log("Hit: " + id.Number.ToString());
            isClimbing = false;
            isWalking = true;
		} 
        else if (other.tag == "exit")
		{
			//exit code
		}
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
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
        rigidbody.AddForce(forceDirection * force);
        Debug.Log("Clicked!");
        Destroy(gameObject, 1f);
        minionAnim.SetBool ("IsFlicked", true);
		Destroy(capsuleCollider);
		MinionSqueal();
    }

    public void MinionSqueal()
	{	
        int sound = Random.Range (1, 4);
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
		default:
			audio.clip = screechFour;
			print("Audio 4 was played.");
			break;
		}
        audio.Play();
    }
}
