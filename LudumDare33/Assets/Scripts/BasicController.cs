using UnityEngine;
using System.Collections;

public abstract class BasicController : MonoBehaviour {

	public float jump_strenght = 0;
	public float horizontal_strenght = 0;
	protected float attackDelay = 0.3f;

	protected bool timerJump = false;
	protected GameObject jump;
	protected bool WallJump = false;

	protected Animator animator;

	private int animVerticalVel;
	private int animHorizontalVel;
	private int animIsGrounded;
	private int animAttack;

	private float attackCurrentDelay;
	
	void Awake () 
	{
		jump = this.transform.GetChild (0).gameObject;
		animator = this.GetComponent<Animator>();

		animVerticalVel = Animator.StringToHash( "VerticalVel" );
		animHorizontalVel = Animator.StringToHash( "HorizontalVel" );
		animIsGrounded = Animator.StringToHash( "isGrounded" );
		animAttack = Animator.StringToHash( "Attack" );

		attackCurrentDelay = 0;
	}

	void Update()
	{
		if ( attackCurrentDelay > 0 )
			attackCurrentDelay -= Time.deltaTime;
	}
	
	protected void FixedUpdate () {

		if ( jump.GetComponent<Jump>().getCanJump() )
		{
			Debug.Log("can jump");
			animator.SetBool( animIsGrounded, true );
		}
		else
		{
			Debug.Log("cant jump");
			animator.SetBool( animIsGrounded, false );
		}

		if ( jump.GetComponent<Jump>().Landed )
		{
			Camera.main.transform.parent.GetComponent<CameraController>().PlayShakeAnim();
        }

		animator.SetFloat( animHorizontalVel, Input.GetAxis ("Horizontal") );
		animator.SetFloat( animVerticalVel, Mathf.Abs(this.GetComponent<Rigidbody2D>().velocity.y ));

		if (jump.GetComponent<Jump>().getCanJump() && Input.GetAxis ("Jump") != 0 && !timerJump) {
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

		if ( Input.GetAxis( "Fire1" ) == 1 && attackCurrentDelay <= 0 )
		{
			Attack();
		}
	}

	IEnumerator timer_jump() {

		timerJump = true;
		yield return new WaitForSeconds (0.07f);
		timerJump = false;
	}

	protected void Attack()
	{
		attackCurrentDelay = attackDelay;
		animator.SetTrigger( animAttack );
	}
}
