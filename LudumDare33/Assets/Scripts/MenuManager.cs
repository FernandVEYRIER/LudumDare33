using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
	
	public void LoadLevel( int level )
	{
		Application.LoadLevel( level );
	}

	public void Quit()
	{
		Application.Quit();
	}
}
