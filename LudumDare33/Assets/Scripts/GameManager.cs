﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	/*private GameObject player;
	private GameObject monster;*/

	[Header("Canvas")]
	public GameObject canvasPause;
	public GameObject canvasPauseButton;
	public Slider canvasPauseSound;
	[Header("Player")]
	public Text countdownText;
	public Text countdownP1;
	public Text countdownP2;
	public Text nameP1;
	public Text nameP2;
	public Image [] inventorySpriteP1;
	public Image [] inventorySpriteP2;
	[Header("")]
	public EventSystem es;
	
	private bool isResumingGame;
	private float resumeDelay = 0;
	private float currentElapsedTime;


	void Start () 
	{
		canvasPauseSound.value = PlayerPrefs.GetFloat( "MasterVolume", 1 );
		canvasPause.SetActive( false );

		Time.timeScale = 0;
		currentElapsedTime = Time.realtimeSinceStartup + resumeDelay;
        isResumingGame = true;

		nameP1.text = PlayerPrefs.GetString("Player1", "Player1");
		nameP2.text = PlayerPrefs.GetString("Player2", "Player2");
		if ( nameP1.text == "" )
			nameP1.text = "Player 1";
		if ( nameP2.text == "" )
			nameP2.text = "Player 2";
	}
	

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
