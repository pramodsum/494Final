using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	public float forceModifier = 100;
	public GameObject shot;	
	public float shotCooldownTime = 10f;
	public float knockbackRemaining = 0f;
	public float knockBack = 100f;
	
	private int health;
	private int lives;
	private int score;
	private float shotCooldownRemaining;
	private Camera cameraScreen;
	private Vector3 velocity;
	
	void Start() {
		health = 10;
		score = 0;
		lives = 1;
		shotCooldownRemaining = 0f;
		adjustCamera();
	}
	
	void Update() {
		shotCooldownRemaining -= Time.deltaTime;
		Rotate(Vector3.right, 10f);
	}
	
	void Rotate(Vector3 direction, float speed) {
		transform.Rotate(direction * Time.deltaTime * speed);
	}
	
	void MoveForward() {
		Vector3 directionVector = (transform.position - cameraScreen.transform.position).normalized;
		rigidbody.AddForce(directionVector * forceModifier);
	}
	
	void Fire() {
		if (shotCooldownRemaining > 0) { return; }
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
		shotCooldownRemaining = shotCooldownTime;
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

	private void adjustCamera() {
		cameraScreen = GetComponentInChildren<Camera>() as Camera;
		var playerNumber = GetPlayerNumber();
		var shipCount = FindAll().Length;
		float x = 0f;
		float y = 0f;
		float w = (shipCount > 2) ? 0.5f : 1f;
		float h = (shipCount == 1) ? 1f : 0.5f;
		if (playerNumber == 1) {
			y = (shipCount > 2) ? 0.5f : 0f;
		} else if (playerNumber == 2) {
			x = (shipCount > 2) ? 0.5f : 0f;
			y = 0.5f;
		} else if (playerNumber == 3) {
			y = 0f;
			w = (shipCount > 3) ? 0.5f : 1f;
		} else if (playerNumber == 4) {
			x = 0.5f;
		}
		cameraScreen.rect = new Rect(x, y, w, h);
	}

	public static Object[] FindAll() {
		return GameObject.FindObjectsOfType(typeof(Ship));
	}

	public int GetPlayerNumber() {
		int result = 0;
		foreach (var ship in FindAll()) {
			result ++;
			if (ship == this) {
				return result;
			}
		}
		throw new System.Exception("Unable to find player in list of players");
	}
}