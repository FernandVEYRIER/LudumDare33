using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public 		int start;
	public 		int end;
	public		float speed;
	private		float shake_decay = .005f;
	private		float shake_intensity;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.y < end) {
			this.transform.Translate(0, speed * Time.deltaTime, 0);
		}
		if (Random.Range(0, 1000) == 2) {
			shake_intensity = .6f;
		} else {
			this.transform.position = new Vector3(0, transform.position.y, -15);

		}
		if(shake_intensity > 0){
			Vector3 shake = transform.position + Random.insideUnitSphere * shake_intensity;
			transform.position = new Vector3(shake.x, transform.position.y + speed * Time.deltaTime, shake.z);
			shake_intensity -= shake_decay;
		}
	}
}