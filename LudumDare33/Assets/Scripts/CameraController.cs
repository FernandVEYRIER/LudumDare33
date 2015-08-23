using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public 		int start;
	public 		int end;
	public		float speed;
	public GameObject smoke;
	/*private		float shake_decay = .005f;
	private		float shake_intensity;*/

	private Animation shakeAnim;

	public GameObject playerSpawnZone;
	public GameObject monsterSpawnZone;

	void Start () 
	{
		shakeAnim = Camera.main.GetComponent<Animation>();
	}

	public void PlayShakeAnim()
	{
		shakeAnim.Play();
	}

	void Update () 
	{
		if (this.transform.position.y < end) 
		{
			this.transform.Translate(0, speed * Time.deltaTime, 0);
		}
		/*if (Random.Range(0, 1000) == 2) {
			shake_intensity = .6f;
		} else {
			this.transform.position = new Vector3(0, transform.position.y, -15);

		}
		if(shake_intensity > 0){
			Vector3 shake = transform.position + Random.insideUnitSphere * shake_intensity;
			transform.position = new Vector3(shake.x, transform.position.y + speed * Time.deltaTime, shake.z);
			shake_intensity -= shake_decay;
		}*/
	}

	void OnTriggerEnter2D( Collider2D col )
	{
		if ( col.gameObject.tag == "Monster" )
		{
			Instantiate( smoke, col.gameObject.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity );
			Destroy( col.gameObject );
			this.transform.GetChild(1).gameObject.GetComponent<TryRespawnPawn>().HasToRespawnPlayer = 2;
		}
		else if ( col.gameObject.tag == "Player" )
		{
			//Debug.Log(smoke.GetComponent<Animation>().clip.length);
			 GameObject _smoke = (GameObject) Instantiate( smoke, col.gameObject.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity );
			Destroy(_smoke, 1f);
			Destroy( col.gameObject );
			this.transform.GetChild(2).gameObject.GetComponentInChildren<TryRespawnPawn>().HasToRespawnPlayer = 1;
		}
	}
}