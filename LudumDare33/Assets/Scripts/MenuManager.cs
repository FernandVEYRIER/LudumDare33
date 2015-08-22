using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour {

	[Header("Event System Handler")]
	public EventSystem es;
	[Header("Menu Buttons")]
	public GameObject [] buttons;
	[Header("Pannels")]
	public GameObject settingsPannel;

	private int currentButtonSelected;

	void Start()
	{
		currentButtonSelected = 0;
		ChangeSelectedButton( currentButtonSelected );
		settingsPannel.SetActive( false );
	}

	public void ChangeSelectedButton( int id )
	{
		es.SetSelectedGameObject( buttons[id] );
	}

	public void LoadLevel( int level )
	{
		Application.LoadLevel( level );
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
	}

	public void Quit()
	{
		Application.Quit();
	}
}
