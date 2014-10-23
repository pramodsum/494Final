﻿using UnityEngine;
using System.Collections;

public class AsteroidMovement : MonoBehaviour {
	public Transform explosion;
	public GameObject bullseye;
	// Use this for initialization
	void Start () {
		bullseye = GameObject.Find ("Bullseye");
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.z < -15) 
			GameObject.Destroy(gameObject);

		Vector3 pos = transform.position;
		Vector3 v = new Vector3 (0,0,-25);
		pos += v * Time.deltaTime;
		transform.position = pos;
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag.Equals("PlayerShot"))
		{
			Player_Script._score += 100;
			print ("Asteroid destroyed!");
			Destroy(other.gameObject);
			Destroy(gameObject);
			Instantiate(explosion, transform.position, Quaternion.identity);
		}
		if (other.tag.Equals("PlayerShip"))
		{
			print ("Asteroid has collided with ship!");
			Destroy(gameObject);
			Instantiate(explosion, transform.position, Quaternion.identity);
		}
	}

	void OnCollisionEnter(Collision collision) 
	{
		if (collision.collider.tag.Equals("PlayerShip"))
		{
			print ("Asteroid has collided with ship!");
			Destroy(gameObject);
			Instantiate(explosion, transform.position, Quaternion.identity);
		}    
	} 

	void OnMouseOver()
	{
//		print (transform.position);
//		Vector3 pos = bullseye.transform.position;
//		pos.z = transform.position.z;
//		bullseye.transform.position = pos;
	}
}
