using UnityEngine;
using System.Collections;

public class MonsterController : BasicController {

	private GameObject wallJump;
	private GameObject explosion;
	private bool timerJumpWall = false;
	private bool dash = false;
	private bool can_dash = false;
	// Use this for initialization

	protected override void Awake()
	{
		playerID = 2;
		base.Awake();
	}

	void Start () {
		wallJump = this.transform.GetChild (1).gameObject;
		explosion = this.transform.GetChild (3).gameObject;
	}
	
	// Update is called once per frame
	protected override void FixedUpdate () {
		base.FixedUpdate();
		if (Input.GetAxis (keyBinds["Jump"]) != 0 && !timerJumpWall && !timerJump && wallJump.GetComponent<WallJump>().getWallJump()) {
			StartCoroutine("timer_jump_wall");
			this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(200 * this.transform.localScale.x, 375));
			WallJump = true;
		}
		if (jump.GetComponent<Jump>().getCanJump()) {
			WallJump = false;
		}
		if (Input.GetKeyDown (KeyCode.Z) && !can_dash) {
			StartCoroutine("timer_dash");
			this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 550));
		}
		explosion.GetComponent<CircleCollider2D> ().enabled = dash;
		if (dash && this.gameObject.GetComponent<Rigidbody2D> ().velocity.y < 0) {
			dash = false;
		}
	}

	IEnumerator timer_jump_wall() {
		timerJumpWall = true;
		yield return new WaitForSeconds (0.07f);
		timerJumpWall = false;
	}

	IEnumerator timer_dash() {

		dash = true;
		can_dash = true;
		this.gameObject.layer = 10;
		yield return new WaitForSeconds (2f);
		this.gameObject.layer = 9;
		dash = false;
		can_dash = false;
	}
	public bool getDash() {
		return dash;
	}
	protected override void Attack()
	{
		base.Attack();
		if ( wallJump.GetComponent<WallJump>().CollidedObj == null )
			return;
		Debug.Log (wallJump.GetComponent<WallJump>().CollidedObj);
		if ( wallJump.GetComponent<WallJump>().CollidedObj.tag == "Player" )
		{
			GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().DisplayVictory( this, playerID );
		}
	}
}
