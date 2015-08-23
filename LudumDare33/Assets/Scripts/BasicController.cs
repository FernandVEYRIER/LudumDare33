using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BasicController : MonoBehaviour {

	public float jump_strenght = 0;
	public float horizontal_strenght = 0;
	protected float attackDelay = 0.3f;
	private float scale;

	// Contient tous les binds pour chaque joueur
	protected Dictionary<string, string> keyBinds;
	private List<string> imputName;

	private int _playerID = 0;

	protected bool timerJump = false;
	protected GameObject jump;
	protected bool WallJump = false;

	protected Animator animator;

	private int animVerticalVel;
	private int animHorizontalVel;
	private int animIsGrounded;
	private int animAttack;

	private float attackCurrentDelay;
	private int currentKeyBind;
	
	protected virtual void Awake () 
	{
		jump = this.transform.GetChild (0).gameObject;
		animator = this.GetComponent<Animator>();

		animVerticalVel = Animator.StringToHash( "VerticalVel" );
		animHorizontalVel = Animator.StringToHash( "HorizontalVel" );
		animIsGrounded = Animator.StringToHash( "isGrounded" );
		animAttack = Animator.StringToHash( "Attack" );

		attackCurrentDelay = 0;

		imputName = new List<string>();
		imputName.Add( "Fire1" );
		imputName.Add( "Jump" );
		imputName.Add( "Horizontal" );
		imputName.Add("item_1");
		imputName.Add("item_2");
		imputName.Add("item_3");
		// Les binds sont gérés par le game manager

		// Player ID est initialisé dans les classes enfant
		scale = this.transform.localScale.x;
	}

	public void SetKeyBinds( int characterID )
	{
		if ( characterID == 1 )
		{
			Debug.Log("P1 settings");
			keyBinds = new Dictionary<string, string>();
			foreach ( string str in imputName )
			{
				keyBinds.Add( str, str );
			}
			currentKeyBind = 1;
		}
		else if ( characterID == 2 )
		{
			Debug.Log("P2 settings");
			keyBinds = new Dictionary<string, string>();
			foreach ( string str in imputName )
			{
				keyBinds.Add( str, str + "Alt" );
			}
			currentKeyBind = 2;
		}
	}

	protected virtual void Update()
	{
		if ( attackCurrentDelay > 0 )
			attackCurrentDelay -= Time.deltaTime;
	}
	
	protected virtual void FixedUpdate () {

		#region Cette région s'occupe de la gestion des animations
		if ( jump.GetComponent<Jump>().getCanJump() )
		{
			animator.SetBool( animIsGrounded, true );
		}
		else
		{
			animator.SetBool( animIsGrounded, false );
		}
		// Si le controller est un monstre, on fait le shake de caméra quand il retombe
		if ( jump.GetComponent<Jump>().Landed && GetType() == typeof(MonsterController) )
		{
			Camera.main.transform.parent.GetComponent<CameraController>().PlayShakeAnim();
        }

		animator.SetFloat( animHorizontalVel, Input.GetAxis (keyBinds[("Horizontal")] ) );
		animator.SetFloat( animVerticalVel, Mathf.Abs(this.GetComponent<Rigidbody2D>().velocity.y ));
		#endregion

		if (jump.GetComponent<Jump>().getCanJump() && Input.GetAxis (keyBinds["Jump"]) != 0 && !timerJump) {
		    StartCoroutine("timer_jump");
			this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jump_strenght));
		}
		if (jump.GetComponent<Jump> ().getCanJump ()) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);
		}
		if (Input.GetAxis (keyBinds["Horizontal"]) != 0 && !jump.GetComponent<Jump> ().getCanJump () && !WallJump) {

			this.transform.Translate(new Vector2(horizontal_strenght * Time.deltaTime * Input.GetAxis (keyBinds["Horizontal"]) * 0.8f,0));	
		}
		if (Input.GetAxis (keyBinds["Horizontal"]) != 0 && jump.GetComponent<Jump> ().getCanJump ()) {
			this.transform.Translate(new Vector2(horizontal_strenght * Time.deltaTime * Input.GetAxis (keyBinds["Horizontal"]),0));
		}
		if (Input.GetAxis (keyBinds["Horizontal"]) < 0) {
			this.transform.localScale = new Vector2(scale, this.transform.localScale.y);
		} else if (Input.GetAxis (keyBinds["Horizontal"]) > 0) {
			this.transform.localScale = new Vector2(scale * -1, this.transform.localScale.y);
		}

		if ( Input.GetAxis( keyBinds["Fire1"] ) == 1 && attackCurrentDelay <= 0 )
		{
			Attack();
		}
	}

	IEnumerator timer_jump() {

		timerJump = true;
		yield return new WaitForSeconds (0.07f);
		timerJump = false;
	}

	protected virtual void Attack()
	{
		attackCurrentDelay = attackDelay;
		animator.SetTrigger( animAttack );
	}

	public int playerID
	{
		get
		{
			return _playerID;
		}
		set
		{
			if ( value != 1 && value != 2 )
				_playerID = 1;
			else
				_playerID = value;
		}
	}
}
