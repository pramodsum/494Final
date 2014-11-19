using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	public GameObject _explosion;
	private int count = 0;
	public Texture[] textures;

	public float cdr = .1f;
	public float cdrRemaining = .1f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		GameObject.Instantiate (_explosion);
	}
}
