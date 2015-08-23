using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	// TODO : Destruction des particules smoke
	//		  Curseur P1 cassé

	private GameObject player;
	private GameObject monster;

	[Header("Canvas")]
	public GameObject canvasPause;
	public GameObject canvasPauseButton;
	public GameObject canvasVictory;
	public Text canvasVictoryText;
	public GameObject canvasVictoryButton;
	public Slider canvasPauseSound;
	[Header("Player")]
	public Text countdownText;
	public Text countdownP1;
	public Text countdownP2;
	public Text nameP1;
	public Text nameP2;
	public Image player1Sprite;
	public Image player2Sprite;
	public Image [] inventorySpriteP1;
	public Image [] inventorySpriteP2;
	public GameObject player1Cursor;
	public GameObject player2Cursor;
	[Header("")]
	public EventSystem es;
	
	private bool isResumingGame;
	private float resumeDelay = 0;
	private float currentElapsedTime;

	private int currentBindP1;
	private int currentBindP2;

	private GameObject player1CursorInstance;
	private GameObject player2CursorInstance;
	private bool isSwitchingCursor;
	private Vector3 currentVelCursorP1;
	private Vector3 currentVelCursorP2;
	private int cursorP1TargetID;
	private int cursorP2TargetID;

	void Start () 
	{
		currentBindP1 = 1;
		currentBindP2 = 2;

		canvasPauseSound.value = PlayerPrefs.GetFloat( "MasterVolume", 1 );
		canvasPause.SetActive( false );
		canvasVictory.SetActive( false );

		Time.timeScale = 0;
		currentElapsedTime = Time.realtimeSinceStartup + resumeDelay;
        isResumingGame = true;

		nameP1.text = PlayerPrefs.GetString("Player1", "Player1");
		nameP2.text = PlayerPrefs.GetString("Player2", "Player2");
		if ( nameP1.text == "" )
			nameP1.text = "Player 1";
		if ( nameP2.text == "" )
			nameP2.text = "Player 2";

		player = GameObject.FindGameObjectWithTag("Player");
		monster = GameObject.FindGameObjectWithTag("Monster");
		player.GetComponent<PlayerController>().SetKeyBinds( currentBindP1 );
		monster.GetComponent<MonsterController>().SetKeyBinds( currentBindP2 );

		player1CursorInstance = (GameObject) Instantiate( player1Cursor, player.transform.position, Quaternion.identity );
		player2CursorInstance = (GameObject) Instantiate( player2Cursor, monster.transform.position, Quaternion.identity );

		cursorP1TargetID = 1;
		cursorP2TargetID = 2;
	}

	public void SpawnPlayer( Object player, Vector3 position, int playerType )
	{
		GameObject go;

		go = (GameObject) Instantiate( player, position, Quaternion.identity );
		if ( playerType == 1 )
			go.GetComponent<PlayerController>().SetKeyBinds( currentBindP1 );
        else if ( playerType == 2 )
			go.GetComponent<MonsterController>().SetKeyBinds( currentBindP2 );
    }

	void Update ()
	{
		Vector3 target = Vector3.zero;

		// Si on n'a pas de joueur, on le cherche
		if ( player == null )
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}
		else
		{
			// Si la cible du curseur est le héros
			if ( cursorP1TargetID == 1 && player != null )
				target = player.transform.position + new Vector3(0, .45f, 0);
			else if ( cursorP1TargetID == 2 && monster != null )
				target = monster.transform.position + new Vector3(0, 0.8f, 0);

			// Si on ne change pas de curseur, il suit sa cible
			if ( !isSwitchingCursor )
				player1CursorInstance.transform.position = target;
			// Sinon il va vers l'autre joueur
			else
			{
				player1CursorInstance.transform.position = Vector3.SmoothDamp( player1CursorInstance.transform.position,
				                                                              target, ref currentVelCursorP1, 0.12f );
				// Si la distance vers la cible est très petite (ctb) on stoppe le switch
				if ( Mathf.Abs ( Vector3.Distance( player1CursorInstance.transform.position, target ) ) < 0.1f )
				{
					isSwitchingCursor = false;
				}
			}
		}

		if ( monster == null )
		{
			monster = GameObject.FindGameObjectWithTag("Monster");
		}
		else
		{
			if ( cursorP2TargetID == 1 && player != null )
				target = player.transform.position + new Vector3(0, .45f, 0);
			else if ( cursorP2TargetID == 2 && monster != null )
				target = monster.transform.position + new Vector3(0, 0.8f, 0);

            if ( !isSwitchingCursor )
				player2CursorInstance.transform.position = target;
			else
			{
				player2CursorInstance.transform.position = Vector3.SmoothDamp( player2CursorInstance.transform.position,
				                                                              target, ref currentVelCursorP2, 0.12f );
			}
        }

		if ( Input.GetKeyDown( KeyCode.F12 ) )
		{
			SwitchPlayers();
		}

		if ( Input.GetKeyDown( KeyCode.Joystick1Button9 ) || Input.GetKeyDown( KeyCode.Escape ) )
		{
			SetPause();
		}

		// Gère le retour en jeu après une pause, en décomptant trois secondes
		if ( isResumingGame )
		{
			if ( currentElapsedTime - Time.realtimeSinceStartup > 0 )
			{
				if ( currentElapsedTime - Time.realtimeSinceStartup > 2 )
					countdownText.text = "3";
				else if ( currentElapsedTime - Time.realtimeSinceStartup > 1 )
					countdownText.text = "2";
				else
					countdownText.text = "1";
			}
			else
			{
				isResumingGame = false;
				Time.timeScale = 1;
				countdownText.text = "";
			}
		}
	}

	public void SwitchPlayers()
	{
		Sprite _sprite;

		// On change les binds
		currentBindP1 = (currentBindP1 == 1) ? 2 : 1;
		currentBindP2 = (currentBindP2 == 2) ? 1 : 2;
		if ( player != null )
			player.GetComponent<PlayerController>().SetKeyBinds( currentBindP1 );
		if ( monster != null )
			monster.GetComponent<MonsterController>().SetKeyBinds( currentBindP2 );

		// On change les targets des curseurs
		cursorP1TargetID = (cursorP1TargetID == 1) ? 2 : 1;
		cursorP2TargetID = (cursorP2TargetID == 1) ? 2 : 1;
        isSwitchingCursor = true;

		// Et on swap les sprites
		_sprite = player1Sprite.sprite;
		player1Sprite.sprite = player2Sprite.sprite;
		player2Sprite.sprite = _sprite;
		for ( int i = 0; i < 3; i++ )
		{
			_sprite = inventorySpriteP1[i].sprite;
			inventorySpriteP1[i].sprite = inventorySpriteP2[i].sprite;
			inventorySpriteP2[i].sprite = _sprite;
		}
    }
    
    public void SetPause()
	{
		// Si la pause est en train d'etre enlevée, on ne peut pas mettre la pause
		if ( isResumingGame )
			return;
		if ( canvasPause.activeSelf )
		{
			PlayerPrefs.SetFloat( "MasterVolume", canvasPauseSound.value );
			canvasPause.SetActive( false );
			isResumingGame = true;
			currentElapsedTime = Time.realtimeSinceStartup + resumeDelay;
		}
		else 
		{
			canvasPause.SetActive( true );
			es.SetSelectedGameObject( canvasPauseButton );
			Time.timeScale = 0;
		}
	}

	public void SetRespawnTimeText( int player, int value )
	{
		string timeValue;
		if ( value <= 0 )
			timeValue = "";
		else
			timeValue = value.ToString();

		if ( player == 1 )
		{
			countdownP1.text = timeValue;
		}
		else if ( player == 2 )
		{
			countdownP2.text = timeValue;
		}
	}

	public void LoadLevel( int level )
	{
		PlayerPrefs.SetFloat( "MasterVolume", canvasPauseSound.value );
        Time.timeScale = 1;
		Application.LoadLevel( level );
	}

	public void DisplayVictory( Object winner, int playerID )
	{
		canvasVictory.SetActive( true );
		es.SetSelectedGameObject( canvasVictoryButton );
		string playerName = PlayerPrefs.GetString( "Player" + playerID, "Player " + playerID );
		if ( playerName == "" )
			playerName = "Player " + playerID;
		if ( winner.GetType() == typeof(MonsterController) )
		{
			Debug.Log("Monster " + playerName + " wins !" );
			canvasVictoryText.text = "Monster " + playerName + " wins !";
		}
		else
		{
			Debug.Log("Human " + playerName + " wins !" );
			canvasVictoryText.text = "Human " + playerName + " wins !";
        }
	}

	// Change les sprites des items dans l'inventaire
	public void AddItem( Sprite _sprite, int index, int player )
	{
		if ( player == 1 )
		{
			inventorySpriteP1[index].sprite = _sprite;
		}
		else if ( player == 2 )
		{
			inventorySpriteP1[index].sprite = _sprite;
        }
	}
}
