using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : BasicController {
	
	private GameObject wallJump;
	private bool timerJumpWall = false;
	private List<GameObject> inventory = new List<GameObject>();
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
	void Update() {

		if (GetType() == typeof (PlayerController)) {
			print(Input.GetAxis (keyBinds ["item_1"]));
			if (Input.GetAxis (keyBinds ["item_1"]) != 0) {
				Instantiate(inventory[0], this.transform.position, Quaternion.identity);
				inventory[0] = null;
			} else if (Input.GetAxis (keyBinds ["item_2"]) != 0) {
				Instantiate(inventory[1], this.transform.position, Quaternion.identity);
				inventory[1] = null;
			} else if (Input.GetAxis (keyBinds ["item_3"]) != 0) {
				Instantiate(inventory[2], this.transform.position, Quaternion.identity);
				inventory[2] = null;
			}
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
	void OnCollisionEnter2D(Collision2D col) {

		if (col.collider.tag == "item" && inventory.Count < 4) {

			int i = 0;
			foreach (var item in inventory) {
				if (item == null) {
					inventory.Insert(i, col.collider.gameObject.GetComponent<Item>().prefabs);
					return;
				}
			}
			inventory.Add(col.collider.gameObject.GetComponent<Item>().prefabs);
			print (inventory[0]);
			Destroy(col.collider.gameObject);
		}
	}
}
