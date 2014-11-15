using UnityEngine;
using System.Collections;

public class CreateAsteroidBelt : MonoBehaviour
{
		public Transform objectType;
		public GameObject asteroidBelt;
		private System.Random random;
		public int numSpawned = 0;
		public int numToSpawn;
		public float minVal = -458.86f;
		public float maxVal = 941.14f;

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
				float x = Random.Range (minVal, maxVal);
				float y = Random.Range (-600, 600);
				float z = Random.Range (minVal, maxVal);
		
				spawnPosition.x = x;
				spawnPosition.y = y;
				spawnPosition.z = z;
		
				GameObject obj = Instantiate (objectType, spawnPosition, Quaternion.identity) as GameObject;
//				obj.transform.parent = asteroidBelt.transform;
				numSpawned++;
		}
}
