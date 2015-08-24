using UnityEngine;
using System.Collections;

public class zip : MonoBehaviour {

	public GameObject smoke;
	private float time;
	private bool canBoom = false;
	private bool destroy = false;
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		player.GetComponent<BasicController> ().PlayerAttack (0);
		Destroy(this, 1.5f);
	}

	void Update() {

		if (time < 1.2f) {
			time += Time.deltaTime;
		} else {
			canBoom = true;
		}
		if (gameObject) {
			transform.position = player.transform.position;
		}
		if (destroy) {

		}
	}

	void OnTriggerEnter2D(Collider2D col) {

		if (col.tag == "Monster" && canBoom && !destroy) {

			GameObject i_smoke = (GameObject) Instantiate( smoke, GameObject.FindGameObjectWithTag("Monster").gameObject.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity );
			Destroy(i_smoke, 1f);
			Destroy( GameObject.FindGameObjectWithTag("Monster").gameObject );
			GameObject.Find("MonsterRespawnZone").GetComponentInChildren<TryRespawnPawn>().HasToRespawnPlayer = 2;
		}
	}
}
