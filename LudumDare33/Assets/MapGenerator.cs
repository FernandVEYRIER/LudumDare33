using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MapGenerator : MonoBehaviour {

	public GameObject end;
	public GameObject base_template;
	public GameObject[] backgound;
	public GameObject[] templates;
	private System.Random rng = new System.Random();
	private List<int> templates_id = new List<int>();
	private List<int> base_templates_id = new List<int>();

	public List<T> Shuffle<T>(this List<T> list) {
		
		int n = list.Count;
		while (n > 1) {
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
		return list;
	}

	// Use this for initialization
	void Awake () {

		int max = templates.Length;
		int nb_templates = Random.Range(5, max);
		for (int i = 0; i < max; i++) {
			base_templates_id.Add(i);
		}
		base_templates_id = Shuffle (base_templates_id);
		int y = 0;
		while (y < nb_templates) {
			templates_id.Add(base_templates_id[y]);
			y++;
		}
		if (templates_id.Count % 2 != 0) {
			templates_id.Add(base_templates_id[y]);
		}
		Instantiate(backgound[0]);
		Vector3 final_background = Vector3.zero;
		float templates_pos_y = this.transform.position.y - base_template.GetComponent<SpriteRenderer> ().bounds.size.y/2;
		for (int i = 0; i < templates_id.Count; i++) {
			GameObject obj = templates[templates_id[i]];
			Vector3 position = this.transform.position + new Vector3(0, base_template.GetComponent<SpriteRenderer> ().bounds.size.y * i , 0);
			Instantiate(obj, position, Quaternion.identity);
			Instantiate(base_template, position - new Vector3(0, base_template.GetComponent<SpriteRenderer> ().bounds.size.y/2, 0), Quaternion.identity);
			if (i != 0 && i % 2 == 0) {
				final_background = this.transform.position + new Vector3(0, backgound[1].GetComponent<SpriteRenderer> ().bounds.size.y * (i / 2), 0);
				Instantiate(backgound[1], final_background, Quaternion.identity);
			}
		}
		Instantiate(backgound[2], final_background + new Vector3(0, backgound[2].GetComponent<SpriteRenderer> ().bounds.size.y), Quaternion.identity);
	}
}
