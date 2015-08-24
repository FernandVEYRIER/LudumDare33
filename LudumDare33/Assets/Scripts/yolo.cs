using UnityEngine;
using System.Collections;

public class yolo : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col) {

		if (col.tag == "Monster") {
			print ("yolo");
		}
	}
}
