using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {
	private Vector3 direction;
	private Vector3 velocity;

	public float maxDist = 10f;

	private Vector3 startPosition;

	void Start() {
		startPosition = transform.position;
	}
	
	void Update() {
		if (Vector3.Distance(transform.position, startPosition) > maxDist)
			GameObject.Destroy(gameObject);
	}
}
