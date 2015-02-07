using UnityEngine;
using System.Collections;

public class UpDriver : MonoBehaviour 
{
	private int sound;
	private Vector3 forceDirection;
	private Animator minionAnim;
	private UpDriver upDriver;
	private CapsuleCollider capsuleCollider;

	public float force;
	public float speed;
	public AudioClip screechOne;
	public AudioClip screechTwo;
	public AudioClip screechThree;
	public AudioClip screechFour;

	void Awake()
	{
		minionAnim = GetComponent<Animator>();
		upDriver = GetComponent<UpDriver>();
		capsuleCollider = GetComponent<CapsuleCollider>();
	}
	
	void Start()
	{	
		forceDirection = new Vector3(1f, 1f, 0);
		sound = Random.Range (1, 4);
		//min - inclusive & max - exclusive
	}

	void Update()
    {
        transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
	}

	void OnMouseDown()
	{
		upDriver.enabled = false;
		rigidbody.useGravity = true;
		rigidbody.isKinematic = false;
		rigidbody.AddForce(forceDirection * force);
		Destroy(gameObject, 1.5f);
		minionAnim.SetBool ("IsFlicked", true);
		Destroy(capsuleCollider);
		MinionSqueal();
	}

	public void MinionSqueal()
	{	
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

		audio.Play ();
	}
}
