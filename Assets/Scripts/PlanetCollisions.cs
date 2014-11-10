using UnityEngine;
using System.Collections;

public class PlanetCollisions : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnCollisionEnter (Collision other)
		{
				if (other.gameObject.tag == "PlayerShip") {
						other.gameObject.GetComponent<Ship> ().Damage ();
						other.gameObject.GetComponent<Ship> ().Damage ();
				}
		}
}
