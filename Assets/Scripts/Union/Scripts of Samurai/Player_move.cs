using UnityEngine;
using System.Collections;

public class Player_move : MonoBehaviour {
	public float moveSpeed = 10f;
	public float jumpHeight;
	public bool isJumping = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A))
			transform.Translate (Vector3.left * moveSpeed * Time.deltaTime);
		if (Input.GetKey (KeyCode.D))
			transform.Translate (Vector3.right * moveSpeed * Time.deltaTime);

		if(Input.GetKeyDown(KeyCode.Space)){
			if(isJumping == false){
				rigidbody2D.AddForce(Vector3.up*jumpHeight);
				isJumping = true;
			}
		}


	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "platform") {
			isJumping = false;
		}
			
	}
}
