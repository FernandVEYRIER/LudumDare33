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
	public GameObject [] buttonsP1;
	public GameObject [] buttonsP2;
	[Header("Pannels")]
	public GameObject settingsPannel;
	public GameObject creditsPannel;
	public GameObject keyBindsPannel;
	public InputField [] playerName;
	public Slider soundVolume;

	private int currentButtonSelected;

	private bool isBindingKey;
	private GameObject keyClicked;
	private GameObject lastButtonSelected;

	void Start()
	{
		currentButtonSelected = 0;
		ChangeSelectedButton( currentButtonSelected );
		settingsPannel.SetActive( false );
		creditsPannel.SetActive( false );
		keyBindsPannel.SetActive( true );

		soundVolume.value = PlayerPrefs.GetFloat( "MasterVolume", 1 );
		playerName[0].text = PlayerPrefs.GetString( "Player1", "Player1" );
		playerName[1].text = PlayerPrefs.GetString( "Player2", "Player2" );

		// Initialise tous les boutons en fonction des binds actuels
		// Ceux du P1
		buttonsP1[0].GetComponentInChildren<Text>().text = CustomInput.GetInput( "Horizontal", false );
		buttonsP1[0].GetComponentInChildren<Button>().interactable = CustomInput.isPlayer1UsingKeyboard;
		buttonsP1[1].GetComponentInChildren<Text>().text = CustomInput.GetInput( "Horizontal", true );
		buttonsP1[1].GetComponentInChildren<Button>().interactable = CustomInput.isPlayer1UsingKeyboard;
		buttonsP1[2].GetComponentInChildren<Text>().text = CustomInput.GetInput( "Jump", true );
		buttonsP1[3].GetComponentInChildren<Text>().text = CustomInput.GetInput( "Fire1", true );
		buttonsP1[4].GetComponentInChildren<Text>().text = CustomInput.GetInput( "item_1", true );
		buttonsP1[5].GetComponentInChildren<Text>().text = CustomInput.GetInput( "item_2", true );
		buttonsP1[6].GetComponentInChildren<Text>().text = CustomInput.GetInput( "item_3", true );
		buttonsP1[7].GetComponentInChildren<Toggle>().isOn = CustomInput.isPlayer1UsingKeyboard;

		// Ceux du P2
		buttonsP2[0].GetComponentInChildren<Text>().text = CustomInput.GetInput( "HorizontalAlt", false );
		buttonsP2[0].GetComponentInChildren<Button>().interactable = CustomInput.isPlayer2UsingKeyboard;
		buttonsP2[1].GetComponentInChildren<Text>().text = CustomInput.GetInput( "HorizontalAlt", true );
		buttonsP2[1].GetComponentInChildren<Button>().interactable = CustomInput.isPlayer2UsingKeyboard;
		buttonsP2[2].GetComponentInChildren<Text>().text = CustomInput.GetInput( "JumpAlt", true );
		buttonsP2[3].GetComponentInChildren<Text>().text = CustomInput.GetInput( "Fire1Alt", true );
		buttonsP2[4].GetComponentInChildren<Text>().text = CustomInput.GetInput( "item_1Alt", true );
		buttonsP2[5].GetComponentInChildren<Text>().text = CustomInput.GetInput( "item_2Alt", true );
		buttonsP2[6].GetComponentInChildren<Text>().text = CustomInput.GetInput( "item_3Alt", true );
		buttonsP2[7].GetComponentInChildren<Toggle>().isOn = CustomInput.isPlayer2UsingKeyboard;
	}

	void Update()
	{
		if ( keyClicked != null && isBindingKey )
		{
			foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
			{
				if (Input.GetKeyDown(kcode))
				{
					if ( keyClicked != null )
					{
						keyClicked.transform.GetChild(0).GetComponent<Text>().text = kcode.ToString();
						switch ( keyClicked.name )
						{
							case "ButtonLeft" :
								CustomInput.AddImput( "Horizontal", CustomInput.GetInput( "Horizontal", true ), kcode.ToString() );
								break;
							case "ButtonRight" :
								CustomInput.AddImput( "Horizontal", kcode.ToString(), CustomInput.GetInput( "Horizontal", false ) );
								break;
							case "ButtonJump" :
								CustomInput.AddImput( "Jump", kcode.ToString(), "" );
								break;
							case "ButtonAttack" :
								CustomInput.AddImput( "Fire1", kcode.ToString(), "" );
								break;
							case "ButtonItem1" :
								CustomInput.AddImput( "item_1", kcode.ToString(), "" );
								break;
							case "ButtonItem2" :
								CustomInput.AddImput( "item_2", kcode.ToString(), "" );
								break;
							case "ButtonItem3" :
								CustomInput.AddImput( "item_3", kcode.ToString(), "" );
								break;
							case "ButtonLeftAlt" :
							CustomInput.AddImput( "HorizontalAlt", CustomInput.GetInput( "HorizontalAlt", true ), kcode.ToString() );
								break;
							case "ButtonRightAlt" :
								CustomInput.AddImput( "HorizontalAlt", kcode.ToString(), CustomInput.GetInput( "HorizontalAlt", false ) );
								break;
							case "ButtonJumpAlt" :
								CustomInput.AddImput( "JumpAlt", kcode.ToString(), "" );
								break;
							case "ButtonAttackAlt" :
								CustomInput.AddImput( "Fire1Alt", kcode.ToString(), "" );
								break;
							case "ButtonItem1Alt" :
								CustomInput.AddImput( "item_1Alt", kcode.ToString(), "" );
								break;
							case "ButtonItem2Alt" :
								CustomInput.AddImput( "item_2Alt", kcode.ToString(), "" );
								break;
							case "ButtonItem3Alt" :
								CustomInput.AddImput( "item_3Alt", kcode.ToString(), "" );
								break;
						}
						keyClicked = null;
						isBindingKey = false;
						es.SetSelectedGameObject( lastButtonSelected );
					}
				}
			}
		}
		else if ( keyClicked != null && !isBindingKey )
			isBindingKey = true;
	}

	public void SetPlayerUsingController( GameObject toggleObj )
	{
		if ( toggleObj.name == "ToggleP1" )
			CustomInput.isPlayer1UsingKeyboard = toggleObj.GetComponent<Toggle>().isOn;
		if ( toggleObj.name == "ToggleP2" )
			CustomInput.isPlayer2UsingKeyboard = toggleObj.GetComponent<Toggle>().isOn;
		CustomInput.SaveInput();
	}

	public void ChangeBinding( GameObject keyObj )
	{
		keyClicked = keyObj;
		isBindingKey = false;
		lastButtonSelected = es.currentSelectedGameObject;
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
			keyBindsPannel.SetActive( true );
		}
		else
		{
			settingsPannel.SetActive( true );
			keyBindsPannel.SetActive( false );
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
		#if UNITY_WEBPLAYER
			Application.ExternalEval( "window.close()" );
		#else
			Application.Quit();
		#endif
	}
}
