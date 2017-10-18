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

	void Start () {

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		lives = player.maxHealth;
	}

// Update lives and points amount.
	void Update () {

		lives = player.curHealth;
		pointsText.text = ("Points \n" + points);
		livesText.text = ("x " + lives);
	}
}