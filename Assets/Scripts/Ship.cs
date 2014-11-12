using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
		public GameObject shot;

		public GameObject otherShip;
		public Transform p1Home;
		public CTF_Script CTF;
	
		public float shotCooldownTime = 0.3f;
		public float knockbackRemaining = 0f;
		public float knockBack = 100f;
	
		public bool outOfBounds;

		private readonly float FORCE_MODIFIER = 500f;
		private readonly float ROTATION_SPEED = 100f;
		private readonly float MAX_HEALTH = 3;
		private readonly float CONSTANT_MOVEMENT_AMOUNT = 10f;
	
		private readonly float CAMERA_MIN_FOV = 60f;
		private readonly float CAMERA_MAX_FOV = 80f;
	
		private float health;
		private int lives;
		private int score;
		private float shotCooldownRemaining;
		private Camera cameraScreen;
		private Vector3 velocity;
		private int playerNumber;
		private Texture2D healthPixel;

		public bool enemyInSights;
		public float maxAngleLockOn = 60f;
		public float maxDistLockOn = 50f;
	
		void Start ()
		{
				health = MAX_HEALTH;
				score = 0;
				lives = 1;
				outOfBounds = false;
				shotCooldownRemaining = 0f;
				playerNumber = GetPlayerNumber ();
				adjustCamera ();
				healthPixel = new Texture2D (1, 1);
				healthPixel.SetPixel (0, 0, new Color (0.9F, 0.0F, 0.3F, 0.9F));
				healthPixel.Apply ();
		}
	
		void Update ()
		{
				if (health <= 0) {
						respawn ();
				}
				shotCooldownRemaining -= Time.deltaTime;
		
				string inputPrefix = "Player" + playerNumber;
				Rotate (Vector3.forward, Input.GetAxis (inputPrefix + "Horizontal"));
				Rotate (Vector3.right, Input.GetAxis (inputPrefix + "Vertical"));
				MoveForward (Input.GetAxis (inputPrefix + "Forward"));
				if (Input.GetAxis (inputPrefix + "Fire") == 1) {
						Fire ();
				}
		}
	
		void OnGUI ()
		{
				var healthPercentage = (((float)health) / ((float)MAX_HEALTH));
				var healthWidth = healthPercentage * cameraScreen.pixelWidth;
				var healthCoords = cameraScreen.ViewportToScreenPoint (new Vector3 (0, 0, 0));
				GUI.DrawTexture (new Rect (healthCoords.x, healthCoords.y, healthWidth, 15f), healthPixel);
		}
	
		void OnTriggerEnter (Collider other)
		{
				if (other.gameObject.name == "Shot") {
						other.SendMessage ("CollideWithShip", gameObject);
				}
		}
	
		void Rotate (Vector3 direction, float amount)
		{
				transform.Rotate (direction * Time.deltaTime * amount * ROTATION_SPEED);
		}
	
		void MoveForward (float extraPercent)
		{
				var force = (extraPercent + 1) * FORCE_MODIFIER;
				cameraScreen.fieldOfView = Mathf.Lerp (60, 80, extraPercent);
				rigidbody.AddForce (transform.up * force);
		}

		public void Damage ()
		{
				health -= 0.3f;
		}
	
		void Fire ()
		{
				if (shotCooldownRemaining > 0) {
						return;
				}
				shotCooldownRemaining = shotCooldownTime;
				// Vector3.Scale (transform.localScale / 2, new Vector3 (1, 1, -1));
				Vector3[] pos = new Vector3[4];
				//top-left
				Matrix4x4 thisMatrix = transform.localToWorldMatrix;
				Quaternion storedRotation = transform.rotation;
				transform.rotation = Quaternion.identity;
				Vector3 extents = collider.bounds.extents * 2;
				pos [0] = thisMatrix.MultiplyPoint3x4 (new Vector3 (-extents.x, extents.y, extents.z));
				pos [1] = thisMatrix.MultiplyPoint3x4 (new Vector3 (extents.x, -extents.y, extents.z));
				pos [2] = thisMatrix.MultiplyPoint3x4 (new Vector3 (-extents.x, -extents.y, extents.z));
				pos [3] = thisMatrix.MultiplyPoint3x4 (new Vector3 (extents.x, -extents.y, -extents.z));
				transform.rotation = storedRotation;
				// pos[0] = transform.TransformPoint (new Vector3 (1, 1, -1));
				// pos[1] = transform.TransformPoint (new Vector3 (1, 1, -1));
				// pos[2] = transform.TransformPoint (new Vector3 (1, 1, 1));
				// pos[3] = transform.TransformPoint (new Vector3 (1, 1, 1));
				// pos[0].x = transform.collider.bounds.min.x;
				// pos[0].y = transform.collider.bounds.max.y;
				// pos[0].z = transform.collider.bounds.max.z;
				// //top-right
				// pos[1].x = transform.collider.bounds.max.x;
				// pos[1].y = transform.collider.bounds.max.y;
				// pos[1].z = transform.collider.bounds.max.z;
				// //bottom-left
				// pos[2].x = transform.collider.bounds.min.x;
				// pos[2].y = transform.collider.bounds.min.y;
				// pos[2].z = transform.collider.bounds.max.z;
				// //bottom-right
				// pos[3].x = transform.collider.bounds.max.x;
				// pos[3].y = transform.collider.bounds.min.y;
				// pos[3].z = transform.collider.bounds.max.z;
				foreach (var position in pos) {
						var newShot = Instantiate (shot, position, Quaternion.identity) as GameObject;
						newShot.name = "Shot";
						newShot.SendMessage ("SetShooter", gameObject);
						// otherShip;
						Vector3 forward = facingDirection ();
						Vector3 between = (otherShip.transform.position - transform.position).normalized;
						float dist = (transform.position - otherShip.transform.position).magnitude;
						float angleTwixt = Vector3.Angle (forward, between);
						// print (""+angleTwixt+" "+dist);
						enemyInSights = false;
						if (angleTwixt <= maxAngleLockOn && dist <= maxDistLockOn) {
								enemyInSights = true;
								print ("" + angleTwixt + " " + dist);
						}
						if (enemyInSights)
								newShot.rigidbody.AddForce (between * 0.001f);
						else
								newShot.rigidbody.AddForce (facingDirection () * 0.001f);
				}
		}
	
		public string getAttributeByName (string s)
		{
				if (s.Equals ("_health"))
						return "" + health;
				else if (s.Equals ("_lives"))
						return "" + lives;
				else if (s.Equals ("_score"))
						return "" + score;
				else
						return null;
		}
	
		private void adjustCamera ()
		{
				cameraScreen = GetComponentInChildren<Camera> () as Camera;
				var shipCount = FindAll ().Length;
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
				cameraScreen.rect = new Rect (x, y, w, h);
		}
	
		public static Object[] FindAll ()
		{
				return GameObject.FindObjectsOfType (typeof(Ship));
		}
	
		public int GetPlayerNumber ()
		{
				int result = 0;
				foreach (var ship in FindAll()) {
						result ++;
						if (ship == this) {
								return result;
						}
				}
				throw new System.Exception ("Unable to find player in list of players");
		}
	
		private Vector3 facingDirection ()
		{
				return transform.up;
		}

		public void respawn ()
		{
				if (CTF != null && CTF.cargo.ship == transform) {
						CTF.cargo.cargoStatus = 0;
						CTF.cargo.transform.localScale = new Vector3(10f,10f,10f);
				}
				Vector3 newPos = p1Home.position;
				newPos.y += 20;
				transform.position = newPos;
				health = MAX_HEALTH;
		}
}