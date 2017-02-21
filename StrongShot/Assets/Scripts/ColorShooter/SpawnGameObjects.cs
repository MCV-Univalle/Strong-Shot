using UnityEngine;
using System.Collections;

public class SpawnGameObjects : MonoBehaviour {

	public GameObject redWordPrefab;
	public GameObject blueWordPrefab;
	public GameObject yellowWordPrefab;

	public float minSecondsBetweenSpawning = 3.0f;
	public float maxSecondsBetweenSpawning = 6.0f;

	private float savedTime;
	private float secondsBetweenSpawning;

	private int id = 0;

	// Use this for initialization
	void Start () {
		savedTime = Time.time;
		secondsBetweenSpawning = Random.Range (minSecondsBetweenSpawning, maxSecondsBetweenSpawning);

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - savedTime >= secondsBetweenSpawning) // is it time to spawn again?
		{
			MakeThingToSpawn();
			savedTime = Time.time; // store for next spawn
			secondsBetweenSpawning = Random.Range (minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
		}	
	}

	void MakeThingToSpawn()
	{
		id = Random.Range (0,3);
		switch (id){
		case 0:
			GameObject cloneRed = Instantiate (redWordPrefab, transform.position, transform.rotation) as GameObject;
			break;
		case 1:
			GameObject cloneBlue = Instantiate (blueWordPrefab, transform.position, transform.rotation) as GameObject;
			break;
		case 2:
			GameObject cloneYellow = Instantiate (yellowWordPrefab, transform.position, transform.rotation) as GameObject;
			break;
		}
	}
}
