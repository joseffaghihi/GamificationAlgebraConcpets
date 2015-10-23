using UnityEngine;
using System.Collections;

public class CursorHand : MonoBehaviour {
	
	private Texture2D cursorTex;
	private int sizeX;
	private int sizeY;
	public Texture2D closeFlick;		// refernce to close hand sprite
	public Texture2D openFlick;			// reference to open hand sprite
	public int cursorSize;				// size of the cursor icon
	public float waitTime;				// time in seconds to wait for hand to close

	void Awake () {
		sizeX = cursorSize;
		sizeY = cursorSize;
	}
	
	void Start() { 
		Cursor.visible = false;
		cursorTex = closeFlick;
	}
	
	// Update is called once per frame
	void Update () {
		sizeX = cursorSize;
		sizeY = cursorSize;
	}
	
	/// <summary>
	/// Draws hand cursor on the screen
	/// </summary>
	void OnGUI() {
		GUI.DrawTexture(new Rect(Event.current.mousePosition.x - (cursorSize/2), Event.current.mousePosition.y - (cursorSize / 2), sizeX, sizeY), cursorTex);
	}
	
	void OnMouseDown() {
		print("Flick() was called");
		_Flick();
	}
	
	/// <summary>
	/// public access to call _Flick()
	/// </summary>
	public void Flick() { 
		StartCoroutine(_Flick());
	}
	
	/// <summary>
	/// changes cursor to open hand and changes it back after waiting
	/// for 'waitTime' in seconds
	/// </summary>
	private IEnumerator _Flick() {
		cursorTex = openFlick;
		yield return new WaitForSeconds(waitTime);
		cursorTex = closeFlick; 
		yield return null;
	}
}
