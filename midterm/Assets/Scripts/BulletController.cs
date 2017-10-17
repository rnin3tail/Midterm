using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletController : MonoBehaviour { 

	public float speed;
	public GameObject bullet;
	private PlayerController player;
	int bossHealth = 2;
	//public GameObject boss;

// GM for point system
	private GameMaster gm;


	public GameObject enemyShipDeath;
	public GameObject meteorDeath;

	AudioSource audio;
	public AudioClip enemyShipSfx;
	public AudioClip meteorSfx;


	void Start () {

// Obtain GameMaster
		gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster> (); 

// Obtain playercontroller and audio component
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		audio = GetComponent<AudioSource> ();
		//boss = GameObject.FindGameObjectWithTag ("BossBattle");
	}
	
	// Update is called once per frame
	void Update () {

		GetComponent<Rigidbody2D> ().AddForce(transform.up * speed);

		//if (bossHealth <= 0) {
			//Destroy (boss);
		//}
	}

	void OnTriggerEnter2D (Collider2D other) {
		
//Destroys enemy ships.
		if (other.tag == "Enemy") {
			audio.PlayOneShot (enemyShipSfx, 1.0f);
			gm.points += 100;
			//Instantiate (enemyShipDeath, other.transform.position, other.transform.rotation);
			Destroy (other.gameObject);

		}
// etc etc, New point values and effects for different enemies.
		if (other.tag == "enemy2") {
			audio.PlayOneShot (enemyShipSfx, 1.0f);
			gm.points += 100;
			//Instantiate (enemyShipDeath, other.transform.position, other.transform.rotation);
			Destroy (other.gameObject);

		}
		if (other.tag == "BossBattle") {
			audio.PlayOneShot (enemyShipSfx, 1.0f);
			bossHealth -= 1;
			gm.points += 20; //testing to see if this registers
			Debug.Log ("Current bossDamage is: " + bossHealth);
			//bossDeath();
		}
		if (other.tag == "BossBattle" && bossHealth == 0) {
			Destroy (other.gameObject);
			gm.points += 300;
		}
//Destroys meteors
		if (other.tag == "Rock") {
			audio.PlayOneShot (meteorSfx, 1.0f); 
			gm.points += 50;
			//Instantiate (meteorDeath, other.transform.position, other.transform.rotation);
			Destroy (other.gameObject);
		}
//Preserves bullet if it hits another bullet or the player. Otherwise, destroys it.
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
	//public void bossDeath(){
	//	bossHealth -= 1;
	//	if (bossHealth < 0) {
	//		Destroy (boss);
	//	}
	//}
}
