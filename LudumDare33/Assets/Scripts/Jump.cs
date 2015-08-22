using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	private bool canJump = false;
	private bool landed = false;

	void OnTriggerStay2D(Collider2D col) {

		if (col.tag == "ground") {
			canJump = true;
			landed = false;
		}
	}
	void OnTriggerExit2D(Collider2D col) {

		if (col.tag == "ground") {
			canJump = false;
		}
	}

	void OnTriggerEnter2D( Collider2D col )
	{
		if ( col.tag == "ground" )
		{
			landed = true;
		}
	}

	public bool getCanJump() {
		return canJump;
	}

	public bool Landed
	{
		get { return landed ;}
	}
}