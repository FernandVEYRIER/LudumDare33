using UnityEngine;
using System.Collections;

public class MonsterController : BasicController {

	private GameObject wallJump;
	private GameObject explosion;
	private bool timerJumpWall = false;
	private bool dash = false;
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
			doubleJumped = true;
		}
		if (jump.GetComponent<Jump>().getCanJump()) {
			WallJump = false;
		}
		if (Input.GetAxis (keyBinds["Dash"]) != 0 && !dash) {
			StartCoroutine("timer_dash");
			this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 550));
			doubleJumped = true;
		}
		explosion.GetComponent<CircleCollider2D> ().enabled = dash;
	}

	IEnumerator timer_jump_wall() {
		timerJumpWall = true;
		yield return new WaitForSeconds (0.07f);
		timerJumpWall = false;
	}

	IEnumerator timer_dash() {
		dash = true;
		print (gameObject);
		this.gameObject.layer = 10;
		print (gameObject.layer);
		yield return new WaitForSeconds (2f);
		this.gameObject.layer = 9;
		print (gameObject.layer);
		dash = false;
	}
	public bool getDash() {
		return dash;
	}

	protected override void Attack()
	{
		RaycastHit2D [] ray = Physics2D.CircleCastAll( this.transform.position, 0.2f, Vector2.right );
		base.Attack();

		foreach ( RaycastHit2D rc in ray )
		{
			if ( rc.transform.gameObject.tag == "Player" )
			{
				Debug.Log("KILLED PLAYER");
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
}
