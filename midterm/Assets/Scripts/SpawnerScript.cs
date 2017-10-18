using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

	//Public Variables
	public GameObject enemySpawn;
	public GameObject rockSpawn;
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
			Instantiate (rockSpawn, position, Quaternion.identity);

			position = new Vector3 (Random.Range (xmin, xmax), Random.Range (ymin, ymax), 0);
			Instantiate(enemySpawn, position, Quaternion.identity);

			ResetSpawnTimer();
		}
	}

	//Resets the spawn timer with a random offset
	void ResetSpawnTimer()
	{
		spawnTimer = (float)(spawnTime + Random.Range(0, spawnTimeRandom*100)/100.0);
	}
}