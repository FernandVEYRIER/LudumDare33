using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	private bool canJump = false;
	
	void OnTriggerStay2D(Collider2D col) {

		if (col.tag == "ground") {
			canJump = true;
		}
	}
	void OnTriggerExit2D(Collider2D col) {

		if (col.tag == "ground") {
			canJump = false;
		}
	}
	public bool getCanJump() {
		return canJump;
	}
}