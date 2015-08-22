using UnityEngine;
using System.Collections;

public class BasicController : MonoBehaviour {

	public float jump_strenght = 0;
	public float horizontal_strenght = 0;
	private bool canJump = false;
	private bool timerJump = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		print (canJump);
		print (Input.GetAxis ("Jump"));
		if (canJump && Input.GetAxis ("Jump") != 0 && !timerJump) {
		     StartCoroutine("timer_jump");
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jump_strenght));
		}
		if (Input.GetAxis ("Horizontal") != 0) {
			this.transform.Translate(new Vector2(horizontal_strenght * Time.deltaTime * Input.GetAxis ("Horizontal"),0));
		}
		if (Input.GetAxis ("Horizontal") < 0) {
			this.transform.localScale = new Vector2(1, 1);
		} else if (Input.GetAxis ("Horizontal") > 0) {
			this.transform.localScale = new Vector2(-1, 1);
		}
	}

	public void setCanJump(bool jump) {

		canJump = jump;
	}

	IEnumerator timer_jump() {

		timerJump = true;
		yield return new WaitForSeconds (0.07f);
		timerJump = false;
	}
}
