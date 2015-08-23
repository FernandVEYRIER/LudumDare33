using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
