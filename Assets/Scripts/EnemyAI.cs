using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	
	// Vars
	#region Vars
	public Transform target;
	public int moveSpeed;
	public int rotationSpeed;
	
	public int maxDistance; // distance to player above which enemy will start moving towards the player
	
	private Transform myTransform;
	#endregion
	
	// Awake is called when the script instance is being loaded
	void Awake () {
		myTransform = transform; // to avoid Unity having to look up the transform every time again cache it to a var
	}
	
	// Use this for initialization
	void Start () {
		// Set the player as the target
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
		
		maxDistance = 2;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine(target.position, myTransform.position, Color.yellow); // draw line between enemy and target
		
		// Rotate towards target: Quaternion.Slerp*(from rotation A, to rotation B, turnspeed) *slow movement
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
												Quaternion.LookRotation(target.position - myTransform.position),
												rotationSpeed * Time.deltaTime);
		
		if (Vector3.Distance(target.position, myTransform.position) > maxDistance)
		{
			// Move towards target 
			// if we used Vector3.forward instead of myTransform.forward it would move forward in global space, not in local
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
		}
	}
}
