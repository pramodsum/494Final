using UnityEngine;
using System.Collections;

public class Bullseye : MonoBehaviour {
	public float dist;
	static public bool isOnAsteroid = false;

	// Use this for initialization
	void Start () {
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = dist;
		Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
		transform.position = pos;

	}
	
	// Update is called once per frame
	void Update () {
		float oldZ = transform.position.z;

		Vector3 mousePos = Input.mousePosition;
		mousePos.z = dist;
		Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);

		if (isOnAsteroid)
			pos.z = oldZ;

		transform.position = pos;

//		Vector3 mousePos = Input.mousePosition;
//		//		mousePos.z = dist;
//		float oldZ = transform.position.z;
//		//		Vector3 newPos = transform.position;
//		//		newPos.x = mousePos.x;
//		//		newPos.y = mousePos.y;
//		
//		Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
//		pos.z = oldZ;
//		transform.position = pos;
	}


}
