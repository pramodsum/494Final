using UnityEngine;
//using UnityEditor;
using System.Collections;

public class BoundsScript : MonoBehaviour
{
		float distFromCenter;
		Vector3 center;
		
		void FixedUpdate ()
		{
				distFromCenter = Vector3.Distance (transform.position, center);
				
				if (distFromCenter >= 200) {
						Debug.Log (name + " is out of bounds @ " + distFromCenter + "f from center!!!!");
						GetComponent<Ship> ().outOfBounds = true;
						ReboundShipOffWalls ();
				} else {
						GetComponent<Ship> ().outOfBounds = false;
				}
		}
		
		void ReboundShipOffWalls ()
		{
				rigidbody.velocity = Vector3.Reflect (rigidbody.velocity, (center - transform.position).normalized);
				Vector3 f = new Vector3 (Random.value, Random.value, Random.value) * 40;
				rigidbody.AddForce (f);
		}
    
		//TODO: Rotate ship away from wall 
		void TurnShipAround ()
		{
		}
}
