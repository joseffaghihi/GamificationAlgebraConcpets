using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class FuseBehavior : MonoBehaviour 
	{
		public float shrinkSpeed = 1f;
		public float size = 1f;
		bool shrinking = true;


		void Start () 
		{

		}

	    private void Awake()
		{

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
						
					}
			}
		}
	}//end of Fusebehavior
}//end of namespace
