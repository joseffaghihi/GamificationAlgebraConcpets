using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{

	public class DeleteCube : MonoBehaviour {

		public TextMesh CubeText;

		void Start () 
		{
		
		}

		void Update () 
		{
				if (transform.position.y < -20) 
				{
					Destroy (gameObject);
				}
			}
	}
}//end of namespace