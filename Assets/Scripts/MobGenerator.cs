using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobGenerator : MonoBehaviour {
	
	public enum State {
		Idle,
		Initialize,
		Setup,
		SpawnMob
	}
	
	public GameObject[] mobPrefabs;				// an array to hold all of the prefabs of mobs we want to spawn
	public GameObject[] spawnPoints;			// an array to hold a reference to all of the spawn points
	
	public State state;							// local variable that holds our current state
	
	// Called before the script runs
	// Use this to make sure you have references and variables set to what is needed before the script runs
	void Awake () {
		state = MobGenerator.State.Initialize;		
	}
	
	IEnumerator Start () {
		while (true)
		{
			switch (state) 
			{
			case State.Initialize:
				Initialize();
				break;
			case State.Setup:
				Setup();
				break;
			case State.SpawnMob:
				SpawnMob();
				break;
			}
			
			yield return 0;						// to limit the while statement to run only once per frame
		}
	}
	
	// Make sure everyting is initialized before we go on to the next step
	private void Initialize () {
		Debug.Log("*** We are in the Initialize function ***");
		
		if (!CheckForMobPrefabs())
			return; //i.e. break out of the function
		if (!CheckForSpawnPoints())
			return;
		
		state = MobGenerator.State.Setup;
	}
	
	// Make sure that everything is set up before we continue
	private void Setup () {
		Debug.Log("*** We are in the Setup function ***");
		
		state = MobGenerator.State.SpawnMob;
	}
	
	// Spawn a mob if we have an open spawn point
	private void SpawnMob () {
		Debug.Log("*** We are in the SpawnMob function ***");
		
		GameObject[] gos = AvailableSpawnPoints();
		for (int cnt = 0; cnt < gos.Length; cnt++)
		{
			GameObject go = Instantiate(mobPrefabs[Random.Range(0, mobPrefabs.Length)],
										gos[cnt].transform.position,
										Quaternion.identity
										) as GameObject;
			go.transform.parent = gos[cnt].transform;
				
		}
		
		
		state = MobGenerator.State.Idle;
	}
	
	// Check to see that we have at least one prefab to spawn
	private bool CheckForMobPrefabs () {
		if (mobPrefabs.Length > 0)
			return true;
		else
			return false;
	}
	
	// Check to see that we have at least one spawn point
	private bool CheckForSpawnPoints () {
		if (spawnPoints.Length > 0)
			return true;
		else
			return false;
	}
	
	// Generate a list of available spawn points (those that do not have any mobs childed to them)
	private GameObject[] AvailableSpawnPoints () {
		List<GameObject> gos = new List<GameObject>();
		
		for (int cnt = 0; cnt < spawnPoints.Length; cnt++)
		{
			if (spawnPoints[cnt].transform.childCount == 0)
			{
				Debug.Log("*** Spawn point available ***");
				gos.Add(spawnPoints[cnt]);
			}
		}
		
		return gos.ToArray();
	}
}
