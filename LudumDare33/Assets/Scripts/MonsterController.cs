using UnityEngine;
using System.Collections;

public class MonsterController : BasicController {

	private GameObject wallJump;
	private GameObject head;
	private bool timerJumpWall = false;
	// Use this for initialization
	void Start () {
		wallJump = this.transform.GetChild (1).gameObject;
		head = this.transform.GetChild (2).gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		base.FixedUpdate ();
		if (Input.GetAxis ("Jump") != 0 && !timerJumpWall && !timerJump && wallJump.GetComponent<WallJump>().getWallJump()) {
			StartCoroutine("timer_jump_wall");
			this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(200 * this.transform.localScale.x, 500));
			WallJump = true;
		}
		if (jump.GetComponent<Jump>().getCanJump()) {
			WallJump = false;
		}
	}

	IEnumerator timer_jump_wall() {
		timerJumpWall = true;
		yield return new WaitForSeconds (0.07f);
		timerJumpWall = false;
	}
}
