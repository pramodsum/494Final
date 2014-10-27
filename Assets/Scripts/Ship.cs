using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	public GameObject shot;

	public float forceModifier = 100f;
	public float shotCooldownTime = 0.3f;
	public float knockbackRemaining = 0f;
	public float knockBack = 100f;

	private readonly float ROTATION_SPEED = 100f;
	private readonly float MAX_HEALTH = 10;

	private float health;
	private int lives;
	private int score;
	private float shotCooldownRemaining;
	private Camera cameraScreen;
	private Vector3 velocity;
	private int playerNumber;
	private Texture2D healthPixel;
	
	void Start() {
		health = MAX_HEALTH;
		score = 0;
		lives = 1;
		shotCooldownRemaining = 0f;
		playerNumber = GetPlayerNumber();
		adjustCamera();
		healthPixel = new Texture2D(1, 1);
		healthPixel.SetPixel(0, 0, new Color(0.9F, 0.0F, 0.3F, 0.9F));
		healthPixel.Apply();
	}
	
	void Update() {
		shotCooldownRemaining -= Time.deltaTime;

		string inputPrefix = "Player" + playerNumber;
		Rotate(Vector3.forward, Input.GetAxis(inputPrefix + "Horizontal"));
		Rotate(Vector3.right, Input.GetAxis(inputPrefix + "Vertical"));
		if (Input.GetAxis(inputPrefix + "Forward") == 1) { MoveForward(); }
		if (Input.GetAxis(inputPrefix + "Fire") == 1) { Fire(); }
	}

	void OnGUI() {
		var healthPercentage = (((float) health) / ((float) MAX_HEALTH));
		var healthWidth = healthPercentage * cameraScreen.pixelWidth;
		var healthCoords = cameraScreen.ViewportToScreenPoint(new Vector3(0, 0, 0));
		GUI.DrawTexture(new Rect(healthCoords.x, healthCoords.y, healthWidth, 15f), healthPixel);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "Shot") {
			other.SendMessage("CollideWithShip", gameObject);
		}
	}
	
	void Rotate(Vector3 direction, float amount) {
		transform.Rotate(direction * Time.deltaTime * amount * ROTATION_SPEED);
	}
	
	void MoveForward() {
		rigidbody.AddForce(facingDirection() * forceModifier);
	}
	
	void Fire() {
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
			newShot.name = "Shot";
			newShot.SendMessage("SetShooter", gameObject);
			newShot.rigidbody.AddForce(facingDirection() * 0.001f);
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
			if (ship == this) { return result; }
		}
		throw new System.Exception("Unable to find player in list of players");
	}

	private Vector3 facingDirection() {
		return (transform.position - cameraScreen.transform.position).normalized;
	}
}