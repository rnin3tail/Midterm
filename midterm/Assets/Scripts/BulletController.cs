using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletController : MonoBehaviour { 

	public float speed;
	public GameObject bullet;
	private PlayerController player;



	public GameObject enemyShipDeath;
	public GameObject meteorDeath;

	AudioSource audio;
	public AudioClip enemyShipSfx;
	public AudioClip meteorSfx;


	void Start () {

// Obtain playercontroller and audio component

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		audio = GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {

		GetComponent<Rigidbody2D> ().AddForce(transform.up * speed);

	}

	void OnTriggerEnter2D (Collider2D other) {
		
//Destroys enemy ships.
		if (other.tag == "Enemy"){
			Destroy (other.gameObject);
		}
		if (other.tag == "enemyShip") {
			audio.PlayOneShot (enemyShipSfx, 1.0f);
			Instantiate (enemyShipDeath, other.transform.position, other.transform.rotation);
			Destroy (other.gameObject);

		}
//Destroys meteors
		if (other.tag == "meteor") {
			audio.PlayOneShot (meteorSfx, 1.0f); 
			Instantiate (meteorDeath, other.transform.position, other.transform.rotation);
			Destroy (other.gameObject);
		}
//Destroys bullet if it hits anything other than an enemy ship or player.
		if (other.tag != "Player" && other.tag != "Bullet") {
			if (other.transform.parent != null) {
				if (other.transform.parent.tag == "Player") {
					return;
				}
			}
			Debug.Log ("Hit: " + other.tag);
			Destroy (this.gameObject);
		}

	}
}
