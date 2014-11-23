using UnityEngine;
using System.Collections;

public class Transport : MonoBehaviour
{

		public Vector3[] path;
		float t;
		int count = 0;

		// Use this for initialization
		void Start ()
		{
				
		}
	
		// Update is called once per frame
		void Update ()
		{
				Quaternion q = transform.rotation;
				transform.position = Spline.MoveOnPath (path, transform.position, ref t, ref q, 30, 10000, EasingType.Quintic, true, true);
				transform.localRotation = q;
				transform.Rotate (180, 0, 0);
				
				if (t >= 0.80)
						t = 0;
		}
}
