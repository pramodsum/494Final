using UnityEngine;
using System.Collections;

public class CTF_Script : MonoBehaviour
{
 
		public GameObject _cargo;
		public GameObject arrow;

		public Cargo_Script cargo;

		public float timeTilCargo;
		public float cargoWait = 1;
		private bool noNewCargo;

		public Transform ship1;
		public Transform ship2;

		public Transform p1Home;
		public Transform p2Home;

		public int p1Score;
		public int p2Score;

		public ArrowScript arrow1;
		public ArrowScript arrow2;

		System.Random rand;


		// Use this for initialization 
		void Start ()
		{
				p1Score = 0;
				p2Score = 0;
				timeTilCargo = cargoWait;
				noNewCargo = false;
				rand = new System.Random ((int)Time.time);

				Vector3 newPos = p1Home.position;
				newPos.y += 20;
				ship1.position = newPos;
				newPos = p2Home.position;
				newPos.y += 20;
				ship2.position = newPos;
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (noNewCargo) {
				} else if (timeTilCargo > 0)
						timeTilCargo -= Time.deltaTime;
				else if (timeTilCargo <= 0) {
						noNewCargo = true;
						timeTilCargo = 0;
						int x, y, z;
						x = Random.Range (0, 200);
						y = Random.Range (-100, 100);
						z = Random.Range (0, 200);
						Vector3 newCargoPosition = new Vector3 (x, y, z);


						Instantiate (_cargo, newCargoPosition, Quaternion.identity);
						this.cargo = GameObject.Find ("Cargo(Clone)").GetComponent<Cargo_Script> ();

						//instantiate arrows
						Instantiate (arrow, Vector3.zero, Quaternion.identity);
						arrow1 = GameObject.Find ("Arrow(Clone)").GetComponent<ArrowScript> ();
						arrow1.name = "Arrow1";
						arrow1.ship1 = ship1;
						arrow1.ship2 = cargo.transform;

						Instantiate (arrow, Vector3.zero, Quaternion.identity);
						arrow2 = GameObject.Find ("Arrow(Clone)").GetComponent<ArrowScript> ();
						arrow2.name = "Arrow2";
						arrow2.ship1 = ship2;
						arrow2.ship2 = cargo.transform;
				}
		}

		public void captureNotification (int which)
		{
				if (which == 1)
						p1Score++;
				if (which == 2)
						p2Score++;
				timeTilCargo = cargoWait;
				noNewCargo = false;
				GameObject.Destroy (arrow1.gameObject);
				GameObject.Destroy (arrow2.gameObject);
		}
}
