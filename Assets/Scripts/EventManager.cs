using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EventManager : MonoBehaviour
{
		public List<Ship> players;
		public Material redMat;
		public Material blueMat;
	
		public void playerDied (string killer, int dead_player) //red = 1 | blue = 2
		{
				//reformat killer string
				string killerStr = "";
				if (killer [killer.Length - 1].ToString () == dead_player.ToString ()) {
						killerStr = "Bounds";
				} else if (killer.Contains ("Ship")) {
						killerStr = "Player " + killer [killer.Length - 1];
				} else if (killerStr.Contains ("omega") || killerStr.Contains ("AI")) {
						killerStr = "Fighter";
				} else if (killerStr.Contains ("Transport")) {
						killerStr = "Transport";
				} else if (killerStr.Contains ("Station")) {
						killerStr = "Station";
				} else {
						killerStr = killer;
				}
				
				foreach (Ship player in players) {
						if (player.GetPlayerNumber () != dead_player) {
								player.dead_player = dead_player;
								player.killer = killerStr;
						}
				}
		}
		
		public void transportArrived ()
		{
				foreach (Ship player in players) {
						player.transportArrived = true;
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
						player.stationCaptured = capturedColor;
				}
		}
}
