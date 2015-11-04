using UnityEngine;
using System.Collections;

namespace PrimeNumbers
{
	public class DeleteCube : MonoBehaviour 
	{
		public TextMesh CubeText;
		
		IEnumerator MyMethod()
		{
			yield return new WaitForSeconds(0); 
			Destroy (gameObject);
		}
		
		
		public void Access_MyMethod()
		{
			StartCoroutine(MyMethod());
		}
		
	}
}//end of namespace