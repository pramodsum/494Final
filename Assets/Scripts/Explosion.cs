using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	private int count = 0;
	public Texture[] textures;

	public float cdr = .1f;
	public float cdrRemaining = .1f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

			if (cdrRemaining <= 0)
			{
				cdrRemaining = cdr;
				count = count + 1;
				if (count > 7)
			{
					GameObject.Destroy(gameObject);
				return;
			}
				renderer.material.mainTexture = textures[count];
			}
			else
			{
				cdrRemaining -= Time.deltaTime;
			}
	}
}
