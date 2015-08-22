using UnityEngine;
using System.Collections;

public class randomMap : MonoBehaviour {

	public 	Transform _brick;
	public 	Transform[] _levels;
	public 	int total_lengh = 10;
	public 	int mode = 1;
	private	int lengh;
	private int nb_wall;
	private	float pos_y = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (pos_y < 100) {
			if (mode == 0) {
				int nb_wall = Random.Range(1,4);
				for(int i = 0; i < nb_wall; i++) {
					int lengh = Random.Range(total_lengh / nb_wall - 3, total_lengh / nb_wall + 3);
					float pos_x = Random.Range(1, 10) % 7 - 5f;
					for(int j = 0; j < lengh; j++) {
						Transform newGameObj = Instantiate( _brick, new Vector3(pos_x, pos_y - 2, 0), Quaternion.identity) as Transform;
						pos_x += 0.3f;
					}
				}
				pos_y += Random.Range(1, 2) + .5f;
			} else {
			Transform newGameObj = Instantiate( _levels[Random.Range(0, _levels.Length)], new Vector3(0, pos_y, 0), Quaternion.identity) as Transform;
			pos_y += 10;
			}
		}
	}
}
