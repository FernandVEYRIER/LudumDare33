﻿using UnityEngine;
using System.Collections;

public class PlayerController : BasicController {
	
	private GameObject wallJump;
	private bool timerJumpWall = false;
	// Use this for initialization
	
	protected override void Awake()
	{
		playerID = 1;
		base.Awake();
	}
	
	void Start () 
	{
		wallJump = this.transform.GetChild (1).gameObject;
	}
	
	protected override void FixedUpdate () 
	{
		base.FixedUpdate();
		if (Input.GetAxis (keyBinds["Jump"]) != 0 && !timerJumpWall && !timerJump && wallJump.GetComponent<WallJump>().getWallJump())
		{
			StartCoroutine("timer_jump_wall");
			this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(200 * this.transform.localScale.x, 500));
			WallJump = true;
		}
		if (jump.GetComponent<Jump>().getCanJump()) 
		{
			WallJump = false;
		}
	}
	
	IEnumerator timer_jump_wall() 
	{
		timerJumpWall = true;
		yield return new WaitForSeconds (0.07f);
		timerJumpWall = false;
	}
	
	protected override void Attack()
	{
		base.Attack();
		if ( wallJump.GetComponent<WallJump>().CollidedObj == null )
			return;
		if ( wallJump.GetComponent<WallJump>().CollidedObj.tag == "Monster" )
		{
			GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().DisplayVictory( this, playerID );
		}
	}
}