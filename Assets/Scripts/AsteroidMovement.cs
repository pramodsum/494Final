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
						// Increment score
						print ("Asteroid destroyed!");
						Destroy (other.gameObject);
						Destroy (gameObject);
						Instantiate (explosion, transform.position, Quaternion.identity);
				}
				if (other.tag.Equals ("PlayerShip")) {
						print ("Asteroid has collided with ship!");
						Destroy (gameObject);
						Instantiate (explosion, transform.position, Quaternion.identity);
				}
		}

		void OnCollisionEnter (Collision collision)
		{
				if (collision.collider.tag.Equals ("PlayerShip")) {
						print ("Asteroid has collided with ship!");
						Destroy (gameObject);
						Instantiate (explosion, transform.position, Quaternion.identity);
				}    
		} 

		void OnMouseOver ()
		{
//		print (transform.position);
//		Vector3 pos = bullseye.transform.position;
//		pos.z = transform.position.z;
//		bullseye.transform.position = pos;
		}
}
