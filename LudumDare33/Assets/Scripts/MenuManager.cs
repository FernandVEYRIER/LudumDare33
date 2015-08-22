using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour {

	public EventSystem es;
	public GameObject [] buttons;

	private int currentButtonSelected;

	void Start()
	{
		currentButtonSelected = 0;
		ChangeSelectedButton( currentButtonSelected );
	}

	public void ChangeSelectedButton( int id )
	{
		es.SetSelectedGameObject( buttons[id] );
	}

	public void LoadLevel( int level )
	{
		Application.LoadLevel( level );
	}

	public void Quit()
	{
		Application.Quit();
	}
}
