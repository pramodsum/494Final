using UnityEngine;
using System.Collections;
using InControl;

public class Ship : MonoBehaviour
{	
		private int gameOver = 0;//0 game on, 1 you win, 2 you lose
		public ParticleSystem particleSystem;

		public GameObject shot;
		public GameObject explosion;
		
		public GameObject[] otherShip;

		public Transform p1Home;
		public CTF_Script CTF;

		public bool classicMovement = true;
	
		public float shotCooldownTime = 0.3f;
		public float knockbackRemaining = 0f;
		public float knockBack = 100f;

		public float respawnIn;
		public float deadLength = 10f;
		private bool hasExploded = false;
		private bool awardPointsForDestruction = true;
	
		public bool outOfBounds;

		public float FORCE_MODIFIER = 500f;
		public float ROTATION_SPEED = 100f;
		public float MAX_HEALTH = 3;
		public float MAX_BOOST = 10;
		public float CONSTANT_MOVEMENT_AMOUNT = 10f;
	
		public float CAMERA_MIN_FOV = 60f;
		public float CAMERA_MAX_FOV = 80f;
	
		public float health;
		public float boost;
		private bool boostAvailable = true;
		public float boostRefreshRate = .05f;
		public int boostReAvailableAt = 5;
		public float boostDrainRate = .2f;
		private int lives;
		private int score;
		private float shotCooldownRemaining;
		private Camera cameraScreen;
		private Vector3 velocity;
		private int playerNumber;
		private Texture2D healthPixel;
		private Texture2D boostPixel;
		private Texture2D greyPixel;

		public bool enemyInSights;
		public float maxAngleLockOn = 60f;
		public float maxDistLockOn = 50f;
		
		public AudioClip shotSound;

		public GameObject boundary;
	
		void Start ()
		{
				respawnIn = deadLength;
				boost = MAX_BOOST;	
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
				boostPixel = new Texture2D (1, 1);
				boostPixel.SetPixel (0, 0, new Color (0.0F, 0.9F, 0.0F, 0.9F));
				boostPixel.Apply ();
				greyPixel = new Texture2D (1, 1);
				greyPixel.SetPixel (0, 0, new Color (0F, 0F, 0F, 0.5F));
				greyPixel.Apply ();
		}
	
		void Update ()
		{
				if (gameOver == 2) {
					health = 0;
				}
				if (health <= 0) {
						if (!hasExploded) {
								makeInvisible ();
								hasExploded = true;
								Instantiate (explosion, transform.position, Quaternion.identity);
								this.collider.enabled = false;
						}
						if (respawnIn > 0) {
								respawnIn -= Time.deltaTime;
								return;
						}
						if ((gameOver > 0) && name == "Ship1" || name == "Ship2") {
							Application.LoadLevel("_Scene_Main_Menu");
						}
						respawnIn = deadLength;
						respawn ();
				}
				shotCooldownRemaining -= Time.deltaTime;
		
				var inputDevice = (InputManager.Devices.Count >= playerNumber) ? InputManager.Devices [playerNumber - 1] : null;
				if (inputDevice != null) {
						Rotate (Vector3.forward, inputDevice.LeftStickX);
						Rotate (Vector3.right, inputDevice.LeftStickY);
						Rotate (Vector3.down, inputDevice.RightStickX);
						Rotate (Vector3.right, inputDevice.RightStickY);
						
						if (inputDevice.LeftBumper || inputDevice.RightBumper)
								Fire ();
						MoveForward (inputDevice.Action1);	
				} else {
						string inputPrefix = "Player" + playerNumber;
						if (classicMovement) {
								Rotate (Vector3.forward, Input.GetAxis (inputPrefix + "Horizontal"));
								Rotate (Vector3.right, Input.GetAxis (inputPrefix + "Vertical"));
						} else {
								Rotate (Vector3.down, Input.GetAxis (inputPrefix + "Horizontal"));
								Rotate (Vector3.right, Input.GetAxis (inputPrefix + "Vertical"));
						}
						MoveForward (Input.GetAxis (inputPrefix + "Forward"));
						if (Input.GetAxis (inputPrefix + "Fire") == 1) {
								Fire ();
						}
				}

				if (!boundary.collider.bounds.Contains (transform.position)) {
						health -= 0.005f;
				}
		}
	
		void OnGUI ()
		{
				if (gameOver > 0) {
					string text;
					
					var centeredStyle = GUI.skin.GetStyle ("Label");
					centeredStyle.alignment = TextAnchor.UpperCenter;
					var rectVect = cameraScreen.ViewportToScreenPoint (new Vector3 (.5f - (50f / cameraScreen.pixelWidth), .5f - (0 / cameraScreen.pixelHeight), 0));
					Rect rect = new Rect (rectVect.x, Screen.height - rectVect.y, 100, 50);

					if (CTF.p1Score > CTF.p2Score) text = "Red Team Wins!";
					else text = "Blue Team Wins!";
					GUI.Label(rect,text);
					return;
				}
				if (health <= 0) {
						var rectStart = cameraScreen.ViewportToScreenPoint (new Vector3 (0, 0, 0));
						GUI.DrawTexture (new Rect (rectStart.x, Screen.height - rectStart.y, cameraScreen.pixelWidth, cameraScreen.pixelHeight), greyPixel);

						var centeredStyle = GUI.skin.GetStyle ("Label");
						centeredStyle.alignment = TextAnchor.UpperCenter;
						var rectStart2 = cameraScreen.ViewportToScreenPoint (new Vector3 (.5f - (50f / cameraScreen.pixelWidth), .5f - (0 / cameraScreen.pixelHeight), 0));
            
						GUI.Label (new Rect (rectStart2.x, Screen.height - rectStart2.y, 100, 50), "Respawn in " + (int)respawnIn, centeredStyle);
            
						return;
				}

				if (!boostAvailable) {
						boostPixel.SetPixel (0, 0, new Color (0.0F, 0.9F, 0.0F, 0.3F));
						boostPixel.Apply ();
				} else {
						boostPixel.SetPixel (0, 0, new Color (0.0F, 0.9F, 0.0F, 0.9F));
						boostPixel.Apply ();
				}

				var healthPercentage = (((float)health) / ((float)MAX_HEALTH));
				var healthWidth = healthPercentage * cameraScreen.pixelWidth;
				var healthCoords = cameraScreen.ViewportToScreenPoint (new Vector3 (0, 0, 0));
				
				GUI.DrawTexture (new Rect (healthCoords.x, Screen.height - 30 - healthCoords.y, healthWidth, 15f), healthPixel);
				
				var boostPercentage = (((float)boost) / ((float)MAX_BOOST));
				var boostWidth = boostPercentage * cameraScreen.pixelWidth;
				var boostCoords = cameraScreen.ViewportToScreenPoint (new Vector3 (0, .05f, 0));
				
				GUI.DrawTexture (new Rect (boostCoords.x, (Screen.height) - boostCoords.y, boostWidth, 15f), boostPixel);     
		}
	
		void OnTriggerEnter (Collider other)
		{
				if (other.gameObject.name == "Shot") {
						other.SendMessage ("CollideWithShip", gameObject);
				}
		}

		void OnTriggerExit (Collider other)
		{
				if (other.gameObject.name == "Boundary") {
						Debug.Log ("This is not that path you are looking for....");
						Damage (0,0);
				}
		}
	
		void Rotate (Vector3 direction, float amount)
		{
				transform.Rotate (direction * Time.deltaTime * amount * ROTATION_SPEED);
		}
	
		void MoveForward (float extraPercent)
		{
				if (particleSystem != null)
						particleSystem.enableEmission = false;
				bool isNotBreak = true;
						
				var inputDevice = (InputManager.Devices.Count >= playerNumber) ? InputManager.Devices [playerNumber - 1] : null;
				if (inputDevice != null) {
						isNotBreak = inputDevice.Action2 == 0;
				} else {					
						string inputPrefix = "Player" + playerNumber;
						isNotBreak = Input.GetAxis (inputPrefix + "Break") == 0;
				}

				var force = FORCE_MODIFIER;
				boost += boostRefreshRate; 
				if (boost > MAX_BOOST)
						boost = MAX_BOOST;
				if (!boostAvailable && boost > boostReAvailableAt)
						boostAvailable = true;
							
				if (extraPercent != 0) {
						if (isNotBreak && boostAvailable) 
								boost -= boostDrainRate;
						if (boost <= 0) {
								boost = 0;
								boostAvailable = false;
						}
						if (boost > 0 && boostAvailable)
								force *= (extraPercent + 1);
				}
			
        
				cameraScreen.fieldOfView = Mathf.Lerp (60, 80, extraPercent);
				
				if (isNotBreak) {
						rigidbody.AddForce (transform.up * force);
						if (FORCE_MODIFIER != 0 && particleSystem != null)
								particleSystem.enableEmission = true;
				}

		}

		public void BounceBack ()
		{
				rigidbody.velocity = new Vector3 (rigidbody.velocity.x, -rigidbody.velocity.y * 10, rigidbody.velocity.z);
		}

		public void Damage (int isShot, int shooter)
		{
				health -= 0.3f;
				if (isShot == 1 && health <= 0 && awardPointsForDestruction) {
					if (((playerNumber == 1 || playerNumber == 3) && (shooter == 2 || shooter == 4)) ||
			    		(playerNumber == 2 || playerNumber == 4) && (shooter == 1 || shooter == 3)) {
						awardPointsForDestruction = false;
						if ((playerNumber == 1 || playerNumber == 3)) {
							CTF.shipDestroyed(1);
						}
						else 
							CTF.shipDestroyed(2);
					}
				}
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
				foreach (GameObject _otherShip in otherShip)
						foreach (var position in pos) {
								var newShot = Instantiate (shot, position, Quaternion.identity) as GameObject;
								newShot.name = "Shot";
								newShot.SendMessage ("SetShooter", gameObject);
								// otherShip;
								Vector3 forward = facingDirection ();
								Vector3 between = (_otherShip.transform.position - transform.position).normalized;
								float dist = (transform.position - _otherShip.transform.position).magnitude;
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

								//Add sound effect to shots
								GameObject.Find ("Directional light").audio.PlayOneShot (shotSound, 0.7f);
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
//				cameraScreen = GetComponentInChildren<Camera> () as Camera;
				if (playerNumber == 1)
						cameraScreen = GameObject.Find ("Camera1").GetComponent<Camera> ();
				if (playerNumber == 2)
						cameraScreen = GameObject.Find ("Camera2").GetComponent<Camera> ();
				if (playerNumber == 3)
						cameraScreen = GameObject.Find ("Camera3").GetComponent<Camera> ();
				if (playerNumber == 4)
						cameraScreen = GameObject.Find ("Camera4").GetComponent<Camera> ();
				var shipCount = FindAll ().Length;
				float x = 0f;
				float y = 0f;
				float w = (shipCount > 2) ? 0.5f : 1f;
				float h = (shipCount == 1) ? 1f : 0.5f;
				if (playerNumber == 1) {
						y = (shipCount > 2) ? 0.5f : 0f;
				} else if (playerNumber == 2) {
						x = (shipCount > 2) ? 0.5f : 0f;//why do we do this if 2player?
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
				//int result = 0;
				if (this.name == "Ship1")
						return 1;
				else if (this.name == "Ship2")
						return 2;
				else if (this.name == "Ship3")
						return 3;
				else /*if (this.name == "Ship4")*/
						return 4;
//				foreach (var ship in FindAll()) {
//						result ++;
//						if (ship == this) {
//								return result;
//						}
//				}
//				throw new System.Exception ("Unable to find player in list of players");
		}
	
		private Vector3 facingDirection ()
		{
				return transform.up;
		}

		public void respawn ()
		{
				awardPointsForDestruction = true;
				makeVisible ();
				this.collider.enabled = true;
				hasExploded = false;
				if (CTF != null && CTF.cargo.ship == transform) {
						CTF.cargo.cargoStatus = 0;
						CTF.cargo.transform.localScale = new Vector3 (10f, 10f, 10f);
				}
				Vector3 newPos = p1Home.position;
				newPos.y += 20;
				transform.position = newPos;
				health = MAX_HEALTH;
				if (CTF != null) {
						transform.LookAt (new Vector3 (0, 0, 0));
						transform.Rotate (new Vector3 (90, 0, 0));
				}
		}

		public void makeInvisible ()
		{
				this.renderer.enabled = false;	
				foreach (Transform child in transform) {
						if (!child.name.Contains ("Camera"))
								child.renderer.enabled = false;	
				}
				GameObject arrow = GameObject.Find ("Arrow" + playerNumber);
				if (arrow != null) {	
						this.renderer.enabled = false;
						foreach (Transform child in arrow.transform) {
								if (!child.name.Contains ("Camera"))
										child.renderer.enabled = false;	
						}
				}
		}

		public void makeVisible ()
		{
				this.renderer.enabled = true;
				foreach (Transform child in transform) {
						if (!child.name.Contains ("Camera"))
								child.renderer.enabled = true;	
				}
				GameObject arrow = GameObject.Find ("Arrow" + playerNumber);
				if (arrow != null) {	
						this.renderer.enabled = true;
						foreach (Transform child in arrow.transform) {
								if (!child.name.Contains ("Camera"))
										child.renderer.enabled = true;	
						}
				}   
		}

		public void makeGameOver(bool won)
		{
			if (won)
				gameOver = 1;
			else gameOver = 2;
		}
}