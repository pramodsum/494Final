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
						Debug.Log ("Ship: " + other.gameObject.transform.position);
						other.gameObject.GetComponent<Ship> ().BounceBack ();
						Debug.Log ("Ship: " + other.gameObject.transform.position);
				}
		}
}
