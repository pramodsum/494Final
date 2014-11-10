using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour {
	public Transform point;
//	public bool clockwise;
	public Vector3 axis;
	public float speed;

	static System.Random rand;

	// Use this for initialization
	void Start () {
//		if (rand == null) rand = new System.Random();
//		int x = rand.Next (0, 360);
//		transform.RotateAround (point.position, axis, x);
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (point.position, axis, speed);

	}
}
