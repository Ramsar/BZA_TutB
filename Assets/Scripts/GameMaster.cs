using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	
	//Vars
	public GameObject playerCharacter;
	public Camera mainCamera;
	public GameObject gameSettings;
	
	public float zOffset;
	public float yOffset;
	public float xRotOffset;
	
	private GameObject _pc;
	private PlayerCharacter _pcScript;
	
	private Vector3 _playerSpawnPointPos;			// this is the place in 3D space where the player spawns
	
	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find(GameSettings.PLAYER_SPAWN_POINT);	// player spawn point gameobject
		
		_playerSpawnPointPos = new Vector3(1285, 0.2f, 1666);					// default position for player spawn point
			
		if (go == null) // check if go isn't null
		{
			Debug.LogWarning("Cannot find player spawn point");
			go = new GameObject(GameSettings.PLAYER_SPAWN_POINT);
			go.transform.position = _playerSpawnPointPos;
			Debug.Log("Created player spawn point");
		}
		
		_pc = Instantiate(playerCharacter, go.transform.position, Quaternion.identity) as GameObject;
		_pc.name = "pc";
		
		_pcScript = _pc.GetComponent<PlayerCharacter>();
		
		// Get camera to be behind player char
		zOffset = -2.5f;
		yOffset = 2.5f;
		xRotOffset = 22.5f;
		mainCamera.transform.position = new Vector3(_pc.transform.position.x,
													_pc.transform.position.y + yOffset,
													_pc.transform.position.z + zOffset);
		mainCamera.transform.Rotate(xRotOffset, 0, 0);
		
		LoadCharacter();
	}
	
	//
	public void LoadCharacter () {
		GameObject gs = GameObject.Find("__GameSettings");
		if (gs == null)
		{
			GameObject gs1 = Instantiate(gameSettings, Vector3.zero, Quaternion.identity) as GameObject;
			gs1.name = "__GameSettings";
		}
			
		GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();
			
		gsScript.LoadCharacterData();	// Loading the character data	
	}
}
