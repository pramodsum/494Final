using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
		public GameObject explosion;
		private Ship shooter;
		private int team;
		private float birthTime;

		void Start ()
		{
				birthTime = Time.time;
		}

		void Update ()
		{
				if (shooter == null) {
						return;
				}

				if (age () > 3f) {
						Destroy (gameObject);
						return;
				}

				//transform.LookAt (targetShip ().transform.position);

				// rigidbody.AddForce (transform.forward * 0.001f);
		}

		protected Ship targetShip ()
		{
				Ship bestShip = shooter;
				float bestDistance = Mathf.Infinity;
				foreach (Ship ship in Ship.FindAll()) {
						if (ship.team != team) {
								var distance = Vector3.Distance (transform.position, ship.transform.position);
								if (distance < bestDistance) {
										bestShip = ship;
										bestDistance = distance;
								}
						}
				}
				return bestShip;
		}

		protected float age ()
		{
				return Time.time - birthTime;
		}

		void SetShooter (Ship s)
		{
				shooter = s;
				team = shooter.team;
		}

		void CollideWithShip (Ship ship)
		{
				if (ship.team == team) {
						return;
				}

				var otherPos = ship.transform.position;
				var knockDirection = (transform.position - otherPos).normalized;
				int shooterNum = shooter.GetComponent<Ship> ().GetPlayerNumber ();
				ship.GetComponent<Ship> ().Damage (5, shooterNum);
				Instantiate (explosion, otherPos, Quaternion.identity);
				Destroy (gameObject);
				ship.rigidbody.AddForce (knockDirection * 100f);
		}
}
