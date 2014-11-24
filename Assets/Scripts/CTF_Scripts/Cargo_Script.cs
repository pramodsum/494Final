using UnityEngine;
using System.Collections;

public class Cargo_Script : MonoBehaviour
{

		public int cargoStatus;//0 is free, 1 is p1, 2 is p2

		public Transform ship;
		public TransportMove transport;

		public CTF_Script CTF;

		// Use this for initialization
		void Start ()
		{
				cargoStatus = 0;
				CTF = GameObject.Find ("Capture The Flag").GetComponent<CTF_Script> ();
				transport = FindObjectOfType<TransportMove> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (cargoStatus == 0) {
						ship = null;
						CTF.arrow1.ship2 = transform;
						CTF.arrow2.ship2 = transform;
						CTF.arrow3.ship2 = transform;
						CTF.arrow4.ship2 = transform;
				}
				if (ship != null) {
						transform.position = ship.position;
						transform.rotation = ship.rotation;
				}
				
		}

		void OnTriggerEnter (Collider collision)
		{
				if (cargoStatus == 0) {
						if (collision.collider.tag.Equals ("PlanetaryObject")) {
								Vector3 newPos = transform.position;
								newPos.y += 100;
								transform.position = newPos;
						} else if (collision.collider.name.Equals ("Ship1") ||
								collision.collider.name.Equals ("Ship3")) {
								if (collision.collider.name.Equals ("Ship1"))
										CTF.arrow1.ship2 = CTF.p1Home;
								else
										CTF.arrow3.ship2 = CTF.p1Home;
								ship = collision.collider.transform;
								cargoStatus = 1;
								transform.localScale = new Vector3 (2f, 2f, 2f);
						} else if (collision.collider.name.Equals ("Ship2") ||
								collision.collider.name.Equals ("Ship4")) {
								if (collision.collider.name.Equals ("Ship2"))
										CTF.arrow2.ship2 = CTF.p2Home;
								else
										CTF.arrow4.ship2 = CTF.p2Home;

								CTF.arrow2.ship2 = CTF.p2Home;
								ship = collision.collider.transform;
								cargoStatus = 2;
								transform.localScale = new Vector3 (2f, 2f, 2f);
						}
				} else {
						if (cargoStatus == 1 && collision.collider.name.Equals ("Ship1Home")) {
								GameObject.Destroy (CTF.arrow1.gameObject);
								GameObject.Destroy (CTF.arrow2.gameObject);
								if (CTF.arrow3 != null)
										GameObject.Destroy (CTF.arrow3.gameObject);
								if (CTF.arrow4 != null)
										GameObject.Destroy (CTF.arrow4.gameObject);
								transport.makeVisible ();
								CTF.captureNotification (1);
								Destroy (gameObject);
						} else if (cargoStatus == 2 && collision.collider.name.Equals ("Ship2Home")) {
								GameObject.Destroy (CTF.arrow1.gameObject);
								GameObject.Destroy (CTF.arrow2.gameObject);
								if (CTF.arrow3 != null)
										GameObject.Destroy (CTF.arrow3.gameObject);
								if (CTF.arrow4 != null)
										GameObject.Destroy (CTF.arrow4.gameObject);
								transport.makeVisible ();
								CTF.captureNotification (2);
								Destroy (gameObject);
						}
				}
		}


}
