using UnityEngine;
using System.Collections;

public class Player_Script1 : MonoBehaviour {

	public  int _health = 10;
	public  int _lives = 1;
	public  int _score = 0;

	public int playerNum = 1;

	public Transform camera;

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
	private bool isEnginePressed = false;
	private bool isQPressed = false;
	private bool isMousePressed = false;

	public float shotCoolDownRemaining = 0f;
	public float shotCoolDown = 10f;

	public float knockbackRemaining = 0f;
	public float knockBack = 100f;
	private Vector3 knockBackVector;

	private Transform arrow;
	private Transform otherShip;

	public Transform shot;

	private Quaternion rot;
	// Use this for initialization
	void Start () {
		rot = transform.rotation;
		_health = 10;
		_score = 0;
		_lives = 1;


		if (playerNum == 1)
		{
			otherShip = GameObject.Find ("Ship2").transform;
			camera = GameObject.Find ("Player 1 Camera").transform;
		}
		else if (playerNum == 2)
		{
			otherShip = GameObject.Find ("Ship1").transform;
			camera = GameObject.Find ("Player 2 Camera").transform;
		}
		else camera = GameObject.Find ("Player 1 Camera").transform;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.G))
			if (godMode)
				godMode = false;
			else godMode = true;
		

		if (Input.GetKeyDown(KeyCode.UpArrow) && playerNum == 2)
			isUpPressed = true;
		if (Input.GetKeyDown(KeyCode.DownArrow) && playerNum == 2)
			isDownPressed = true;
		if (Input.GetKeyDown(KeyCode.LeftArrow) && playerNum == 2)
			isLeftPressed = true;
		if (Input.GetKeyDown(KeyCode.RightArrow) && playerNum == 2)
			isRightPressed = true;
		if (Input.GetKeyDown(KeyCode.W) && playerNum == 1)
			isWPressed = true;
		if (Input.GetKeyDown(KeyCode.A) && playerNum == 1)
			isAPressed = true;
		if (Input.GetKeyDown(KeyCode.S) && playerNum == 1)
			isSPressed = true;
		if (Input.GetKeyDown(KeyCode.D) && playerNum == 1)
			isDPressed = true;
		if ((Input.GetKeyDown(KeyCode.E) && playerNum == 1) || 
		    (Input.GetKeyDown(KeyCode.Slash) && playerNum == 2))
			isEnginePressed = true;
//		if (Input.GetKeyDown(KeyCode.Q) && playerNum == 1)
//			isQPressed = true;
		if ((Input.GetKeyDown(KeyCode.Mouse0) && playerNum == 1) || 
		    (Input.GetKeyDown(KeyCode.Period) && playerNum == 2))
			isMousePressed = true;
		if ((Input.GetKeyUp(KeyCode.UpArrow) && playerNum == 2))
			isUpPressed = false;
		if (Input.GetKeyUp(KeyCode.DownArrow) && playerNum == 2)
			isDownPressed = false;
		if (Input.GetKeyUp(KeyCode.LeftArrow) && playerNum == 2)
			isLeftPressed = false;
		if (Input.GetKeyUp(KeyCode.RightArrow) && playerNum == 2)
			isRightPressed = false;
		if (Input.GetKeyUp(KeyCode.W) && playerNum == 1)
			isWPressed = false;
		if (Input.GetKeyUp(KeyCode.A) && playerNum == 1)
			isAPressed = false;
		if (Input.GetKeyUp(KeyCode.S) && playerNum == 1)
			isSPressed = false;
		if (Input.GetKeyUp(KeyCode.D) && playerNum == 1)
			isDPressed = false;
		if ((Input.GetKeyUp(KeyCode.E) && playerNum == 1) || 
			(Input.GetKeyUp(KeyCode.Slash) && playerNum == 2))
			isEnginePressed = false;
//		if (Input.GetKeyUp(KeyCode.Q))
//			isQPressed = false;
		if ((Input.GetKeyUp(KeyCode.Mouse0) && playerNum == 1) || 
		    (Input.GetKeyUp(KeyCode.Period) && playerNum == 2))
			isMousePressed = false;

//		transform.rotation = rot;
		Vector3 vect = transform.position;
//		vect.z = 0;
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

		if (true)//Do we want to allow the rotations if no engine?
		{
			if (isUpPressed || isWPressed)
				transform.Rotate(Vector3.right);
			if (isDownPressed || isSPressed)
				transform.Rotate(Vector3.left);
			if (isLeftPressed || isAPressed)
				transform.Rotate(Vector3.up);
			if (isRightPressed || isDPressed)
				transform.Rotate(Vector3.down);

			if (isEnginePressed)
			{
				Vector3 directionVector = (transform.position - camera.position).normalized;
				rigidbody.AddForce(directionVector * forceModifier);
			}

		}
		if (isQPressed) 
		{
			if (shotCoolDownRemaining <= 0)
				FIRE ();
		}
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
		if ((collision.collider.tag.Equals("Asteroid") && knockbackRemaining <= 0 && !godMode))
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

	void OnTriggerEnter(Collider other) 
	{
		if (((other.collider.tag.Equals("PlayerShot") && playerNum == 2) || (other.collider.tag.Equals("PlayerShot2") && playerNum == 1)) && knockbackRemaining <= 0 && !godMode)
		{
			print ("Asteroid has collided with ship!");
			//Do kncckback
			rigidbody.velocity = Vector3.zero;
			Vector3 direction = (transform.position - other.collider.transform.position).normalized;
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

//	void OnDrawGizmos() 
//	{
//			Gizmos.color = Color.red;
//			Gizmos.DrawLine (this.transform.position, otherShip.position);
//	}


	}
