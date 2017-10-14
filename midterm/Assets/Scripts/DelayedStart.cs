using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedStart : MonoBehaviour {

	public GameObject countDown;
	public PlayerController pc;

// Whenever the level starts it goes straight to a StartDelay method which counts down 3 seconds before starting.
	void Start () {
		StartCoroutine ("StartDelay");
		pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}
		
	IEnumerator StartDelay () {
		Time.timeScale = 0;
		float pauseTime = Time.realtimeSinceStartup + 3f;
		while (Time.realtimeSinceStartup < pauseTime)

			yield return 0;

		countDown.gameObject.SetActive (false);
		pc.canControl = true;
		Time.timeScale = 1;
	}
}
