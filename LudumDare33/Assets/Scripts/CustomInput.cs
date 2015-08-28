using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Custom input.
/// Cette classe remplace Input de Unity, qui ne permet pas de bind des touches en jeu
/// </summary>
using System;


public static class CustomInput {

	public static bool isPlayer1UsingKeyboard;
	public static bool isPlayer2UsingKeyboard;

	private static Dictionary<string, MyKeys> keyMap = new Dictionary<string, MyKeys>();

	// Construteur statique qui initialise les touches
	// essaye de load une sauvegarde, met des par défaut le cas échéant
	static CustomInput()
	{
		string[] str = new string[2];

		// Player 1
		str = PlayerPrefs.GetString( "Horizontal", "D|Q" ).Split('|');
		keyMap.Add ( "Horizontal", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
		str = PlayerPrefs.GetString( "Jump", "Space|" ).Split('|');
		keyMap.Add ( "Jump", new MyKeys(ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1])));
		str = PlayerPrefs.GetString( "Fire1", "Mouse0|" ).Split('|');
		keyMap.Add ( "Fire1", new MyKeys(ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1])) );
		str = PlayerPrefs.GetString( "item_1", "Alpha1|" ).Split('|');
		keyMap.Add ( "item_1", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
		str = PlayerPrefs.GetString( "item_2", "Alpha2|" ).Split('|');
		keyMap.Add ( "item_2", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
		str = PlayerPrefs.GetString( "item_3", "Alpha3|" ).Split('|');
		keyMap.Add ( "item_3", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
		str = PlayerPrefs.GetString( "Dash", "A|" ).Split('|');
		keyMap.Add ( "Dash", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
		isPlayer1UsingKeyboard = Convert.ToBoolean( PlayerPrefs.GetInt( "isPlayer1UsingKeyboard", 0 ) );

		// Player 2
		str = PlayerPrefs.GetString( "HorizontalAlt", "RightArrow|LeftArrow" ).Split('|');
		keyMap.Add ( "HorizontalAlt", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
		str = PlayerPrefs.GetString( "JumpAlt", "LeftShift|" ).Split('|');
		keyMap.Add ( "JumpAlt", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
		str = PlayerPrefs.GetString( "Fire1Alt", "Keypad0|" ).Split('|');
		keyMap.Add ( "Fire1Alt", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
		str = PlayerPrefs.GetString( "item_1Alt", "Keypad1|" ).Split('|');
		keyMap.Add ( "item_1Alt", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
		str = PlayerPrefs.GetString( "item_2Alt", "Keypad2|" ).Split('|');
		keyMap.Add ( "item_2Alt", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
		str = PlayerPrefs.GetString( "item_3Alt", "Keypad3|" ).Split('|');
		keyMap.Add ( "item_3Alt", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
		str = PlayerPrefs.GetString( "DashAlt", "LeftShift|" ).Split('|');
		keyMap.Add ( "DashAlt", new MyKeys( ConvertStringToKeycode(str[0]), ConvertStringToKeycode(str[1]) ) );
		isPlayer2UsingKeyboard = Convert.ToBoolean( PlayerPrefs.GetInt( "isPlayer2UsingKeyboard", 0 ) );
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
			//Debug.Log( "Saved : " + obj.Key + "=" + obj.Value.positiveKey + "|" + obj.Value.negativeKey );
		}
		PlayerPrefs.SetInt( "isPlayer1UsingKeyboard", Convert.ToInt32(isPlayer1UsingKeyboard) );
		PlayerPrefs.SetInt( "isPlayer2UsingKeyboard", Convert.ToInt32(isPlayer2UsingKeyboard) );
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
		if ( keyMap.ContainsKey( inputName ) )
		{
			if ( posVal != "" )
				keyMap[inputName].positiveKey = (KeyCode) System.Enum.Parse( typeof(KeyCode), posVal );
			else
				keyMap[inputName].positiveKey = KeyCode.None;
			if ( negVal != "" )
				keyMap[inputName].negativeKey = (KeyCode) System.Enum.Parse( typeof(KeyCode), negVal );
			else
				keyMap[inputName].negativeKey = KeyCode.None;
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
		// Si l'input demandé n'existe pas
		if ( !keyMap.ContainsKey(axisName) )
		{
			Debug.LogWarning( axisName + " is not defined." );
			return 0;
		}

		// Cas particulier pour les axes horizontaux, qui ne sont pas des bouttons dans le cas des manettes
		if ( (axisName == "Horizontal" && !isPlayer1UsingKeyboard) || (axisName == "HorizontalAlt" && !isPlayer2UsingKeyboard) )
		{
			if ( Input.GetAxis( "HorizontalAlt" ) > 0 )
				return 1;
			else if ( Input.GetAxis( "HorizontalAlt" ) < 0 )
				return -1;
			else
				return 0;
		}

		// Sinon on vérifie avec les occurences du tableau si le bouton est actuellement utilisé
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
