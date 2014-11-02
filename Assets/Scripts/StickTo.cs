using UnityEngine;
using System.Collections;

public class StickTo : MonoBehaviour {
	public Transform stickee;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = stickee.position;
	}
}
