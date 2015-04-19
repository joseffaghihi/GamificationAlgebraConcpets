using UnityEngine;
using System.Collections;

public class MoviePlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Renderer r = GetComponent<Renderer>();
		MovieTexture movie = (MovieTexture)r.material.mainTexture;
		movie.Play ();
	}
}
