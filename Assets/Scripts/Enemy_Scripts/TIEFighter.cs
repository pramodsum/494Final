using UnityEngine;
using System.Collections;

public class TIEFighter : MonoBehaviour {
	public GameObject pathThingy;
	public float degree = 0;

	public GameObject shot;
	public AudioClip shotSound;
	public float ROTATION_SPEED = 100f;
	public float forceModifier = 500f;
	public Vector3 relativePosStart;
	public GameObject defend;

	public GameObject[] hostileShips;
	public float hostileDistance;

	public float shotCooldownTime = 1;
	private float shotCooldownRemaining;

	private float distFromShip = 10f;
	public float chaseDistance = 25f;
	private bool isDead = false;
	public int health = 4;

	public GameObject explosion;

	public CTF_Script CTF;
	public Material red;
	public Material blue;
	public int pointsOnKill = 100;

	public 
	// Use this for initialization
	void Start () {
		shotCooldownRemaining = shotCooldownTime;
//		defend = GameObject.Find ("Ship_prefab");
//		relativePosStart = transform.position - defend.transform.position;

	
//		Vector3 temp = transform.position;
//		temp.x = pathThingy.transform.localScale.x/2;
//		transform.position = temp;
	}
	
	// Update is called once per frame
	void Update () {
//		if (health <= 0)
//		{
//			Instantiate (explosion, transform.position, Quaternion.identity);
//			GameObject.Destroy(gameObject);
//		}
		shotCooldownRemaining -= Time.deltaTime;
		if (shotCooldownRemaining < 0)
			shotCooldownRemaining = 0;

		GameObject nearestShip = null;
		float nearestDistThis = 10000000f;
		float nearestDistToShip = 10000000f;
		Vector3 destination;
		foreach (GameObject ship in hostileShips)
		{
			float dist = Vector3.Distance(ship.transform.position, transform.position);
			if (dist < nearestDistThis && (ship.GetComponent<Ship>().health > 0))
			{
				nearestShip = ship;
				nearestDistThis = dist;
				nearestDistToShip = Vector3.Distance(ship.transform.position, defend.transform.position);
			}
		}
//		print ("" + nearestDist + " " + hostileDistance);
		float _forceModifier = forceModifier;
		if ((nearestDistThis < hostileDistance) && (nearestDistToShip < chaseDistance))
		{
			destination = nearestShip.transform.position;
			Fire();
//			Vector3 helper = (destination - transform.position).normalized * 10;
//			destination -= helper;
		}
		else
		{
			destination = defend.transform.position - defend.transform.rotation*relativePosStart;
			_forceModifier*=1.5f;
		}

//		if (Vector3.Distance(destination,transform.position) > 1)
//		{
			rigidbody.AddForce((destination-transform.position).normalized*_forceModifier);
			Quaternion rotateTo = Quaternion.LookRotation((transform.position-destination).normalized);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTo, 10);
//		}
//		else
//		{
//			transform.position = destination;
//			transform.rotation = defend.transform.rotation;
//		}
	}

	void Rotate (Vector3 direction, float amount)
	{
		transform.Rotate (direction * Time.deltaTime * amount * ROTATION_SPEED);
	}

	void Fire ()
	{
		if (shotCooldownRemaining > 0) {
			return;
		}
		shotCooldownRemaining = shotCooldownTime;
		//top-left
		Matrix4x4 thisMatrix = transform.localToWorldMatrix;
		Quaternion storedRotation = transform.rotation;
		transform.rotation = Quaternion.identity;
		Vector3 extents = collider.bounds.extents;
		Vector3[] pos = new Vector3[4];
		pos [0] = thisMatrix.MultiplyPoint3x4 (new Vector3 (-extents.x, extents.y, extents.z));
		pos [1] = thisMatrix.MultiplyPoint3x4 (new Vector3 (extents.x, -extents.y, extents.z));
		pos [2] = thisMatrix.MultiplyPoint3x4 (new Vector3 (-extents.x, -extents.y, extents.z));
		pos [3] = thisMatrix.MultiplyPoint3x4 (new Vector3 (extents.x, -extents.y, -extents.z));
//		Vector3[] pos = new Vector3[1];
//		pos [0] = thisMatrix.MultiplyPoint3x4 (transform.position);

		transform.rotation = storedRotation;
//		foreach (GameObject _otherShip in otherShip)
		foreach (var position in pos) {
			var newShot = Instantiate (shot, transform.position, Quaternion.identity) as GameObject;
			newShot.name = "ShotAI";
			newShot.SendMessage ("SetShooter", gameObject);
			newShot.rigidbody.AddForce (transform.forward * -0.001f);
			
			//Add sound effect to shots
			//								GameObject.Find ("Directional light").audio.PlayOneShot (shotSound, 0.3f);
		}
		GameObject.Find ("Directional light").audio.PlayOneShot (shotSound, 0.1f);
		
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.name == "Shot") {
			health--;
			if (health <= 0 && !isDead)
			{
				isDead = true;
				if (CTF != null)
				{
					if (other.renderer.material.color == red.color) CTF.p1Score += pointsOnKill;
					if (other.renderer.material.color == blue.color) CTF.p1Score += pointsOnKill;
				}
				Instantiate (explosion, transform.position, Quaternion.identity);
				GameObject.Destroy(gameObject);
			}
		}
	}

}
