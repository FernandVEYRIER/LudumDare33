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
	private int playerID;
	private bool use = false;

	void Start()
	{
		timeUntilSpawn = respawnDelay;
		gm = GameObject.FindGameObjectWithTag( "GameManager" );
	}

	void OnTriggerEnter2D( Collider2D col )
	{
		if (col.gameObject.tag == "ground") {
			use = false;
			lastValidPlateform = col.gameObject.transform.position;
		}
	}

	void Update()
	{
		if ( hasToRespawnPlayer != 0 )
		{
			timeUntilSpawn -= Time.deltaTime;
			gm.GetComponent<GameManager>().SetRespawnTimeText( playerID, Mathf.CeilToInt(timeUntilSpawn) );

			if ( timeUntilSpawn <= 0 )
			{
				if ( lastValidPlateform != Vector3.zero )
				{
					if ( hasToRespawnPlayer == 1 )
					{
						gm.GetComponent<GameManager>().SpawnPlayer( playerPrefab, lastValidPlateform, 1 );
					}
					else
					{
						gm.GetComponent<GameManager>().SpawnPlayer( monsterPrefab, lastValidPlateform, 2 );
					}
					hasToRespawnPlayer = 0;
					//lastValidPlateform = Vector3.zero;
				}
			}
		}
		else
		{
			timeUntilSpawn = respawnDelay;
		}
	}

	public int PlayerID
	{
		set
		{
			if ( value != 1 && value != 2 )
				playerID = 0;
			else
				playerID = value;
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
	public Vector3 LastValidPlateform
	{
		get { return lastValidPlateform; }
		set
		{
			lastValidPlateform = value; 
		}
	}
	public bool Use
	{
		get { return use; }
		set
		{
			use = value; 
		}
	}
}
