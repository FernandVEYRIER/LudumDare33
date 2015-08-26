using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomInput : MonoBehaviour {

	Dictionary<string, MyKeys> keyMap = new Dictionary<string, MyKeys>();

	void InitInput()
	{
		//keyMap.Add ( "Player1InputHorizontal", "Horizontal" );
	}

	void SaveInput()
	{
		for ( int i = 0; i < 8; i++ )
		{
			PlayerPrefs.SetString("Player1InputHorizontal" + i, "test");

		}
	}

	public float GetAxis( string axisName )
	{
		if ( Input.GetKey( keyMap[ axisName ].positiveKey ) )
		{
			return 1;
		}
		else if ( Input.GetKey( keyMap[ axisName ].negativeKey ) )
		{
			return -1;
		}
		else
			return 0;
	}

	public class MyKeys
	{
		public KeyCode positiveKey;
		public KeyCode negativeKey;
	}
}
