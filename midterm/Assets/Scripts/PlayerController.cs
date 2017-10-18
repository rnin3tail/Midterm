using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

// Pause and Game Over screen. canControl lets us choose when the player has control of the ship.
	public GameObject gameOver;
	public GameObject pauseScreen;
	private bool paused;
	public bool canControl;

//Movement speed and control
	public float speed;
	public float maxSpeed;
	private Rigidbody2D rigiBody;
	//private Animator anim;

//Plays audio
	public AudioClip shotsfx;
	public AudioClip damagesfx;
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

		deathCheck = false;
		paused = false; 
		canControl = false;

		speed = 10f; 
		maxSpeed = 50f;
		rigiBody = gameObject.GetComponent<Rigidbody2D> (); // Gain access to Ship's body component
		//anim = gameObject.GetComponent<Animator>();
		audio = gameObject.GetComponent<AudioSource> ();
//four walls in the level
		topWall = GameObject.FindGameObjectWithTag ("TWall");
		botWall = GameObject.FindGameObjectWithTag ("BWall");
		leftWall = GameObject.FindGameObjectWithTag ("LWall");
		rightWall = GameObject.FindGameObjectWithTag ("RWall");
// turn off the game over screen 
		gameOver.SetActive (false);

	}

	void Update () {

	if (canControl == true) {
			
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
			rigiBody.AddForce (transform.up * speed);
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

// Shoot Gun. Disables shooting while in pause mode.
		if (Input.GetKeyDown (KeyCode.Space)) {
			Instantiate (bullet, bulletPoint.position, bulletPoint.rotation); 
			audio.PlayOneShot (shotsfx, 1.0f);
		}
	}
// Pause Menu. Also disables shooting.
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (paused == false) {
				Time.timeScale = 0.0f;
				pauseScreen.SetActive (true);
				paused = true;
				canControl = false;
				return;
			}
			if (paused == true) {
				Time.timeScale = 1.0f;
				pauseScreen.SetActive (false);
				paused = false;
				canControl = true;
				return;
			}
		}
//Checks to see if player is dead
		if (curHealth <= 0) {
			StartCoroutine ("DelayedRespawn");
		}
	
}
// Collisions... Wall collisions cause ship to transform to the opposite wall.
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
			Damage (1);
			Destroy (col.gameObject);

		}
		if (col.tag == "Rock") {
			Damage (1);
			Destroy (col.gameObject);

		}
	}
//Player has died
	void Death () {
		deathCheck = true;
		gameOver.SetActive(true);
		canControl = false;

		if (deathCheck)
		{
			Debug.Log("Player is dead!");
			Time.timeScale = 0; //pause everything.
		}
	}
//Wait a bit before death
	IEnumerator DelayedRespawn () {
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (.3f);
		Death();

	}
// Player was hit.
	public void Damage (int dmg) {
		audio.PlayOneShot (damagesfx, 1.0f);
		curHealth -= dmg;

	}

}
