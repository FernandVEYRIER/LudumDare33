using UnityEngine;
using System.Collections;

public class stunTrap : MonoBehaviour {

	public AudioClip sound;

	private bool use = false;
	private AudioSource audioSource;

	void Start()
	{
		audioSource = this.GetComponent<AudioSource>();
	}

	void OnCollisionEnter2D(Collision2D col) {

		if (col.collider.tag == "Monster" && !use) {
			this.GetComponent<Rigidbody2D>().isKinematic = true;
			this.GetComponent<BoxCollider2D>().enabled = false;
			use = true;
			StartCoroutine(move_timer(col));
			audioSource.PlayOneShot( sound );
		}
	}
	IEnumerator move_timer(Collision2D col) {

		col.collider.GetComponent<MonsterController>().CanMove = false;
		col.collider.GetComponent<SpriteRenderer> ().color = Color.red;
		yield return new WaitForSeconds (1);
		col.collider.GetComponent<SpriteRenderer> ().color = Color.white;
		col.collider.GetComponent<MonsterController>().CanMove = true;
		Destroy (this.gameObject);
	}
}
