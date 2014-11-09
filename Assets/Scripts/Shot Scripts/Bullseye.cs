using UnityEngine;
using System.Collections;

public class Bullseye : MonoBehaviour {
	public float dist;
	static public bool isOnAsteroid = false;
	public bool isFixed = true;

	// Use this for initialization
	void Start () {
//		Vector3 mousePos = Input.mousePosition;
//		mousePos.z = dist;
//		Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
//		transform.position = pos;

	}
	
	// Update is called once per frame
	void Update () {
		if (!isFixed)
		{
			float oldZ = transform.position.z;
		
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = dist;
			Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);

			if (isOnAsteroid)
				pos.z = oldZ;

			transform.position = pos;
		}
		else
		{
//			Vector3 pos = new Vector3 (0,10,0);
//			transform.position = pos;
		}

	}


}
