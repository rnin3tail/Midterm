﻿/*
  * This confidential and proprietary software may be used only as
  * authorised by a licensing agreement from ARM Limited
  * (C) COPYRIGHT 2014 ARM Limited
  * ALL RIGHTS RESERVED
  * The entire notice above must be reproduced on all authorised
  * copies and copies may only be made to the extent permitted
  * by a licensing agreement from ARM Limited.
  */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

	//Public Variables
	public GameObject enemySpawn;
	public GameObject rockSpawn;
	public float spawnTime;
	public float spawnTimeRandom;
	//Private Variables
	private float spawnTimer;

	//Used for initialisation
	void Start () 
	{
		ResetSpawnTimer();
	}

	//Update is called once per frame
	void Update () 
	{
		spawnTimer -= Time.deltaTime;
		if (spawnTimer <= 0.0f)
		{
			Vector3 position = new Vector3 (Random.Range (-25f, 25f), Random.Range (-19f, 19f), 0);
			Instantiate (rockSpawn, position, Quaternion.identity);

			Instantiate(enemySpawn, transform.position, Quaternion.identity);
			ResetSpawnTimer();
		}
	}

	//Resets the spawn timer with a random offset
	void ResetSpawnTimer()
	{
		spawnTimer = (float)(spawnTime + Random.Range(0, spawnTimeRandom*100)/100.0);
	}
}