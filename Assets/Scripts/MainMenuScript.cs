﻿using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		Rect playButton = new Rect (Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50);
		if ( GUI.Button(playButton, "Play - 4 Players") )
		{
			Application.LoadLevel("_Scene_2_Duncan_CTF 1");
		}
		Rect playButton2 = new Rect (Screen.width / 2 - 50, Screen.height / 2 - 25 + 100, 100, 50);
		if ( GUI.Button(playButton2, "Play - 2 Players") )
		{
			Application.LoadLevel("_Scene_2_Duncan_CTF 2 Players");
		}
	}
}
