using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
		
	
		}
		
		public void StartTwoPlayer ()
		{
				Application.LoadLevel ("_Scene_2_Duncan_CTF 2 Players");
		}
	
		public void StartFourPlayer ()
		{
				Application.LoadLevel ("_Scene_2_Duncan_CTF 1");
		}
}
