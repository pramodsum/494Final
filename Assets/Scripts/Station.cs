using UnityEngine;
using System.Collections;

public class Station : MonoBehaviour
{
		public Station_Control station;
		void OnTriggerEnter (Collider other)
		{
				if ((other.gameObject.name == "Shot") || ((other.gameObject.name == "Missile")) /*&& !station.inControl*/) {
						this.renderer.material.color = other.renderer.material.color; 
				}
		}
}
