using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class item {
	public GameObject obj;
	public float percents = 10;
}

public class SpawnItems : MonoBehaviour {
	
	public item[] items = new item[10];
	private List<GameObject> item_spawn = new List<GameObject>();
	private bool canSpawn = true;
	// Use this for initialization
	void Start () {

		foreach (item item in items) {
			for (int i = 0; i < item.percents; i++) {
				item_spawn.Add(item.obj);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if (canSpawn) {
			StartCoroutine("Spawn");
			int index = Random.Range(0, item_spawn.Count);
			if (!this.GetComponent<TryRespawnPawn>().Use) {
				this.GetComponent<TryRespawnPawn>().Use = true;
				Instantiate(item_spawn[index], this.GetComponent<TryRespawnPawn>().LastValidPlateform, Quaternion.identity);
			}
		}
	}

	IEnumerator Spawn() {
		canSpawn = false;
		yield return new WaitForSeconds(Random.Range(4,7));
		canSpawn = true;
	}
}
