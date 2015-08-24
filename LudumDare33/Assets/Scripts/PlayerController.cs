using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : BasicController {

	private List<GameObject> inventory = new List<GameObject>();
	// Use this for initialization
	
	protected override void Awake()
	{
		playerID = 1;
		base.Awake();
	}
	
	void Start () 
	{
	}

	protected override void Update() {
		base.Update();
		if (GetType() == typeof (PlayerController)) {
			if (Input.GetAxis (keyBinds ["item_1"]) != 0 && inventory.Count >= 1 && inventory[0] != null) {
				Instantiate(inventory[0], this.transform.position, Quaternion.identity);
				inventory[0] = null;
				GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().AddItem(null, 0, playerID);
			} else if (Input.GetAxis (keyBinds ["item_2"]) != 0  && inventory.Count >= 2 && inventory[1] != null) {
				Instantiate(inventory[1], this.transform.position, Quaternion.identity);
				inventory[1] = null;
				GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().AddItem(null, 1, playerID);
			} else if (Input.GetAxis (keyBinds ["item_3"]) != 0 && inventory.Count == 3 && inventory[2] != null) {
				Instantiate(inventory[2], this.transform.position, Quaternion.identity);
				inventory[2] = null;
				GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().AddItem(null, 2, playerID);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col) {

		if (col.collider.tag == "item" && inventory.Count < 3) {

			int i = 0;
			GameObject obj = null;
			int index;
			audioSource.PlayOneShot( sounds[2] );
			foreach (GameObject item in inventory) {
				if (item == null) {
					obj = col.collider.gameObject.GetComponent<Item>().prefabs;
					break;
				}
				i++;
			}
			if (obj != null) {
				index = i;
				inventory.Insert(i, obj);
			} else {
				obj = col.collider.gameObject.GetComponent<Item>().prefabs;
				inventory.Add(obj);
				index = inventory.Count - 1;
			}
			GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().AddItem(col.collider.gameObject.GetComponent<Item>().sprite, index, 1);
			Destroy(col.collider.gameObject);
		}
	}
}
