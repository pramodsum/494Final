using UnityEngine;
using System.Collections;

public class Shot2 : MonoBehaviour {
	private Vector3 direction;
	private Vector3 velocity;
	public float speed = 10f;

	private Vector3 endPos;
	public float maxDist = 10f;

	// Use this for initialization
	void Start () {
		GameObject ship = GameObject.Find ("Ship2");
		endPos = ship.transform.position;
		GameObject obj = GameObject.Find ("Bullseye2");
		Vector3 pos = obj.transform.position;
		direction = (pos - transform.position).normalized;
		velocity = direction * speed;
		transform.rotation = Quaternion.LookRotation (Vector3.one - direction);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += velocity * Time.deltaTime;
		if (Vector3.Distance(transform.position,endPos) > maxDist)
			GameObject.Destroy(gameObject);
	}
}
