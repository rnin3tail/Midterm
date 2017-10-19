using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

	//Public Variables
	public GameObject enemySpawn;
	public GameObject enemy2Spawn;
	public GameObject rockSpawn;
	public GameObject lifeSpawn;
	public float spawnTime;
	public float spawnTimeRandom;

// choose x and y axis to spawn things.
	public float xmin;
	public float xmax;
	public float ymin;
	public float ymax;
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
			Vector3 position = new Vector3 (Random.Range (xmin, xmax), Random.Range (ymin, ymax), 0);

		if (spawnTimeRandom != 0) {
// Will generate a random number between 1 and 2. The value gets rounded. This chooses which enemy spawns.
			int enemyType = Random.Range (1, 3);
		
			if (enemyType == 1) {
				Instantiate (enemySpawn, position, Quaternion.identity);
			} 
			if (enemyType == 2) {
				Instantiate (enemy2Spawn, position, Quaternion.identity);
			}
// spawns rocks
				position = new Vector3 (Random.Range (xmin, xmax), Random.Range (ymin, ymax), 0);
				Instantiate (rockSpawn, position, Quaternion.identity);
		}


			// in this case we do not want a random spawn time... This will be the player extra life spawning script.
		if (spawnTimeRandom == 0) {
			Instantiate (lifeSpawn, position, Quaternion.identity);
		}
		
//Reset the timer
			ResetSpawnTimer();
		}


		}

//Resets the spawn timer with a random offset
	void ResetSpawnTimer()
	{
		if (spawnTimeRandom == 0) {
			spawnTimer = spawnTime; // simply set the timer to the value we manually set for the time duration.
			return;
		}
		spawnTimer = (float)(spawnTime + Random.Range(0, spawnTimeRandom*100)/100.0); // set timer with a random number to randomize spawn times.
	}

}