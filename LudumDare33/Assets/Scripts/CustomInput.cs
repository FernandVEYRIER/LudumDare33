using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Custom input.
/// Cette classe remplace Input de Unity, qui ne permet pas de bind des touches en jeu
/// </summary>
public static class CustomInput {

	private static Dictionary<string, MyKeys> keyMap = new Dictionary<string, MyKeys>();

	// Construteur statique qui initialise les touches
	// essaye de load une sauvegarde, met des par défaut le cas échéant
	static CustomInput()
	{
		// Fix : à remplacer par des playerprefs
		string[] str = new string[2];

		// Player 1
		str = PlayerPrefs.GetString( "Horizontal", "D|Q" ).Split('|');
		keyMap.Add ( "Horizontal", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
		str = PlayerPrefs.GetString( "Jump", "Space|" ).Split('|');
		keyMap.Add ( "Jump", new MyKeys(ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1])));
		str = PlayerPrefs.GetString( "Fire1", "Mouse0|" ).Split('|');
		keyMap.Add ( "Fire1", new MyKeys(ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1])) );
		str = PlayerPrefs.GetString( "Item1", "Alpha1|" ).Split('|');
		keyMap.Add ( "Item1", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
		str = PlayerPrefs.GetString( "Item2", "Alpha2|" ).Split('|');
		keyMap.Add ( "Item2", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
		str = PlayerPrefs.GetString( "Item3", "Alpha3|" ).Split('|');
		keyMap.Add ( "Item3", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );

		// Player 2
		str = PlayerPrefs.GetString( "HorizontalAlt", "RightArrow|LeftArrow" ).Split('|');
		keyMap.Add ( "HorizontalAlt", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
		str = PlayerPrefs.GetString( "JumpAlt", "LeftShift|" ).Split('|');
		keyMap.Add ( "JumpAlt", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
		str = PlayerPrefs.GetString( "Fire1Alt", "Keypad0|" ).Split('|');
		keyMap.Add ( "Fire1Alt", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
		str = PlayerPrefs.GetString( "Item1Alt", "Keypad1|" ).Split('|');
		keyMap.Add ( "Item1Alt", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
		str = PlayerPrefs.GetString( "Item2Alt", "Keypad2|" ).Split('|');
		keyMap.Add ( "Item2Alt", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
		str = PlayerPrefs.GetString( "Item3Alt", "Keypad3|" ).Split('|');
		keyMap.Add ( "Item3Alt", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
	}

	public static KeyCode ConvertStringToKeycode( string keyCode )
	{
		if ( keyCode == "" )
			return KeyCode.None;
		return ( (KeyCode) System.Enum.Parse( typeof(KeyCode), keyCode ) );
	}

	// sauvegarde la config dans un fichier
	public static void SaveInput()
	{
		foreach ( var obj in keyMap )
		{
			PlayerPrefs.SetString( obj.Key, obj.Value.positiveKey + "|" + obj.Value.negativeKey );
			Debug.Log( "Saved : " + obj.Key + "=" + obj.Value.positiveKey + "|" + obj.Value.negativeKey );
		}
	}

	/// <summary>
	/// Gets the input by its name.
	/// </summary>
	/// <param name="inputName">Input name.</param>
	/// <param name="isPositiveValue">If set to <c>true</c> returns positive value for the input, otherwise negative.</param>
	public static string GetInput( string inputName, bool isPositiveValue = true )
	{
		if ( !keyMap.ContainsKey( inputName ) )
		{
			return "None";
		}
		if ( isPositiveValue )
			return keyMap[inputName].positiveKey.ToString();
		else
			return keyMap[inputName].negativeKey.ToString();
	}

	public static void AddImput( string inputName, string posVal, string negVal )
	{
		//KeyCode thisKeyCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), "Whatever") ;
		if ( keyMap.ContainsKey( inputName ) )
		{
			keyMap[inputName].positiveKey = (KeyCode) System.Enum.Parse( typeof(KeyCode), posVal );
			keyMap[inputName].negativeKey = (KeyCode) System.Enum.Parse( typeof(KeyCode), negVal );
			SaveInput();
		}
		else
		{
			Debug.LogWarning( "Adding new keys is not supported yet." );
			//keyMap.Add ( inputName, new MyKeys((KeyCode) System.Enum.Parse( typeof(KeyCode), posVal ),
			//                                   (KeyCode) System.Enum.Parse( typeof(KeyCode), negVal )));
		}
	}

	public static float GetAxis( string axisName )
	{
		if ( !keyMap.ContainsKey(axisName) )
		{
			Debug.LogWarning( axisName + " is not defined." );
			return 0;
		}
		if ( keyMap[ axisName ].positiveKey != KeyCode.None && Input.GetKey( keyMap[ axisName ].positiveKey ) )
		{
			return 1;
		}
		else if ( keyMap[ axisName ].negativeKey != KeyCode.None && Input.GetKey( keyMap[ axisName ].negativeKey ) )
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

		public MyKeys( KeyCode keyPos, KeyCode keyNeg )
		{
			positiveKey = keyPos;
			negativeKey = keyNeg;
		}
	}
}
