using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class MenuManager : MonoBehaviour {

	[Header("Event System Handler")]
	public EventSystem es;
	[Header("Menu Buttons")]
	public GameObject [] buttons;
	[Header("Pannels")]
	public GameObject settingsPannel;
	public GameObject creditsPannel;
	public InputField [] playerName;
	public Slider soundVolume;

	private int currentButtonSelected;

	private bool isBindingKey;
	private GameObject keyClicked;

	void Start()
	{
		currentButtonSelected = 0;
		ChangeSelectedButton( currentButtonSelected );
		settingsPannel.SetActive( false );
		creditsPannel.SetActive( false );

		soundVolume.value = PlayerPrefs.GetFloat( "MasterVolume", 1 );
		playerName[0].text = PlayerPrefs.GetString( "Player1", "Player1" );
		playerName[1].text = PlayerPrefs.GetString( "Player2", "Player2" );
	}

	void Update()
	{
		Debug.Log( CustomInput.GetAxis( "HorizontalAlt" ) );
		if ( keyClicked != null && isBindingKey )
		{
			foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
			{
				if (Input.GetKeyDown(kcode))
				{
					if ( keyClicked != null )
					{
						keyClicked.transform.GetChild(0).GetComponent<Text>().text = kcode.ToString();
						// TODO : mettre le bon axe
						CustomInput.AddImput( "Horizontal", kcode.ToString(), kcode.ToString() );
						keyClicked = null;
						isBindingKey = false;
						es.SetSelectedGameObject( es.firstSelectedGameObject );
					}
				}
			}
		}
		else if ( keyClicked != null && !isBindingKey )
			isBindingKey = true;
	}

	public void ChangeBinding( GameObject keyObj )
	{
		keyClicked = keyObj;
		isBindingKey = false;
		es.SetSelectedGameObject( null );
	}

	public void ChangeSelectedButton( int id )
	{
		es.SetSelectedGameObject( buttons[id] );
	}

	public void LoadLevel( int level )
	{
		Application.LoadLevel( level );
	}

	public void DisplayCredits()
	{
		if ( creditsPannel.activeSelf )
			creditsPannel.SetActive( false );
		else
			creditsPannel.SetActive( true );
	}

	public void DisplaySettings()
	{
		if ( settingsPannel.activeSelf )
		{
			settingsPannel.SetActive( false );
		}
		else
		{
			settingsPannel.SetActive( true );
		}
	}

	public void ApplySettings()
	{
		es.SetSelectedGameObject( buttons[1] );
		DisplaySettings();

		PlayerPrefs.SetFloat( "MasterVolume", soundVolume.value );
		PlayerPrefs.SetString( "Player1", playerName[0].text );
		PlayerPrefs.SetString( "Player2", playerName[1].text );
	}

	public void Quit()
	{
		Application.Quit();
	}
}
