using UnityEngine;
using System.Collections;

public class CreateAsteroidBelt : MonoBehaviour
{
		public Transform objectType;
		public GameObject asteroidBelt;
		private System.Random random;
		public int numSpawned = 0;
		public int numToSpawn;

		// Use this for initialization
		void Start ()
		{
				while (numSpawned < numToSpawn) {
						Spawn ();
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void Spawn ()
		{
				Vector3 spawnPosition = transform.position;
		
				int angle = Random.Range (0, 360);
				int radius = Random.Range (0, 360);
				float x = Random.Range (-600, 600);
				float y = Random.Range (-100, 100);
				float z = Random.Range (-600, 600);
		
				spawnPosition.x = x * Mathf.Cos (angle);
				spawnPosition.y = y;
				spawnPosition.z = z * Mathf.Cos (angle);
		
				GameObject obj = Instantiate (objectType, spawnPosition, Quaternion.identity) as GameObject;
//				obj.transform.parent = asteroidBelt.transform;
				numSpawned++;
		}
}
