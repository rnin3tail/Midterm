using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float maxSpeed;

	private Rigidbody2D rigiBody;
	//private Animator anim;

	public AudioClip shotsfx;
	AudioSource audio;

//*** health system
	public int curHealth;
	public int maxHealth = 3;
	public bool deathCheck;
	public bool hurt;
//Boundaries
	public GameObject topWall;
	public GameObject botWall;
	public GameObject leftWall;
	public GameObject rightWall;
//laser beam
	public Transform bulletPoint;
	public GameObject bullet;

	void Start () {
		curHealth = maxHealth;

		speed = 10f; 
		maxSpeed = 50f;
		rigiBody = gameObject.GetComponent<Rigidbody2D> (); // Gain access to Ship's body component
		//anim = gameObject.GetComponent<Animator>();
		audio = gameObject.GetComponent<AudioSource> ();

		topWall = GameObject.FindGameObjectWithTag ("TWall");
		botWall = GameObject.FindGameObjectWithTag ("BWall");
		leftWall = GameObject.FindGameObjectWithTag ("LWall");
		rightWall = GameObject.FindGameObjectWithTag ("RWall");

	}

	void Update () {
		
// *** adds animation to deaths and being damaged.
		//anim.SetBool ("isAlive", deathCheck);
		//anim.SetBool ("isDamaged", hurt);

// Turn Left
		if (Input.GetKey (KeyCode.LeftArrow)) { 
			//rigiBody.velocity = new Vector2 (rigiBody.velocity.x - speed, rigiBody.velocity.y);
			transform.Rotate (Vector3.forward * 10f);
		}
		//if (Input.GetKeyUp (KeyCode.LeftArrow)) { 
			//rigiBody.velocity = new Vector2 (rigiBody.velocity.x + speed, rigiBody.velocity.y);
		//}

// Turn right
		if (Input.GetKey (KeyCode.RightArrow)) { 
			//rigiBody.velocity = new Vector2 (rigiBody.velocity.x + speed, rigiBody.velocity.y);
			transform.Rotate (Vector3.forward * -10f);
		}
		//if (Input.GetKeyUp (KeyCode.RightArrow)) { 
		//	//rigiBody.velocity = new Vector2 (rigiBody.velocity.x - speed, rigiBody.velocity.y);
		//}

// Accelerate in direction faced
		if (Input.GetKey (KeyCode.UpArrow)) {
			//rigiBody.velocity = new Vector2 (rigiBody.velocity.x, rigiBody.velocity.y + speed);
			rigiBody.drag = 0f;
			rigiBody.AddForce(transform.up * speed);
		}

// Slow down once UP key is released
		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			rigiBody.drag = 1f;

			//transform.position = transform.position;
			//rigiBody.velocity = new Vector2 (rigiBody.velocity.x,rigiBody.velocity.y);
			//rigiBody.AddForce(transform.up * 0f);
			//rigiBody.velocity = new Vector2 (rigiBody.velocity.x, rigiBody.velocity.y - speed);
		}

//Brakes and move backwards
		if (Input.GetKey (KeyCode.DownArrow)) {
			rigiBody.AddForce (transform.up * -speed);
			//rigiBody.velocity = new Vector2 (rigiBody.velocity.x, rigiBody.velocity.y - speed);
		}

		//if (Input.GetKeyUp (KeyCode.DownArrow)) {
		//	rigiBody.velocity = new Vector2 (rigiBody.velocity.x, rigiBody.velocity.y + speed);
		//}
// Shoot Gun
		if (Input.GetKeyDown (KeyCode.Space)) {
			Instantiate (bullet,bulletPoint.position,bulletPoint.rotation); 
			audio.PlayOneShot(shotsfx,1.0f);
		}	
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "BWall") {
			Debug.Log ("Hit Wall");
			transform.position = new Vector2 (transform.position.x, topWall.transform.position.y-3f);
		}
		if (col.tag == "TWall") {
			Debug.Log ("Hit Wall");
			transform.position = new Vector2 (transform.position.x, botWall.transform.position.y+3f);
		}
		if (col.tag == "LWall") {
			Debug.Log ("Hit Wall");
			transform.position = new Vector2 (rightWall.transform.position.x-3f, transform.position.y);
		}
		if (col.tag == "RWall") {
			Debug.Log ("Hit Wall");
			transform.position = new Vector2 (leftWall.transform.position.x+3f, transform.position.y);
		}
		if (col.tag == "Enemy") {
			Destroy (this.gameObject);

		}
		if (col.tag == "Rock") {
			Destroy (this.gameObject);
		}
	}

}
