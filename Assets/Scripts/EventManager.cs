using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EventManager : MonoBehaviour
{
		public List<Ship> players = new List<Ship> ();
		public Material redMat;
		public Material blueMat;
	
		public void playerDied (int dead_player) //red = 1 | blue = 2
		{
				foreach (Ship player in players) {
						if (player.GetPlayerNumber () != dead_player && !player.isRespawning ()) 
								player.dead_player = dead_player;
				}
		}
	
		public void stationCaptured (Color col)
		{
				string capturedColor;
				if (col.Equals (redMat.color))
						capturedColor = "Red";
				else
						capturedColor = "Blue";
						
				foreach (Ship player in players) {
						if (!player.isRespawning () && !player.outOfBounds) 
								player.stationCaptured = capturedColor;
				}
		}
}
