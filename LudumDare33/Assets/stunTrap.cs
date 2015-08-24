using UnityEngine;
using System.Collections;

public class stunTrap : MonoBehaviour {

	private bool use = false;
	void OnCollisionEnter2D(Collision2D col) {

		if (col.collider.tag == "Monster" && !use) {
			this.GetComponent<Rigidbody2D>().isKinematic = true;
			this.GetComponent<BoxCollider2D>().enabled = false;
			use = true;
			StartCoroutine(move_timer(col));
		}
	}
	IEnumerator move_timer(Collision2D col) {
		col.collider.GetComponent<MonsterController>().CanMove = false;
		yield return new WaitForSeconds (1);
		col.collider.GetComponent<MonsterController>().CanMove = true;
		Destroy (this.gameObject);
	}
}
