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
