﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBulletController : MonoBehaviour {

	public float speed;
	public GameObject bullet;
	private PlayerController player;


// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}
	
// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D> ().AddForce(transform.up * speed);
	}

	void OnTriggerEnter2D (Collider2D other) {
	
		if (other.tag == "Player"){
			player.Damage (1);
		}

//Preserves bullet if it hits an Enemy (including itself) and other bullets. Otherwise, destroys it.
		if (other.tag != "Enemy" && other.tag != "Bullet"  && other.tag != "Boss") {
			if (other.transform.parent != null) {
				if (other.transform.parent.tag == "Enemy") {
					return;
				}
				if (other.transform.parent.tag == "Boss") {
					return;
				}
			}
			Debug.Log ("Hit: " + other.tag);
			Destroy (this.gameObject);
		}
	}
}
