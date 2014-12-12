using UnityEngine;
using System.Collections;

public class TransportMove : MonoBehaviour
{
		public float respawnTime = 30f;
		public float timeTillRespawn = -100;
		public Vector3[] path;
		public int health = 750;
		public int MAX_HEALTH = 750;
		public GameObject explosion;
		float t;
		bool hasExploded = false;
		public bool isAlive = true;

		public CTF_Script CTF;
		public GameObject defender;
		public bool spawnDefenders = true;
		public AudioClip explosionSound;
		public AudioClip explosionSound2;

		void Start ()
		{
//				spawnTIEFighters ();
				makeInvisible ();	
				timeTillRespawn = respawnTime;
				isAlive = true;
		}

		// Update is called once per frame
		void Update ()
		{
				Quaternion q = transform.rotation;
				transform.position = Spline.MoveOnPath (path, transform.position, ref t, ref q, 30, 10000, EasingType.Quintic, true, true);
				transform.localRotation = q;
				transform.Rotate (180, 0, 0);
				
				if (t >= 0.90)
						t = 0f;	
				
				if (health <= 0) {
						if (!hasExploded) {
								makeInvisible ();
								hasExploded = true;
								Instantiate (explosion, transform.position, Quaternion.identity);
								GameObject.Find ("Directional light").audio.PlayOneShot (explosionSound, 1f);
								GameObject.Find ("Directional light").audio.PlayOneShot (explosionSound2, 1f);
						}				
				}
				if (timeTillRespawn > 0)
						timeTillRespawn -= Time.deltaTime;
				if (timeTillRespawn <= 0 && timeTillRespawn > -100) {
						makeVisible ();
						timeTillRespawn = -100;
						GameObject.Find ("Directional light").GetComponent<EventManager> ().transportArrived ();
				}

		}
		
		public void Respawn ()
		{
				hasExploded = true;
		}
		
		public void makeInvisible ()
		{
				isAlive = false;
				if (this.renderer)
						this.renderer.enabled = false;
				if (this.collider)
						this.collider.enabled = false;
				foreach (Transform child in transform) {
						if (child.renderer) 
								child.renderer.enabled = false;	
						if (child.collider)
								child.collider.enabled = false;
						foreach (Transform ch2 in child.transform) {
								if (ch2.renderer) 
										ch2.renderer.enabled = false;
								if (ch2.collider)
										ch2.collider.enabled = false;	
						}
				}

		}
	
		public void makeVisible ()
		{
				spawnTIEFighters ();
				health = MAX_HEALTH;
				hasExploded = false;
				isAlive = true;
				if (this.renderer)
						this.renderer.enabled = true;
				if (this.collider)
						this.collider.enabled = true;
				foreach (Transform child in transform) {
						if (child.renderer) 
								child.renderer.enabled = true;	
						if (child.collider)
								child.collider.enabled = true;
						foreach (Transform ch2 in child.transform) {
								if (ch2.renderer) 
										ch2.renderer.enabled = true;
								if (ch2.collider)
										ch2.collider.enabled = true;	
						}
				}
		}

		public void spawnTIEFighters ()
		{
				if (spawnDefenders)
						for (int i = 0; i < 4; i++) {
								Vector3 shipPosInit = transform.position;
								if (i == 1 || i == 3)
										shipPosInit.x += 50;
								else
										shipPosInit.x -= 50;
								//				print (shipPosInit.x);
								//						if (i == 1 || i == 3)
								//							shipPosInit.y += 90;
								//						else shipPosInit.y -= 90;
								if (i == 1 || i == 2)
										shipPosInit.z += 100;
								else
										shipPosInit.z -= 100;
								TIEFighter defenderObject = (Instantiate (defender, shipPosInit, Quaternion.identity) as GameObject).GetComponent<TIEFighter> ();
								defenderObject.tag = "AIShip";
								defenderObject.defend = gameObject;
								defenderObject.relativePosStart = defenderObject.transform.position - transform.position;
								defenderObject.CTF = CTF;
								if (CTF.ship3 != null)
										defenderObject.hostileShips = new GameObject[4];
								else
										defenderObject.hostileShips = new GameObject[2];
								defenderObject.hostileShips [0] = CTF.ship1.gameObject;
								defenderObject.hostileShips [1] = CTF.ship2.gameObject;
								if (CTF.ship3 != null) {
										defenderObject.hostileShips [2] = CTF.ship3.gameObject;
										defenderObject.hostileShips [3] = CTF.ship4.gameObject;
								}
						}
		}

		public void notifyCapture ()
		{
				timeTillRespawn = respawnTime;
		}
}