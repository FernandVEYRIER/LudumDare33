using UnityEngine;
using System.Collections;

public class TryRespawnPawn : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject monsterPrefab;

	private int hasToRespawnPlayer;
	private Vector3 lastValidPlateform;

	void OnTriggerEnter2D( Collider2D col )
	{
		if ( col.gameObject.tag == "ground" )
			lastValidPlateform = col.gameObject.transform.position;
	}

	void Update()
	{
		if ( hasToRespawnPlayer != 0 )
		{
			Debug.Log (lastValidPlateform);
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
