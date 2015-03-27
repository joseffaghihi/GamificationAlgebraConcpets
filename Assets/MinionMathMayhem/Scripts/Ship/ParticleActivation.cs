using UnityEngine;
using System.Collections;

public class ParticleActivation : MonoBehaviour {
	ParticleSystem particles;


	// Use this for initialization
	void Start () {
		particles = gameObject.GetComponentInChildren<ParticleSystem>();
		if(particles == null)
		{
			Debug.Log ("No Particle System Found on minion game object");
		} else
		{
			Debug.Log ("Found!");
		}
	}

	public void Emit()
	{
		particles.Play ();
	}
}