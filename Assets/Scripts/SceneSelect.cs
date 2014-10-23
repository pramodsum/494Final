using UnityEngine;
using System.Collections;

public class SceneSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		float groupWidth = 120;
		float groupHeight = 150;
		float screenWidth = Screen.width;
		float screenHeight = Screen.height;
		float groupX = (screenWidth - groupWidth) / 2;
		float groupY = (screenHeight - groupHeight) / 2;

		if (Application.loadedLevel == 0) 
		{
			GUI.BeginGroup (new Rect (groupX, groupY, groupWidth, groupHeight));
			GUI.Box (new Rect (0, 0, groupWidth, groupHeight), "");
			if (GUI.Button (new Rect (10, 30, 100, 30), "Play")) 
				Application.LoadLevel (1);
			if (GUI.Button (new Rect (10, 70, 100, 30), "Controls")) 
				Application.LoadLevel (2);
			if (GUI.Button (new Rect (10, 110, 100, 30), "Credits")) 
				Application.LoadLevel (3);
			GUI.EndGroup ();
		}

		if (Application.loadedLevel == 1) //play
		{
//			GUI.BeginGroup (new Rect (0, 0, groupWidth, groupHeight));
//			GUI.Box (new Rect (0, 0, groupWidth, groupHeight), "");
			if (GUI.Button (new Rect (0, 0, 100, 30), "Back")) 
				Application.LoadLevel (0);//main menu
//			GUI.EndGroup ();
		}

		if (Application.loadedLevel == 2) //controls
		{
			if (GUI.Button (new Rect (0, 0, 100, 30), "Back")) 
				Application.LoadLevel (0);//main menu

//			GUIStyle centeredTextStyle = new GUIStyle("label");
//			centeredTextStyle.alignment = TextAnchor.MiddleCenter;
//			GUI.Label(new Rect(0,100,100,100), "The text",centeredTextStyle);
		}

		if (Application.loadedLevel == 3) //credit
		{
			if (GUI.Button (new Rect (0, 0, 100, 30), "Back")) 
				Application.LoadLevel (0);//main menu
		}
	}
}
