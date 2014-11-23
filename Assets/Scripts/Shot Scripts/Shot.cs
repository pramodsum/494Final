using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour
{
		public GameObject explosion;

		private Vector3 direction;
		private Vector3 velocity;

		public float maxDist = 1200f;

		private Vector3 startPosition;
		private GameObject shooter;
		private GameObject target;

		public float lockOnDistance = 100;


		public bool homing;

		void Start ()
		{
				startPosition = transform.position; 
				maxDist = 1200f;
		}
	
		void Update ()
		{
				if (Vector3.Distance (transform.position, startPosition) > maxDist)
						GameObject.Destroy (gameObject);
		}

		void SetShooter (GameObject s)
		{
				shooter = s;
		}
		void SetTarget (GameObject s)
		{
				target = s;
		}

		void CollideWithShip (GameObject ship)
		{
				if (shooter == ship) {
						return;
				}
				int ship1Num = shooter.GetComponent<Ship> ().GetPlayerNumber ();
				int ship2Num = ship.GetComponent<Ship> ().GetPlayerNumber ();
				if ((ship1Num == 1 && ship2Num == 3) ||
						(ship1Num == 3 && ship2Num == 1) ||
						(ship1Num == 2 && ship2Num == 4) ||
						(ship1Num == 4 && ship2Num == 2))
						return;

				var otherPos = ship.transform.position;
				var knockDirection = (transform.position - otherPos).normalized;
				ship.SendMessage ("Damage");
				Instantiate (explosion, otherPos, Quaternion.identity);
				Destroy (gameObject);
				ship.rigidbody.AddForce (knockDirection * 100f);
		}
}
