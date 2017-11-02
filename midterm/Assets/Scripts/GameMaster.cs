using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

	private PlayerController player;


	public int points;	
	public Text pointsText;

	private int lives;
	public Text livesText;
	public Text time;

//variables required to spawn a life after a certain amount of time. 
	public float spawnTime;
	private float spawnTimer;
// choose x and y axis to spawn things.
	public float xmin;
	public float xmax;
	public float ymin;
	public float ymax;

	void Start () {


		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		lives = player.maxHealth;
	}

// Update lives and points amount.
	void Update () {

		lives = player.curHealth;
		pointsText.text = ("Souls \n" + points);
		livesText.text = ("x " + lives);
	}


}