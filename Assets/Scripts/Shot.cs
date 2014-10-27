using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {
	public GameObject explosion;

	private Vector3 direction;
	private Vector3 velocity;

	public float maxDist = 10f;

	private Vector3 startPosition;
	private GameObject shooter;

	void Start() {
		startPosition = transform.position;
	}
	
	void Update() {
		if (Vector3.Distance(transform.position, startPosition) > maxDist)
			GameObject.Destroy(gameObject);
	}

	void SetShooter(GameObject s) { shooter = s; }

	void CollideWithShip(GameObject ship) {
		if (shooter == ship) { return; }
		var otherPos = ship.transform.position;
		var knockDirection = (transform.position - otherPos).normalized;
		Instantiate(explosion, otherPos, Quaternion.identity);
		Destroy(gameObject);
		ship.rigidbody.AddForce(knockDirection * 100f);
	}
}
