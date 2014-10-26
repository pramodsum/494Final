using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	public float forceModifier = 100f;
	public GameObject shot;
	public float shotCooldownTime = 1f;
	public float knockbackRemaining = 0f;
	public float knockBack = 100f;

	private readonly float ROTATION_SPEED = 100f;

	private int health;
	private int lives;
	private int score;
	private float shotCooldownRemaining;
	private Camera cameraScreen;
	private Vector3 velocity;
	private int playerNumber;
	
	void Start() {
		health = 10;
		score = 0;
		lives = 1;
		shotCooldownRemaining = 0f;
		playerNumber = GetPlayerNumber();
		adjustCamera();
	}
	
	void Update() {
		shotCooldownRemaining -= Time.deltaTime;

		string inputPrefix = "Player" + playerNumber;
		Rotate(Vector3.forward, Input.GetAxis(inputPrefix + "Horizontal"));
		Rotate(Vector3.right, Input.GetAxis(inputPrefix + "Vertical"));
		if (Input.GetAxis(inputPrefix + "Forward") == 1) { MoveForward(); }
		if (Input.GetAxis(inputPrefix + "Fire") == 1) { Fire(); }
	}
	
	void Rotate(Vector3 direction, float amount) {
		transform.Rotate(direction * Time.deltaTime * amount * ROTATION_SPEED);
	}
	
	void MoveForward() {
		rigidbody.AddForce(facingDirection() * forceModifier);
	}
	
	void Fire() {
		print (shotCooldownRemaining);
		if (shotCooldownRemaining > 0) { return; }
		shotCooldownRemaining = shotCooldownTime;
		Vector3[] pos = new Vector3[4];
		//top-left
		pos[0].x = transform.collider.bounds.min.x;
		pos[0].y = transform.collider.bounds.max.y;
		pos[0].z = transform.collider.bounds.max.z;
		//top-right
		pos[1].x = transform.collider.bounds.max.x;
		pos[1].y = transform.collider.bounds.max.y;
		pos[1].z = transform.collider.bounds.max.z;
		//bottom-left
		pos[2].x = transform.collider.bounds.min.x;
		pos[2].y = transform.collider.bounds.min.y;
		pos[2].z = transform.collider.bounds.max.z;
		//bottom-right
		pos[3].x = transform.collider.bounds.max.x;
		pos[3].y = transform.collider.bounds.min.y;
		pos[3].z = transform.collider.bounds.max.z;
		foreach (var position in pos) {
			var newShot = Instantiate(shot, position, Quaternion.identity) as GameObject;
			newShot.rigidbody.AddForce(facingDirection() * 1f);
		}
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

	private Vector3 facingDirection() {
		return (transform.position - cameraScreen.transform.position).normalized;
	}
}