using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {

	public Transform ship1;
	public Transform ship2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 arrowAxis = (ship1.position - ship2.position).normalized;
		float arrowAngle = Vector3.Angle (ship1.position, ship2.position);
//		transform.Rotate (arrowAxis, arrowAngle);

		transform.rotation = Quaternion.LookRotation (arrowAxis);
//		transform.rotation = transform.rotation.ToAngleAxis(arrow
//		transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation(arrowAxis), arrowAngle);
		transform.position = ship1.position;
	}
}
