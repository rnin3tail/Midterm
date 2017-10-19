using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public int Bosshealth = 25;
	public GameObject BossEnemy;
	AudioSource audio;
	public AudioClip enemyShipSfx;

	// Use this for initialization
	void Start () {

		BossEnemy = GameObject.FindWithTag ("Boss");
		audio = gameObject.GetComponent<AudioSource> ();

	}
	
	void Update(){
		

		if (Bosshealth == 0) {
			Destroy (BossEnemy);
			audio.PlayOneShot (enemyShipSfx, 1.0f);
		}


	}


	void OnTriggerEnter2D (Collider2D other) {
	
		if (other.tag == "PBullet") {
			Bosshealth -= 1;
		}
	}
}