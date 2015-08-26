using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BasicController : MonoBehaviour {

	public AudioClip [] sounds;
	protected AudioSource audioSource;

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
	protected int animAttackID;

	private int animVerticalVel;
	protected int animHorizontalVel;
	private int animIsGrounded;
	private int animAttack;
	private int animDeath;
	private int animID;
	
	private float attackCurrentDelay;
	private bool isDead;

	protected bool doubleJumped;

	protected virtual void Awake () 
	{
		jump = this.transform.GetChild (0).gameObject;
		animator = this.GetComponent<Animator>();

		animVerticalVel = Animator.StringToHash( "VerticalVel" );
		animHorizontalVel = Animator.StringToHash( "HorizontalVel" );
		animIsGrounded = Animator.StringToHash( "isGrounded" );
		animAttack = Animator.StringToHash( "Attack" );
		animDeath = Animator.StringToHash( "Die" );
		animID = Animator.StringToHash( "AnimID" );

		attackCurrentDelay = 0;

		imputName = new List<string>();
		imputName.Add( "Fire1" );
		imputName.Add( "Jump" );
		imputName.Add( "Horizontal" );
		imputName.Add( "item_1" );
		imputName.Add( "item_2" );
		imputName.Add( "item_3" );
		imputName.Add( "Dash" );
		// Les binds sont gérés par le game manager

		// Player ID est initialisé dans les classes enfant
		scale = this.transform.localScale.x;

		audioSource = this.GetComponent<AudioSource>();
	}

	public void SetKeyBinds( int characterID )
	{
		if ( characterID == 1 )
		{
			keyBinds = new Dictionary<string, string>();
			foreach ( string str in imputName )
			{
				keyBinds.Add( str, str );
			}
		}
		else if ( characterID == 2 )
		{
			keyBinds = new Dictionary<string, string>();
			foreach ( string str in imputName )
			{
				keyBinds.Add( str, str + "Alt" );
			}
		}
	}

	protected virtual void Update()
	{
		if ( attackCurrentDelay > 0 )
			attackCurrentDelay -= Time.deltaTime;
	}
	
	protected virtual void FixedUpdate () {

		if ( isDead )
			return;

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
		if ( jump.GetComponent<Jump>().Landed && GetType() == typeof(MonsterController) && doubleJumped )
		{
			audioSource.PlayOneShot( sounds[1] );
			Camera.main.transform.parent.GetComponent<CameraController>().PlayShakeAnim();
			doubleJumped = false;
        }

		animator.SetFloat( animHorizontalVel, CustomInput.GetAxis (keyBinds[("Horizontal")] ) );
		animator.SetFloat( animVerticalVel, Mathf.Abs(this.GetComponent<Rigidbody2D>().velocity.y ));
		#endregion

		if (jump.GetComponent<Jump>().getCanJump() && CustomInput.GetAxis (keyBinds["Jump"]) != 0 && !timerJump) {
		    StartCoroutine("timer_jump");
			this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jump_strenght));
		}
		if (jump.GetComponent<Jump> ().getCanJump ()) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);
		}
		if (CustomInput.GetAxis (keyBinds["Horizontal"]) != 0 && !jump.GetComponent<Jump> ().getCanJump () && !WallJump) {

			this.transform.Translate(new Vector2(horizontal_strenght * Time.deltaTime * CustomInput.GetAxis (keyBinds["Horizontal"]) * 0.8f,0));	
		}
		if (CustomInput.GetAxis (keyBinds["Horizontal"]) != 0 && jump.GetComponent<Jump> ().getCanJump ()) {
			this.transform.Translate(new Vector2(horizontal_strenght * Time.deltaTime * CustomInput.GetAxis (keyBinds["Horizontal"]),0));
		}
		if (CustomInput.GetAxis (keyBinds["Horizontal"]) < 0) {
			this.transform.localScale = new Vector2(scale, this.transform.localScale.y);
		} else if (CustomInput.GetAxis (keyBinds["Horizontal"]) > 0) {
			this.transform.localScale = new Vector2(scale * -1, this.transform.localScale.y);
		}

		if ( CustomInput.GetAxis( keyBinds["Fire1"] ) == 1 && attackCurrentDelay <= 0 )
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
		if ( this.GetType() == typeof(MonsterController) )
		{
			attackCurrentDelay = attackDelay;
			animator.SetTrigger( animAttack );
			audioSource.PlayOneShot( sounds[0] );
		}
    }

	public void PlayerAttack( int _animID)
	{
		animID = _animID;
		animator.SetInteger( animID, animAttackID );
		animator.SetTrigger( animAttack );
		audioSource.PlayOneShot( sounds[animAttackID] );
	}
    
    public void Die()
	{
		animator.SetTrigger( animDeath );
		isDead = true;
		audioSource.PlayOneShot( sounds[1] );
	}

	public void Lives()
	{
		animator.SetTrigger( "AnimEnd" );
		isDead = true;
	}

	public bool IsDead
	{
		get
		{
			return isDead;
		}
		set
		{
			isDead = value;
		}
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
