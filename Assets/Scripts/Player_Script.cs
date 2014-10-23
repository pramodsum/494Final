using UnityEngine;
using System.Collections;

public class Player_Script : MonoBehaviour {

	public static int _health = 10;
	public static int _lives = 1;
	public static int _score = 0;

	public Vector3 velocity;

	public float forceModifier = 100;

	public bool godMode = false;

	private bool isUpPressed = false;
	private bool isDownPressed = false;
	private bool isLeftPressed = false;
	private bool isRightPressed = false;	
	private bool isWPressed = false;
	private bool isSPressed = false;
	private bool isAPressed = false;
	private bool isDPressed = false;
	private bool isMousePressed = false;

	public float shotCoolDownRemaining = 0f;
	public float shotCoolDown = 10f;

	public float knockbackRemaining = 0f;
	public float knockBack = 100f;
	private Vector3 knockBackVector;

	public Transform shot;

	private Quaternion rot;
	// Use this for initialization
	void Start () {
		rot = transform.rotation;
		_health = 10;
		_score = 0;
		_lives = 1;
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown(KeyCode.G))
			if (godMode)
				godMode = false;
			else godMode = true;
		

		if (Input.GetKeyDown(KeyCode.UpArrow))
			isUpPressed = true;
		if (Input.GetKeyDown(KeyCode.DownArrow))
			isDownPressed = true;
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			isLeftPressed = true;
		if (Input.GetKeyDown(KeyCode.RightArrow))
			isRightPressed = true;
		if (Input.GetKeyDown(KeyCode.W))
			isWPressed = true;
		if (Input.GetKeyDown(KeyCode.A))
			isAPressed = true;
		if (Input.GetKeyDown(KeyCode.S))
			isSPressed = true;
		if (Input.GetKeyDown(KeyCode.D))
			isDPressed = true;
		if (Input.GetKeyDown(KeyCode.Mouse0))
			isMousePressed = true;
		if (Input.GetKeyUp(KeyCode.UpArrow))
			isUpPressed = false;
		if (Input.GetKeyUp(KeyCode.DownArrow))
			isDownPressed = false;
		if (Input.GetKeyUp(KeyCode.LeftArrow))
			isLeftPressed = false;
		if (Input.GetKeyUp(KeyCode.RightArrow))
			isRightPressed = false;
		if (Input.GetKeyUp(KeyCode.W))
			isWPressed = false;
		if (Input.GetKeyUp(KeyCode.A))
			isAPressed = false;
		if (Input.GetKeyUp(KeyCode.S))
			isSPressed = false;
		if (Input.GetKeyUp(KeyCode.D))
			isDPressed = false;
		if (Input.GetKeyUp(KeyCode.Mouse0))
			isMousePressed = false;

		transform.rotation = rot;
		Vector3 vect = transform.position;
		vect.z = 0;
		transform.position = vect;
		
		if (shotCoolDownRemaining > 0)
			shotCoolDownRemaining -= Time.deltaTime;
		if (shotCoolDownRemaining < 0)
			shotCoolDownRemaining = 0;
		
		if (knockbackRemaining > 0)
		{
			rigidbody.AddForce(knockBackVector);
			knockbackRemaining -= Time.deltaTime;
			if (knockbackRemaining < 0) knockbackRemaining = 0;
			return;
		}

		if (isUpPressed || isWPressed)
			rigidbody.AddForce(Vector3.up * forceModifier);
		if (isDownPressed || isSPressed)
			rigidbody.AddForce(Vector3.down * forceModifier);
		if (isLeftPressed || isAPressed)
			rigidbody.AddForce(Vector3.left * forceModifier);
		if (isRightPressed || isDPressed)
			rigidbody.AddForce(Vector3.right * forceModifier);
		if (isMousePressed)
			if (shotCoolDownRemaining <= 0)
				FIRE ();
	}

	void FIRE()
	{
		Vector3 pos1;//top-left
		pos1.x = transform.collider.bounds.min.x;
		pos1.y = transform.collider.bounds.max.y;
		pos1.z = transform.collider.bounds.max.z;
		Vector3 pos2;//top-right
		pos2.x = transform.collider.bounds.max.x;
		pos2.y = transform.collider.bounds.max.y;
		pos2.z = transform.collider.bounds.max.z;
		Vector3 pos3;//bottom-left
		pos3.x = transform.collider.bounds.min.x;
		pos3.y = transform.collider.bounds.min.y;
		pos3.z = transform.collider.bounds.max.z;
		Vector3 pos4;//bottom-right
		pos4.x = transform.collider.bounds.max.x;
		pos4.y = transform.collider.bounds.min.y;
		pos4.z = transform.collider.bounds.max.z;
		Instantiate (shot, pos1, Quaternion.identity);
		Instantiate (shot, pos2, Quaternion.identity);
		Instantiate (shot, pos3, Quaternion.identity);
		Instantiate (shot, pos4, Quaternion.identity);
		shotCoolDownRemaining = shotCoolDown;
	}

	void OnCollisionEnter(Collision collision) 
	{
		if (collision.collider.tag.Equals("Destructible") && knockbackRemaining <= 0 && !godMode)
		{
			print ("Asteroid has collided with ship!");
			//Do kncckback
			rigidbody.velocity = Vector3.zero;
			Vector3 direction = (transform.position - collision.collider.transform.position).normalized;
			direction.z = 0;
			direction *= forceModifier / 2;
			knockBackVector = direction;
			rigidbody.AddForce(knockBackVector);
			knockbackRemaining = knockBack;
			_health--;
			if (_health == 0)
				Application.LoadLevel(0);
		}    
	}

	public string getAttributeByName(string s)
	{
		if (s.Equals ("_health"))
		    return ""+_health;
		else if (s.Equals("_lives"))
			return ""+_lives;
		else if (s.Equals("_score"))
			return ""+_score;
		else 
			return null;
	}
}
