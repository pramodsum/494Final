﻿using UnityEngine;
using System.Collections;

public class AsteroidMovement : MonoBehaviour
{
		public Transform explosion;
		public GameObject bullseye;
		public float timeSince = 0;
		public GameObject sun;
		public float speed;
		// Use this for initialization
		void Start ()
		{
				bullseye = GameObject.Find ("Bullseye");
		}
	
		// Update is called once per frame
		void Update ()
		{
//				transform.RotateAround (sun.transform.position, Vector3.up, speed);
		}

		void OnTriggerEnter (Collider other)
		{
				if (other.tag.Equals ("PlayerShot")) {
						print ("Asteroid destroyed!");
						Destroy (gameObject);
						Instantiate (explosion, transform.position, Quaternion.identity);
				}
		}

		void OnCollisionEnter (Collision collision)
		{
				if (collision.collider.tag.Equals ("PlayerShip")) {
						print ("Asteroid has collided with ship!");
						collision.gameObject.GetComponent<Ship> ().Damage ();
						Destroy (gameObject);
						Instantiate (explosion, transform.position, Quaternion.identity);
				}    
		} 
}
