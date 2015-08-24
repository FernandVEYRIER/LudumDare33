using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	private List<GameObject> inventory = new List<GameObject>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D col) {

		if (col.collider.tag == "item") {
			inventory.Add(col.collider.gameObject);
		}
	}
}
