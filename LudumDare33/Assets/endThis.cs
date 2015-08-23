using UnityEngine;
using System.Collections;

public class endThis : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col) {

		if (col.tag == "Player") {
			GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().DisplayVictory(col.gameObject, col.gameObject.GetComponent<BasicController>().playerID);
		}
	}
} 
