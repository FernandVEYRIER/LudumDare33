using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	private GameObject character;
	// Use this for initialization
	void Start () {

		character = this.transform.parent.gameObject;
	}
	
	void OnTriggerStay2D(Collider2D col) {

		if (col.CompareTag("ground")) {
			character.GetComponent<BasicController> ().setCanJump (true);
		}
	}

	void OnTriggerExit2D(Collider2D col) {

		if (col.tag == "ground") {
			character.GetComponent<BasicController> ().setCanJump (false);
		}
	}
}
