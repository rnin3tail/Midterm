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
//Animator
	private Animator anim;
	private bool movingUp;
	private bool movingLeft;
	private bool movingRight;
	private bool movingDown;

//Plays audio
	public AudioClip shotsfx;
	public AudioClip damagesfx;
	public AudioClip deathsfx;
	public AudioClip lifeUpsfx;
	AudioSource audio;

//*** health system
	public int curHealth;
	public int maxHealth = 3;
	public bool deathCheck;
	public bool hurt;


// gamemaster for points
	private GameMaster gm;

// Ghost turns into person...
	public GameObject ghost;
	public GameObject person;

// Key/Door stuff
	public bool keyObtained;
	public GameObject keyText;
	public GameObject keyImage;
	public GameObject door;
	public AudioClip doorsfx;

	void Start () {
		keyObtained = false;

		curHealth = maxHealth;

		deathCheck = false;
		hurt = false;
		paused = false; 
		canControl = false;
		movingUp = false;
		movingLeft = false;
		movingRight = false;
		movingDown = false;


		speed = 10f; 
		maxSpeed = 50f;
		rigiBody = gameObject.GetComponent<Rigidbody2D> (); // Gain access to Ship's body component
		anim = gameObject.GetComponent<Animator>();
		audio = gameObject.GetComponent<AudioSource> ();
		gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster> (); 


//four walls in the level
		//topWall = GameObject.FindGameObjectWithTag ("TWall");
		//botWall = GameObject.FindGameObjectWithTag ("BWall");
		//leftWall = GameObject.FindGameObjectWithTag ("LWall");
		//rightWall = GameObject.FindGameObjectWithTag ("RWall");
// turn off the game over screen 
		gameOver.SetActive (false);
	}

	void Update () {

	if (canControl == true) {
			
// *** adds animation to deaths and being damaged.
		anim.SetBool ("isDead", deathCheck);
		anim.SetBool ("isDamaged", hurt);
	
		//Turns hurt animation off after it runs once
			if (hurt = true) {
				hurt = false;
			}

// Move Left
		if (Input.GetKey (KeyCode.LeftArrow)) { 
				rigiBody.AddForce (transform.right * 25f * -speed);
				anim.SetBool ("movingLeft", true);
				anim.SetBool("movingRight",false);
	
		}
			if (Input.GetKeyUp (KeyCode.LeftArrow)) {
				rigiBody.drag = 10f;
				anim.SetBool ("movingLeft", false);
			}
		
// Move Right
		if (Input.GetKey (KeyCode.RightArrow)) { 
				rigiBody.AddForce (transform.right * 25f * speed);
				anim.SetBool ("movingRight", true);
				anim.SetBool ("movingLeft", false);
		}
			if (Input.GetKeyUp (KeyCode.RightArrow)) {
				rigiBody.drag = 10f;
				anim.SetBool ("movingRight", false);
			}
// Move Up
		if (Input.GetKey (KeyCode.UpArrow)) {
			rigiBody.drag = 2f;
			rigiBody.AddForce (transform.up * speed);
			anim.SetBool ("movingUp", true);
			anim.SetBool ("movingDown", false);
		}
// Slow down once UP key is released
		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			rigiBody.drag = 10f;
			anim.SetBool ("movingUp", false);
		}

//Brakes and move backwards
		if (Input.GetKey (KeyCode.DownArrow)) {
				rigiBody.drag = 2f;
			rigiBody.AddForce (transform.up * -speed);
				anim.SetBool ("movingDown", true);
				anim.SetBool ("movingUp", false);
		}
			if (Input.GetKeyUp (KeyCode.DownArrow)) {
				rigiBody.drag = 10f;
				anim.SetBool ("movingDown", false);
		}
		
// Shoot Gun. Disables shooting while in pause mode.
	//	if (Input.GetKeyDown (KeyCode.Space)) {
			//Instantiate (bullet, bulletPoint.position, bulletPoint.rotation); 
	//		audio.PlayOneShot (shotsfx, 1.0f);
	//	}

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
		if (col.tag == "Enemy" || col.tag == "Enemy2") {
			Damage (1);
			//audio.PlayOneShot (enemyShipSfx, 1.0f);
			col.GetComponent<Animator> ().Play ("gruntDeath");
			Destroy (col.gameObject,1f);
		}
		if (col.tag == "Heart") {
			curHealth += 1;
			audio.PlayOneShot (lifeUpsfx, 1.0f);
			Destroy (col.gameObject);
		}
		if (col.tag == "Soul") {
			gm.points += 1;
			audio.PlayOneShot (lifeUpsfx, 1.0f);
			Destroy (col.gameObject);
		}
		if (col.tag == "Key") {
			keyObtained = true;
			keyImage.SetActive (true);
			audio.PlayOneShot (lifeUpsfx, 1.0f);
			Destroy (col.gameObject);
		}
		if (col.tag == "Door") {
			if (keyObtained == true) {
				audio.PlayOneShot (doorsfx, 1.0f);
				door.GetComponent<Animator> ().SetBool ("keyPresent", keyObtained);

				StartCoroutine ("Wait");
// State that the key needs to be obtained
			} else {
				keyText.SetActive (true);
				StartCoroutine ("Wait");
			}
		}
		if (col.tag == "Shrine") {
			if (gm.points >= 10) {
				Instantiate (person, ghost.transform.position, col.transform.rotation);
				Destroy (ghost);
			

			} else {
				keyText.SetActive (true);
				keyText.GetComponent<UnityEngine.UI.Text> ().text = "Need Souls";
				StartCoroutine ("Wait");

			}
		
		}


	}
//Player has died
	void Death () {
		canControl = false;

		if (deathCheck)
		{
			Debug.Log("Player is dead!");
			gameOver.SetActive(true);
			canControl = false;
			Time.timeScale = 0; //pause everything.
		}
	}
//Wait a bit before death
	IEnumerator DelayedRespawn () {
		//gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (2f);
		Death();
	}

	IEnumerator Wait () {
		yield return new WaitForSeconds (3f);

		if (keyText.active) {
			keyText.SetActive (false);
		}
		if (keyObtained == true) {
			door.SetActive (false);
		}
	}

// Player was hit.
	public void Damage (int dmg) {

		if (curHealth > 1) {  // if player has more than 1 health than he is damaged
			hurt = true;
			audio.PlayOneShot (damagesfx, 1.0f);

		} else { // otherwise the player will explode.
			deathCheck = true;
			audio.PlayOneShot (deathsfx, 1.0f);

		}
		curHealth -= dmg;

	}
	// void OnGUI() {
	//	if (keyObtained == true) {
	//		GUI.Label (new Rect (140, Screen.height - 50, Screen.width - 300, 120), ("Key is required!"));
	//	}
	//}


}
