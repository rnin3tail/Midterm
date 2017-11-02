using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFollow : MonoBehaviour {

	Transform target;//set target from inspector instead of looking in Update
	public float speed = 3f;
	public Transform enemyBulletPoint;
	public GameObject enemyBullet;

	public float spawnTime;
	public float spawnTimeRandom;
	private float spawnTimer;

	public AudioClip shotsfx;
	AudioSource audio;
	private SpriteRenderer thisRender;

	float someScale;


	void Start () {
		ResetSpawnTimer ();
		audio = gameObject.GetComponent<AudioSource> ();
		shotsfx = audio.clip;
		thisRender = GetComponent<SpriteRenderer> ();
		//oldVal = transform.position.x;
		someScale = transform.localScale.x;
	}

	void Update(){
		target = GameObject.FindWithTag ("Player").transform;

		//rotate to look at the player
		//transform.LookAt(target.position);
		//transform.Rotate(new Vector3(0, -90, 0),Space.Self);//correcting the original rotation

//This seems to work better. However, the ships go in a circle around  the player. If you get close they shoot.
		Vector3 dir = target.position - transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;


		if (this.gameObject.tag == "Enemy") {
			
			angle = angle - 90f;
			transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);

		//move towards the player
		if (Vector3.Distance (transform.position, target.position) > 7f) { //move if distance from target is greater than 15
			transform.Translate (new Vector3 (speed * Time.deltaTime, speed * Time.deltaTime, 0));
		}
			
		if (Vector3.Distance (transform.position, target.position) <= 15f) { 
			spawnTimer -= Time.deltaTime;
			if (spawnTimer <= 0.0f) {
				audio.PlayOneShot (shotsfx, 1.0f);

				Debug.Log (spawnTimer);
				ResetSpawnTimer ();
			}
			}
		}

		if (this.gameObject.tag == "Enemy2") {
			if (Vector3.Distance (transform.position, target.position) < 15f) { //move if distance from target is greater than 15
				transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);	

				if (target.position.x > transform.position.x) {
					Debug.Log (target.position.x);
					Debug.Log (transform.position.x);
					GetComponent<SpriteRenderer> ().flipX = false;
				} else {
					GetComponent<SpriteRenderer> ().flipX = true;

				}
				//if (oldVal < transform.position.x) {
				//	thisRender.flipX = true;
				//	oldVal = transform.position.x;
				//}

			}
		}

	}
	void ResetSpawnTimer()
	{
		spawnTimer = (float)(spawnTime + Random.Range(0, spawnTimeRandom*100)/100.0);
	}


}