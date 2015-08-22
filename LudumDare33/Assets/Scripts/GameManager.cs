﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject canvasPause;
	public GameObject canvasPauseButton;
	public Slider canvasPauseSound;
	public EventSystem es;


	private bool isResumingGame;
	private float resumeDelay = 3;
	private float currentElapsedTime;

	// Use this for initialization
	void Start () 
	{
		canvasPauseSound.value = PlayerPrefs.GetFloat( "MasterVolume", 1 );
		canvasPause.SetActive( false );
		isResumingGame = false;
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
					Debug.Log("3");
				else if ( currentElapsedTime - Time.realtimeSinceStartup > 1 )
					Debug.Log("2");
				else
					Debug.Log("1");
			}
			else
			{
				isResumingGame = false;
				Time.timeScale = 1;
			}
		}
	}

	public void SetPause()
	{
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

	public void LoadLevel( int level )
	{
		Time.timeScale = 1;
		Application.LoadLevel( level );
	}
}