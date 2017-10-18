using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleMovement : MonoBehaviour {

	Transform target;//will be set to player's position
	public float speed;



	// Use this for initialization
	void Start () {

		// turn towards player
		target = GameObject.FindWithTag ("Player").transform;
		Vector3 dir = target.position - transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

	}
	
	// Update is called once per frame
	void Update () {

		//move towards player
		transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
	}

	void OnTriggerEnter2D (Collider2D col) {
		Debug.Log ("collision");
		if (col.tag=="Enemy") {
			Debug.Log ("Rock should destroy ship");
			Destroy (col.gameObject);
		}
	}

}
