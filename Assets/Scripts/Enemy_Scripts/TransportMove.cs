using UnityEngine;
using System.Collections;

public class TransportMove : MonoBehaviour
{
		public Vector3[] path;
		public int health = 750;
		public int MAX_HEALTH = 750;
		public GameObject explosion;
		float t;
		bool hasExploded = false;
		public bool isAlive = true;

		// Update is called once per frame
		void Update ()
		{
				Quaternion q = transform.rotation;
				transform.position = Spline.MoveOnPath (path, transform.position, ref t, ref q, 30, 10000, EasingType.Quintic, true, true);
				transform.localRotation = q;
				transform.Rotate (180, 0, 0);
				
				if (t >= 0.90)
						t = 0f;	
				
				if (health <= 0) {
						if (!hasExploded) {
								makeInvisible ();
								hasExploded = true;
								Instantiate (explosion, transform.position, Quaternion.identity);
						}				
				}
		}
		
		public void Respawn ()
		{
				hasExploded = true;
		}
		
		public void makeInvisible ()
		{
				isAlive = false;
				if (this.renderer)
						this.renderer.enabled = false;
				if (this.collider)
						this.collider.enabled = false;
				foreach (Transform child in transform) {
						if (child.renderer) 
								child.renderer.enabled = false;	
						if (child.collider)
								child.collider.enabled = false;
						foreach (Transform ch2 in child.transform) {
								if (ch2.renderer) 
										ch2.renderer.enabled = false;
								if (ch2.collider)
										ch2.collider.enabled = false;	
						}
				}

		}
	
		public void makeVisible ()
		{
				health = MAX_HEALTH;
				hasExploded = false;
				isAlive = true;
				if (this.renderer)
						this.renderer.enabled = true;
				if (this.collider)
						this.collider.enabled = true;
				foreach (Transform child in transform) {
						if (child.renderer) 
								child.renderer.enabled = true;	
						if (child.collider)
								child.collider.enabled = true;
						foreach (Transform ch2 in child.transform) {
								if (ch2.renderer) 
										ch2.renderer.enabled = true;
								if (ch2.collider)
										ch2.collider.enabled = true;	
						}
				}
 
		}
}