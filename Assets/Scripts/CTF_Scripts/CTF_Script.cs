using UnityEngine;
using System.Collections;

public class CTF_Script : MonoBehaviour
{
		//just used to tell who is controlling which station
		public Material red;	
		public Material blue;
		public TransportMove transport;
		private int tillTick;
		private int Tick = 1;

		public int winScore = 10000;
		public int captureScore = 2500;

		public int pointsOnKill = 300;

		public Station_Control[] Stations;
		public GameObject _cargo;
		public GameObject arrow;

		public Cargo_Script cargo;

		public float timeTilCargo;
		public float cargoWait = 1;
		private bool noNewCargo;

		public Transform ship1;
		public Transform ship2;
		public Transform ship3;
		public Transform ship4;


		public Transform p1Home;
		public Transform p2Home;

		public float p1Score;
		public float p2Score;

		public ArrowScript arrow1;
		public ArrowScript arrow2;
		public ArrowScript arrow3;
		public ArrowScript arrow4;

		System.Random rand;


		// Use this for initialization 
		void Start ()
		{
				tillTick = Tick;
				p1Score = 0;
				p2Score = 0;
				timeTilCargo = cargoWait;
				noNewCargo = false;
				rand = new System.Random ((int)Time.time);

				Vector3 newPos = p1Home.position;
				newPos.y += 20;
				ship1.position = newPos;
				newPos.z -= 5;
				if (ship3 != null)
						ship3.position = newPos;
				newPos = p2Home.position;
				newPos.y += 20;
				ship2.position = newPos;
				newPos.z -= 5;
				if (ship4 != null)
						ship4.position = newPos;
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (p1Score >= winScore) {
						ship1.gameObject.GetComponent<Ship> ().makeGameOver (true);
						ship2.gameObject.GetComponent<Ship> ().makeGameOver (false);
						ship3.gameObject.GetComponent<Ship> ().makeGameOver (true);
						ship4.gameObject.GetComponent<Ship> ().makeGameOver (false);
						return;
				} else if (p2Score >= winScore) {
						ship1.gameObject.GetComponent<Ship> ().makeGameOver (false);
						ship2.gameObject.GetComponent<Ship> ().makeGameOver (true);
						ship3.gameObject.GetComponent<Ship> ().makeGameOver (false);
						ship4.gameObject.GetComponent<Ship> ().makeGameOver (true);
						return;
				}
				tillTick--;
				foreach (Station_Control station in Stations) {
						if (station.inControl && tillTick == 0) {
								if (station.transform.FindChild ("Sphere").renderer.material.color == red.color) {
										p1Score += 0.1f;
								}
								if (station.transform.FindChild ("Sphere").renderer.material.color == blue.color) {
										p2Score += 0.1f;
								}
						}
				}
				if (transport.isAlive) {
						noNewCargo = false;
				} 
/*				else if (timeTilCargo > 0)
						timeTilCargo -= Time.deltaTime;*/
				else if (!noNewCargo) { 
						noNewCargo = true;
						timeTilCargo = 0;
						/*int x, y, z;
						x = Random.Range (0, 200);
						y = Random.Range (-100, 100);
						z = Random.Range (0, 200);
						Vector3 newCargoPosition = new Vector3 (x, y, z);*/


						Instantiate (_cargo, transport.transform.position, Quaternion.identity);
						this.cargo = GameObject.Find ("Cargo(Clone)").GetComponent<Cargo_Script> ();
						this.cargo.tag = "Cargo";
						//instantiate arrows
						Instantiate (arrow, Vector3.zero, Quaternion.identity);
						arrow1 = GameObject.Find ("Arrow(Clone)").GetComponent<ArrowScript> ();
						arrow1.name = "Arrow1";
						arrow1.ship1 = ship1;
						arrow1.ship2 = cargo.transform;
						arrow1.gameObject.layer = LayerMask.NameToLayer ("Ship1-objects");
						foreach (Transform child in arrow1.transform) {
								child.gameObject.layer = LayerMask.NameToLayer ("Ship1-objects");
						}
						

						Instantiate (arrow, Vector3.zero, Quaternion.identity);
						arrow2 = GameObject.Find ("Arrow(Clone)").GetComponent<ArrowScript> ();
						arrow2.name = "Arrow2";
						arrow2.ship1 = ship2;
						arrow2.ship2 = cargo.transform;
						arrow2.gameObject.layer = LayerMask.NameToLayer ("Ship2-objects");
						foreach (Transform child in arrow2.transform) {
								child.gameObject.layer = LayerMask.NameToLayer ("Ship2-objects");
						}

						if (ship3 != null) {
								Instantiate (arrow, Vector3.zero, Quaternion.identity);
								arrow3 = GameObject.Find ("Arrow(Clone)").GetComponent<ArrowScript> ();
								arrow3.name = "Arrow3";
								arrow3.ship1 = ship3;
								arrow3.ship2 = cargo.transform;
								arrow3.gameObject.layer = LayerMask.NameToLayer ("Ship3-objects");
								foreach (Transform child in arrow3.transform) {
										child.gameObject.layer = LayerMask.NameToLayer ("Ship3-objects");
								}
						}
						if (ship4 != null) {
								Instantiate (arrow, Vector3.zero, Quaternion.identity);
								arrow4 = GameObject.Find ("Arrow(Clone)").GetComponent<ArrowScript> ();
								arrow4.name = "Arrow4";
								arrow4.ship1 = ship4;
								arrow4.ship2 = cargo.transform;
								arrow4.gameObject.layer = LayerMask.NameToLayer ("Ship4-objects");
								foreach (Transform child in arrow4.transform) {
										child.gameObject.layer = LayerMask.NameToLayer ("Ship4-objects");
								}
						}
				}
				if (tillTick == 0)
						tillTick = Tick;
		}

		public void captureNotification (int which)
		{
				if (which == 1)
						p1Score += captureScore;
				if (which == 2)
						p2Score += captureScore;
				timeTilCargo = cargoWait;
//				noNewCargo = false;
				GameObject.Destroy (arrow1.gameObject);
				GameObject.Destroy (arrow2.gameObject);
		}

		public string getAttributeByName (string s)
		{
				if (s.Equals ("p1Score"))
						return "" + p1Score;
				else if (s.Equals ("p2Score"))
						return "" + p2Score;
				else
						return "";
		}

		public void shipDestroyed (int team)
		{
				if (team == 1) {
						p2Score += pointsOnKill;
				} else {
						p1Score += pointsOnKill;
				}
		}
}
