using UnityEngine;
using System.Collections;

public abstract class BasicController : MonoBehaviour {

	public float jump_strenght = 0;
	public float horizontal_strenght = 0;
	protected bool timerJump = false;
	protected GameObject jump;
	protected bool WallJump = false;
	// Use this for initialization
	void Awake () {
		jump = this.transform.GetChild (0).gameObject;
	}
	
	// Update is called once per frame
	protected void FixedUpdate () {

		if (jump.GetComponent<Jump>().getCanJump() && Input.GetAxis ("Jump") != 0 && !timerJump) {
			Camera.main.transform.parent.GetComponent<CameraController>().PlayShakeAnim();
		    StartCoroutine("timer_jump");
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jump_strenght));
		}
		if (jump.GetComponent<Jump> ().getCanJump ()) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);
		}
		if (Input.GetAxis ("Horizontal") != 0 && !jump.GetComponent<Jump> ().getCanJump () && !WallJump) {

			this.transform.Translate(new Vector2(horizontal_strenght * Time.deltaTime * Input.GetAxis ("Horizontal") * 0.5f,0));	
		}
		if (Input.GetAxis ("Horizontal") != 0 && jump.GetComponent<Jump> ().getCanJump ()) {
			this.transform.Translate(new Vector2(horizontal_strenght * Time.deltaTime * Input.GetAxis ("Horizontal"),0));
		}
		if (Input.GetAxis ("Horizontal") < 0) {
			this.transform.localScale = new Vector2(1, 1);
		} else if (Input.GetAxis ("Horizontal") > 0) {
			this.transform.localScale = new Vector2(-1, 1);
		}
	}

	IEnumerator timer_jump() {

		timerJump = true;
		yield return new WaitForSeconds (0.07f);
		timerJump = false;
	}
}
