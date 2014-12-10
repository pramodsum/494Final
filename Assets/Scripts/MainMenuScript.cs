using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{
		public GameObject MainMenu;
		public GameObject Credits;

		// Use this for initialization
		void Start ()
		{
				Credits.SetActive (false);
		}
		
		public void StartTwoPlayer ()
		{
				Application.LoadLevel ("_Scene_2_Duncan_CTF 2 Players");
		}
	
		public void StartFourPlayer ()
		{
				Application.LoadLevel ("_Scene_2_Duncan_CTF 1");
		}
	
		public void ShowCredits ()
		{
				Credits.SetActive (true);
				MainMenu.SetActive (false);
		}
	
		public void ShowMainMenu ()
		{
				Credits.SetActive (false);
				MainMenu.SetActive (true);
		}
}
