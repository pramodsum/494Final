using UnityEngine;
using System.Collections;

public class AsteroidSpawningStaggered : MonoBehaviour {
	public Transform objectType;
	public float timeTillSpawn;
	public float waitTillSpawn = 10;
	private System.Random random;

	// Use this for initialization
	void Start () {
		random = new System.Random ();
		timeTillSpawn = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (timeTillSpawn < 0)
		{
			timeTillSpawn = waitTillSpawn;
			Vector3 spawnPosition = transform.position;

			float x = random.Next((int)transform.collider.bounds.min.x, (int)transform.collider.bounds.max.x);
			float y = random.Next((int)transform.collider.bounds.min.y, (int)transform.collider.bounds.max.y);

			spawnPosition.x = x;
			spawnPosition.y = y;

			Instantiate(objectType,spawnPosition, Quaternion.identity);
		}
		else
			timeTillSpawn -= Time.deltaTime;
	}
}
