using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	/*private GameObject player;
	private GameObject monster;*/

	public GameObject canvasPause;
	public GameObject canvasPauseButton;
	public Slider canvasPauseSound;
	public Text countdownText;
	public Text countdownP1;
	public Text countdownP2;
	public EventSystem es;


	private bool isResumingGame;
	private float resumeDelay = 0;
	private float currentElapsedTime;

	// Use this for initialization
	void Start () 
	{
		canvasPauseSound.value = PlayerPrefs.GetFloat( "MasterVolume", 1 );
		canvasPause.SetActive( false );

		Time.timeScale = 0;
		currentElapsedTime = Time.realtimeSinceStartup + resumeDelay;
        isResumingGame = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
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
		if ( value < 0 )
			value = 0;
		if ( player == 1 )
		{
			countdownP1.text = value.ToString();
		}
		else if ( player == 2 )
		{
			countdownP2.text = value.ToString();
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
		string playerName = PlayerPrefs.GetString( "Player" + playerID, "Player " + playerID );
		if ( playerName == "" )
			playerName = "Player " + playerID;
		if ( winner.GetType() == typeof(MonsterController) )
		{
			Debug.Log("Monster " + playerName + " wins !" );
		}
		else
		{
			Debug.Log("Human " + playerName + " wins !" );
        }
	}
}
