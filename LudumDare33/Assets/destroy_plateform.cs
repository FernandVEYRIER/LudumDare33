using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class destroy_plateform : MonoBehaviour {

	public GameObject plateform;
	public int nb_blocks;
	private List<GameObject> plateforms = new List<GameObject>();
	// Use this for initialization
	void Awake () {

		for (int i = 0; i < nb_blocks; i++) {
			GameObject tmp = (GameObject) Instantiate(plateform, this.transform.position + new Vector3(i * plateform.transform.localScale.x * plateform.GetComponent<SpriteRenderer>().sprite.bounds.size.x, 0, 0), Quaternion.identity);
			plateforms.Add(tmp);
			tmp.transform.parent = this.transform;
		}
		BoxCollider2D box  = this.gameObject.AddComponent<BoxCollider2D> ();
		float size = nb_blocks * plateform.transform.localScale.x * plateform.GetComponent<SpriteRenderer> ().sprite.bounds.size.x;
		box.size = new Vector2 (size, plateform.GetComponent<SpriteRenderer> ().sprite.bounds.size.y);
		box.offset = new Vector2 ((size - plateform.GetComponent<SpriteRenderer> ().bounds.size.x) / 2, 0);
	}

	public void destruct(){
		foreach (GameObject item in plateforms) {

			if (!item.GetComponent<enable_explosion>().getRigid()) {
				item.GetComponent<enable_explosion>().setRigid(true);
				item.AddComponent<Rigidbody2D>();
				this.GetComponent<BoxCollider2D>().enabled = false;
			}
		}
		Destroy (gameObject, 2);
	}
////	// Update is called once per frame
//	void OnCollisionEnter2D (Collision2D col) {
//
//		if (col.collider.tag == "Monster" && col.collider.GetComponent<MonsterController>().getDash()) {
//			this.GetComponent<BoxCollider2D>().enabled = false;
//		}
//	}
}
