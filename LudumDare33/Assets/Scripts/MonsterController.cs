using UnityEngine;
using System.Collections;

public class MonsterController : BasicController {

	private GameObject wallJump;
	private GameObject explosion;
	private bool timerJumpWall = false;
	private bool dash = false;
	private bool can_dash = false;
	private bool can_move = true;
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

		if (can_move) {
			base.FixedUpdate ();
			if (Input.GetAxis (keyBinds ["Jump"]) != 0 && !timerJumpWall && !timerJump && wallJump.GetComponent<WallJump> ().getWallJump ()) {
				StartCoroutine ("timer_jump_wall");
				this.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				this.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (200 * this.transform.localScale.x, 375));
				WallJump = true;
				doubleJumped = true;
			}
			if (jump.GetComponent<Jump> ().getCanJump ()) {
				WallJump = false;
			}
			if (Input.GetAxis (keyBinds ["Dash"]) != 0 && !can_dash) {
				StartCoroutine ("timer_dash");
				this.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				this.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 550));
				doubleJumped = true;
			}
			explosion.GetComponent<CircleCollider2D> ().enabled = dash;
			if (dash && this.gameObject.GetComponent<Rigidbody2D> ().velocity.y < 0) {
				dash = false;
				this.gameObject.layer = 9;
			}
		} else {
			animator.SetFloat(animHorizontalVel , 0);
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
		yield return new WaitForSeconds (5f);
		this.gameObject.layer = 9;
		dash = false;
		can_dash = false;
	}
	public bool getDash() {
		return dash;
	}

	protected override void Attack()
	{
		// On trace une sphère devant le joueur pour voir si on a touché le héros
		Collider2D [] ray = Physics2D.OverlapCircleAll( this.transform.position + -this.transform.right * this.transform.localScale.x / 2, 0.25f );
		base.Attack();
		foreach ( Collider2D rc in ray )
		{
			if ( rc.transform.gameObject.tag == "Player" )
			{
				GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().DisplayVictory( this, playerID );
				rc.transform.gameObject.GetComponent<PlayerController>().Die();
			}
		}

		/*if ( wallJump.GetComponent<WallJump>().CollidedObj == null )
			return;
		Debug.Log (wallJump.GetComponent<WallJump>().CollidedObj);
		if ( wallJump.GetComponent<WallJump>().CollidedObj.tag == "Player" )
		{
			GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().DisplayVictory( this, playerID );
			wallJump.GetComponent<WallJump>().CollidedObj.GetComponent<PlayerController>().Die();
		}*/
	}

	public bool CanMove
	{
		get { return can_move; }
		set { can_move = value; }
	}
}
