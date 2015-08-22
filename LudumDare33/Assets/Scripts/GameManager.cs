using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

	public GameObject canvasPause;
	public GameObject canvasPauseButton;
	public EventSystem es;

	// Use this for initialization
	void Start () 
	{
		canvasPause.SetActive( false );
	}
	
	// Update is called once per frame
	void Update ()
	{
		if ( Input.GetKeyDown( KeyCode.Joystick1Button9 ) || Input.GetKeyDown( KeyCode.Escape ) )
		{
			SetPause();
		}
	}

	public void SetPause()
	{
		if ( canvasPause.activeSelf )
		{
			canvasPause.SetActive( false );
		}
		else 
		{
			canvasPause.SetActive( true );
			es.SetSelectedGameObject( canvasPauseButton );
		}
	}
}
