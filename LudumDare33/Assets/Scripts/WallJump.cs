using UnityEngine;
using System.Collections;

public class WallJump : MonoBehaviour {

	private bool wallJump = false;
	private GameObject collidedObj;

	// Use this for initialization
	void OnTriggerEnter2D (Collider2D col) {
		collidedObj = col.gameObject;

		if (col.tag == "wall") {
			wallJump = true;
		}
	}
	void OnTriggerExit2D (Collider2D col) {
		collidedObj = null;
		if (col.tag == "wall") {
			wallJump = false;
		}
	}
	public bool getWallJump() {
		return wallJump;
	}
	
	public GameObject CollidedObj
	{
		get { return collidedObj; }
	}
}
