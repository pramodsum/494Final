using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
				GameObject.Find ("Credits").GetComponent<CanvasGroup> ().alpha = 0;
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
				GameObject.Find ("Credits").GetComponent<CanvasGroup> ().alpha = 1;
				GameObject.Find ("MainMenu").GetComponent<CanvasGroup> ().alpha = 0;
		}
	
		public void ShowMainMenu ()
		{
				GameObject.Find ("MainMenu").GetComponent<CanvasGroup> ().alpha = 1;
				GameObject.Find ("Credits").GetComponent<CanvasGroup> ().alpha = 0;
		}
}
