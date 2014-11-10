using UnityEngine;
using System.Collections;

public class Highlight : MonoBehaviour
{
		public Ship thisShip;
		public Camera thisCam;
		private Texture2D tex;
		
		// Update is called once per frame
		void Start ()
		{
				tex = new Texture2D (1, 1);
				tex.SetPixel (0, 0, new Color (1F, 1F, 0.0F, 0.6F));
				tex.Apply ();
		}
		
		void OnGUI ()
		{
	
				Ship[] ships = FindObjectsOfType (typeof(Ship)) as Ship[];
				foreach (Ship s in ships) {
						if (s != thisShip) {
								Vector3 vec = thisCam.WorldToViewportPoint (s.transform.position);
								if (vec.x >= 0 && vec.x <= 1) {
										if (vec.y >= 0 && vec.y <= 1) {
												if (vec.z >= 0) {
														var coords = thisCam.ViewportToScreenPoint (vec);
														float sqwidth = 500f / coords.z;
														GUI.DrawTexture (new Rect (coords.x - sqwidth / 2, Screen.height - coords.y - sqwidth / 2, sqwidth, sqwidth), tex);
												}
										}
								}
								
						}
				}		
		}
}
