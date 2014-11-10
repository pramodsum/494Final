using UnityEngine;
using System.Collections;

public class AsteroidSpawning : MonoBehaviour
{
		public Transform objectType;
		public float timeTillSpawn;
		public float waitTillSpawnMin = .5f;
		public float waitTillSpawn = 10;
		private System.Random random;
		public int numSpawned;
		public int numToSpawn;
		public float waitTimeNext = 0;
		public int waitTimeUpdateInterval = 10; //every 10 seconds increase spawn rate

		public float decreaseFactor = .1f;

		// Use this for initialization
		void Start ()
		{
				numSpawned = 0;
				random = new System.Random ();
				timeTillSpawn = 0;

				waitTillSpawn = 1;
				waitTimeNext = 0;
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (timeTillSpawn < 0) {
						timeTillSpawn = waitTillSpawn;
						Vector3 spawnPosition = transform.position;

						float x = random.Next ((int)transform.collider.bounds.min.x, (int)transform.collider.bounds.max.x);
						float y = random.Next ((int)transform.collider.bounds.min.y, (int)transform.collider.bounds.max.y);

						spawnPosition.x = x;
						spawnPosition.y = y;

						Instantiate (objectType, spawnPosition, Quaternion.identity);
						numSpawned++;
//			if (numSpawned = 10)
				} else
						timeTillSpawn -= Time.deltaTime;

				//EVERY 10 SECONDS INCREASE THE SPAWN RATE
				waitTimeNext += Time.deltaTime;
				if (waitTimeNext >= waitTimeUpdateInterval) {
						if (waitTillSpawn < .1)
								decreaseFactor = .01f;

						waitTimeNext = 0;
						waitTillSpawn -= decreaseFactor;
						if (waitTillSpawn < waitTillSpawnMin)
								waitTillSpawn = waitTillSpawnMin;

						if (timeTillSpawn < waitTillSpawnMin)
								timeTillSpawn = waitTillSpawnMin;
				}
		}
}
