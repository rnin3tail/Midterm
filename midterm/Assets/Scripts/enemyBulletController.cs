using System.Collections;
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
			Destroy (other.gameObject);
		}

		if (other.tag != "Enemy" && other.tag != "Bullet") {
			if (other.transform.parent != null) {
				if (other.transform.parent.tag == "Enemy") {
					return;
				}
			}
			Debug.Log ("Hit: " + other.tag);
			Destroy (this.gameObject);
		}
	}
}
