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


	void Start () {
		ResetSpawnTimer ();
	}

	void Update(){
		target = GameObject.FindWithTag ("Player").transform;


		//rotate to look at the player
		//transform.LookAt(target.position);
		//transform.Rotate(new Vector3(0, -90, 0),Space.Self);//correcting the original rotation

//This seems to work better. However, the ships go in a circle around  the player. If you get close they shoot.
		Vector3 dir = target.position - transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		angle = angle - 90f;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		//move towards the player
		if (Vector3.Distance (transform.position, target.position) > 8f) {//move if distance from target is greater than 7
			transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
		}
			
		if (Vector3.Distance (transform.position, target.position) <= 8f) {
			spawnTimer -= Time.deltaTime;
			Debug.Log (spawnTimer);
			if (spawnTimer <= 0.0f) {
				Instantiate (enemyBullet, enemyBulletPoint.position, enemyBulletPoint.rotation);
				Debug.Log (spawnTimer);
				ResetSpawnTimer ();
			}
		}

	}
	void ResetSpawnTimer()
	{
		spawnTimer = (float)(spawnTime + Random.Range(0, spawnTimeRandom*100)/100.0);
	}



}