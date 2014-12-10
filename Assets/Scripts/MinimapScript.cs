using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinimapScript : MonoBehaviour {
	public GameObject blueShip;

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
				//I have no idea why this correctly selects which ship to represent using
				//the blue ship and which to use the red one for, but it appears to work
				//every time on my end.
                
                if (_pair.tag.Equals("PlayerShip") && (i == 1 || i == 0))
				{
					_pair.minimapRealizations.Add (
						(Instantiate(blueShip,new Vector3(0,10,0),Quaternion.identity) as GameObject));
                }
				else if (_pair.tag.Equals("StationParent"))
				{
					Quaternion rot = Quaternion.identity;
					if (i == 0)
						rot = new Quaternion(.25f,0,.5f,1);
					if (i == 1)
						rot = new Quaternion(.5f,0,.25f,1);
					if (i == 2)
						rot = new Quaternion(.33f,0,.33f,1);
					if (i == 3)
						rot = new Quaternion(.2f,0,.4f,1);
					if (i == 5)
						rot = new Quaternion(.4f,0,.2f,1);
                    _pair.minimapRealizations.Add (
						(Instantiate(_pair.minimapRepresentation,new Vector3(0,10,0), rot) as GameObject));
				}
				else _pair.minimapRealizations.Add (
					(Instantiate(_pair.minimapRepresentation,new Vector3(0,10,0),Quaternion.identity) as GameObject));
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
				Vector3 centerWorld = GameObject.Find ("Boundary").transform.position;
				Vector3 between = obj.transform.position - centerWorld;
				//scale vector down based on relative scale of minimap
				float ratio = transform.localScale.x / GameObject.Find ("Boundary").GetComponent<SphereCollider>().radius;
				between *= ratio*5;
				//use x and z to set position of object on minimap
				//set y to put it above the minimap
				Vector3 newPos = new Vector3(between.x, 1, between.z);
				if (obj.tag.Equals("StationParent")) newPos.y = 2;
				if (_count < _pair.minimapRealizations.Count){
					_pair.minimapRealizations[_count].transform.position = newPos;
					if (obj.tag.Equals("Transport"))
					{
						_pair.minimapRealizations[_count].transform.rotation = obj.transform.rotation;
						if (obj.GetComponent<TransportMove>().isAlive == false)
						{
							newPos.y = -5;
							_pair.minimapRealizations[_count].transform.position = newPos;
						}
					}
					else if (obj.tag.Equals("PlayerShip"))
					{
						Vector3 minimapPos = newPos;
						minimapPos.y = 10;
						if (obj.transform.parent.name.Equals("Ship1"))
						{
							GameObject.Find("Ship1").GetComponent<Ship>().minimap.transform.position = minimapPos;
						}
						else if (obj.transform.parent.name.Equals("Ship2"))
						{
							GameObject.Find("Ship2").GetComponent<Ship>().minimap.transform.position = minimapPos;
						}
						else if (obj.transform.parent.name.Equals("Ship3"))
						{
							GameObject.Find("Ship3").GetComponent<Ship>().minimap.transform.position = minimapPos;
						}
						else if (obj.transform.parent.name.Equals("Ship4"))
						{
							GameObject.Find("Ship4").GetComponent<Ship>().minimap.transform.position = minimapPos;
						}

//						if (obj.transform.parent.name.Equals("Ship2") || obj.transform.parent.name.Equals("Ship4"))
//						{
////							print ("hey");
//							_pair.minimapRealizations[_count].renderer.material.color = Color.blue;
//						}
						if (obj.transform.parent.GetComponent<Ship>().health <= 0)
						{
							newPos.y = -5;
							_pair.minimapRealizations[_count].transform.position = newPos;
						}
						_pair.minimapRealizations[_count].transform.rotation = obj.transform.parent.transform.rotation;
                        
                    }
					else if (obj.tag.Equals("StationParent"))
					{
						if (obj.GetComponent<Station_Control>().inControl)
						{
							print ("captured");
//							Transform trans = obj.transform.GetChild(0);
							foreach (Transform trans in _pair.minimapRealizations[_count].transform)
							{
								trans.renderer.material.color = obj.transform.GetChild(0).renderer.material.color;
                            }
//							_pair.minimapRealizations[_count].transform.renderer.material.color = obj.transform.GetChild(0).renderer.material.color;
						}
						else 
						{
							foreach (Transform trans in _pair.minimapRealizations[_count].transform)
							{
								trans.renderer.material.color = Color.white;
                       		}
//							_pair.minimapRealizations[_count].transform.renderer.material.color = Color.white;
						}
                    }
					else if (obj.tag.Equals("AIShip"))
					{
						_pair.minimapRealizations[_count].transform.rotation = obj.transform.rotation;
					}
					else if (obj.tag.Equals("Planet"))
					{
//						print(obj.name)
						if (obj.transform.name.Equals("Neptune"))
						{
							//							print ("hey");
							_pair.minimapRealizations[_count].renderer.material.color = Color.blue;
						}

					}
					_count++;
				}
			}
			for (int j = _count; j < _pair.count; j++)
			{
				_pair.minimapRealizations[j].transform.position = new Vector3(0,-5,0);
			}
			//reset positions of all realizations that have not been used to be below
			//the minimap at 0,0,0
		}
	}
}
