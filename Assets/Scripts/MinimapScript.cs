using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinimapScript : MonoBehaviour {

	[System.Serializable]
	public class pair
	{
		public string tag;
		public GameObject minimapRepresentation;
		public int count;
		public List<GameObject> minimapRealizations;
	}
	public List<pair> objectsToTrack;

	// Use this for initialization
	void Start () {
		foreach (pair _pair in objectsToTrack)
		{
			for (int i = 0; i < _pair.count; i++)
			{
				Instantiate(_pair.minimapRepresentation,new Vector3(0,10,0),Quaternion.identity);
			}
		}
	}
	//BOUNDARY 600 RADIUS
	// Update is called once per frame
	void Update () {

		foreach (pair _pair in objectsToTrack)
		{
			int _count = 0;
			foreach(GameObject obj in GameObject.FindGameObjectsWithTag(_pair.tag))
			{
//				transform.localScale.x;
//				transform.localScale.z;
				//get vector from center of boundary to the object
				//scale vector down based on relative scale of minimap
				//use x and z to set position of object on minimap
				//set y to put it above the minimap
//				_pair.minimapRealizations[_count].transform.position;
				_count++;
			}
			//reset positions of all realizations that have not been used to be below
			//the minimap at 0,0,0
		}
	}
}
