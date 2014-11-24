using UnityEngine;
using System.Collections;

public class TransportShot : MonoBehaviour
{
		public GameObject explosion;

		void OnTriggerEnter (Collider other)
		{
				if (other.gameObject.name == "Shot") {
						GetComponentInParent<TransportMove> ().health--;
						Instantiate (explosion, other.transform.position, Quaternion.identity);
				}
		}
}
