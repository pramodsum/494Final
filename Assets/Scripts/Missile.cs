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
				if (team == 1) {
						renderer.material.color = new Color (0.9F, 0.0F, 0.0F);
				} else {
						renderer.material.color = new Color (0.0F, 0.0F, 0.9F);
				}
		}

		void CollideWithShip (GameObject ship)
		{
				// Don't collide with people of the same team as me.
				// The code below could definitely be cleaned up and I am so sorry
				if (team == 1) {
						if ((ship.name == "Ship1") || (ship.name == "Ship3")) {
								return;
						}
				} else {
						if ((ship.name == "Ship2") || (ship.name == "Ship4")) {
								return;
						}
				}

				var otherPos = ship.transform.position;
				var knockDirection = (transform.position - otherPos).normalized;
				int shooterNum = shooter.GetComponent<Ship> ().GetPlayerNumber ();
				ship.GetComponent<Ship> ().Damage (10, shooterNum);
				Instantiate (explosion, otherPos, Quaternion.identity);
				Destroy (gameObject);
				ship.rigidbody.AddForce (knockDirection * 100f);
		}
}
