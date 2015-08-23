using UnityEngine;
using System.Collections;

public class TryRespawnPawn : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject monsterPrefab;
	public float respawnDelay;

	private int hasToRespawnPlayer;
	private Vector3 lastValidPlateform;
	private float timeUntilSpawn;
	private GameObject gm;

	void Start()
	{
		timeUntilSpawn = respawnDelay;
		gm = GameObject.FindGameObjectWithTag( "GameManager" );
	}

	void OnTriggerEnter2D( Collider2D col )
	{
		if ( col.gameObject.tag == "ground" )
			lastValidPlateform = col.gameObject.transform.position;
	}

	void Update()
	{
		if ( hasToRespawnPlayer != 0 )
		{
			timeUntilSpawn -= Time.deltaTime;
			gm.GetComponent<GameManager>().SetRespawnTimeText( hasToRespawnPlayer, Mathf.CeilToInt(timeUntilSpawn) );

			if ( timeUntilSpawn <= 0 )
			{
				if ( lastValidPlateform != Vector3.zero )
				{
					if ( hasToRespawnPlayer == 1 )
					{
						Instantiate( playerPrefab, lastValidPlateform, Quaternion.identity );
					}
					else
					{
						Instantiate( monsterPrefab, lastValidPlateform, Quaternion.identity );
					}
					hasToRespawnPlayer = 0;
					lastValidPlateform = Vector3.zero;
				}
			}
		}
		else
		{
			timeUntilSpawn = respawnDelay;
		}
	}

	public int HasToRespawnPlayer
	{
		get { return hasToRespawnPlayer; }
		set
		{
			if ( value == 1 || value == 2 )
				hasToRespawnPlayer = value; 
			else
				hasToRespawnPlayer = 0;
		}
	}
}
