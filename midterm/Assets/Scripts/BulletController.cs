using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletController : MonoBehaviour { 

	public float speed;
	public GameObject bullet;
	private PlayerController player;

// GM for point system
	private GameMaster gm;

//audio
	AudioSource audio;
	public AudioClip enemyShipSfx;
	public AudioClip meteorSfx;
//particle 
	public GameObject meteorDeath;

	void Start () {

// Obtain GameMaster
		gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster> (); 

// Obtain playercontroller and audio component
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		audio = gameObject.GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {

		GetComponent<Rigidbody2D> ().AddForce(transform.up * speed);

	}

	void OnTriggerEnter2D (Collider2D other) {

//Destroys enemy ships.
		if (other.tag == "Enemy") {
			audio.PlayOneShot (enemyShipSfx, 1.0f);
			other.GetComponent<Animator> ().Play ("gruntDeath");
			gm.points += 100;
			other.GetComponent<Collider2D> ().enabled = false;
			other.GetComponent<enemyFollow> ().enabled = false;
			Destroy (other.gameObject,1f);

		}
// etc etc, New point values and effects for different enemies.
		if (other.tag == "Enemy2") {
			audio.PlayOneShot (enemyShipSfx, 1.0f);
			other.GetComponent<Animator> ().Play ("gruntDeath");
			gm.points += 100;
			other.GetComponent<Collider2D> ().enabled = false;
			other.GetComponent<enemyFollow> ().enabled = false;
			Destroy (other.gameObject,1f);

		}
//Destroys meteors
		if (other.tag == "Rock") {
			audio.PlayOneShot (meteorSfx, 1.0f);
			Instantiate (meteorDeath, other.transform.position, other.transform.rotation);
			gm.points += 50;
			other.GetComponent<Collider2D> ().enabled = false;
			Destroy (other.gameObject);
		}

//Preserves bullet if it hits another bullet or the player. Otherwise, destroys it.
		if (other.tag != "Player" && other.tag != "Bullet") {
			if (other.transform.parent != null) {
				if (other.transform.parent.tag == "Player") {
					return;
				}
			}

			Debug.Log ("Has Hit: " + other.tag);

			//StartCoroutine ("DelayedDestruction");
			this.GetComponent<Collider2D> ().enabled = false;
			this.GetComponent<SpriteRenderer> ().enabled = false;
			Destroy(this.gameObject, 1f); // enemyShipSfx.length makes the game wait that amount of seconds before destrying. Gives a chance for audio to play. 

		}

	}

	IEnumerator DelayedDestruction () {
		yield return new WaitForSeconds (1f);	
	}

}
