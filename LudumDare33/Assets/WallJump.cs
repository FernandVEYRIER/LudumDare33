using UnityEngine;
using System.Collections;

public class WallJump : MonoBehaviour {

	private bool wallJump = false;
	// Use this for initialization
	void OnTriggerEnter2D (Collider2D col) {

		if (col.tag == "wall") {
			wallJump = true;
		}
	}
	void OnTriggerExit2D (Collider2D col) {
		
		if (col.tag == "wall") {
			wallJump = false;
		}
	}
	public bool getWallJump() {
		return wallJump;
	}
}
