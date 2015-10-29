using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class FuseBehavior : MonoBehaviour 
	{
		//public GameObject cube;
		//public DestructionController script_DestructionController;//Nicholas had me create this variable to use 
		//for our event.

		public float shrinkSpeed = 1f;
		public float size = 1f;
		bool shrinking = true;


		void Start () 
		{

		}

	    private void Awake()//Nicholas told me to make this function and everything inside of it.
		{
			//script_DestructionController = cube.GetComponent<DestructionController>();
		}

		void Update () 
		{

			if (shrinking == true) 
			{
				transform.localScale -= Vector3.up*Time.deltaTime*shrinkSpeed;
				size = transform.localScale.y;
					if (size < .25)
					{
						shrinking = false;
						Destroy (gameObject);
						//script_DestructionController.Access_MyMethod();
					}
			}
		}
	}//end of Fusebehavior
}//end of namespace
