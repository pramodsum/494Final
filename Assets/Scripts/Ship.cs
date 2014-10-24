using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	public int playerNumber = 1;
	public float forceModifier = 100;
	public GameObject shot;	
	public float shotCoolDownTime = 10f;
	public float knockbackRemaining = 0f;
	public float knockBack = 100f;
	
	private int health;
	private int lives;
	private int score;
	private float shotCoolDownRemaining;
	private Camera camera;
	private Vector3 velocity;
	
	void Start() {
		health = 10;
		score = 0;
		lives = 1;
		shotCoolDownRemaining = 0f;
		camera = GetComponent<Camera>();
	}
	
	void Update() {
		shotCoolDownRemaining -= Time.deltaTime;
	}
	
	void Rotate(Vector3 direction) {
		transform.Rotate(direction * Time.deltaTime);
	}
	
	void MoveForward() {
		Vector3 directionVector = (transform.position - camera.transform.position).normalized;
		rigidbody.AddForce(directionVector * forceModifier);
	}
	
	void Fire() {
		if (shotCoolDownRemaining > 0) { return; }
		Vector3 pos1;//top-left
		pos1.x = transform.collider.bounds.min.x;
		pos1.y = transform.collider.bounds.max.y;
		pos1.z = transform.collider.bounds.max.z;
		Vector3 pos2;//top-right
		pos2.x = transform.collider.bounds.max.x;
		pos2.y = transform.collider.bounds.max.y;
		pos2.z = transform.collider.bounds.max.z;
		Vector3 pos3;//bottom-left
		pos3.x = transform.collider.bounds.min.x;
		pos3.y = transform.collider.bounds.min.y;
		pos3.z = transform.collider.bounds.max.z;
		Vector3 pos4;//bottom-right
		pos4.x = transform.collider.bounds.max.x;
		pos4.y = transform.collider.bounds.min.y;
		pos4.z = transform.collider.bounds.max.z;
		Instantiate (shot, pos1, Quaternion.identity);
		Instantiate (shot, pos2, Quaternion.identity);
		Instantiate (shot, pos3, Quaternion.identity);
		Instantiate (shot, pos4, Quaternion.identity);
		shotCoolDownRemaining = shotCoolDownTime;
	}

	public string getAttributeByName(string s)
	{
		if (s.Equals ("_health"))
			return ""+health;
		else if (s.Equals("_lives"))
			return ""+lives;
		else if (s.Equals("_score"))
			return ""+score;
		else
			return null;
	}
}