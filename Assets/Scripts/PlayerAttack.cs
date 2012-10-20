using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	
	// Vars
	#region Vars
	public GameObject target;
	
	public float attackTimer;
	public float coolDown;
	#endregion
	
	// Use this for initialization
	void Start () {
		attackTimer = 0;
		coolDown = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (attackTimer > 0)
			attackTimer -= Time.deltaTime; // reduce attackTimer by time
		if (attackTimer < 0)
			attackTimer = 0; // avoid attackTimer to go below 0
		
		// attack button
		if (Input.GetKeyUp(KeyCode.F))
		{
			if (attackTimer == 0)
			{
				Attack();
				attackTimer = coolDown; // set coolDown for attack
			}
		}
	}
	
	// Attack
	private void Attack () {
		// distance between the target and the player
		float distance = Vector3.Distance(target.transform.position, transform.position);
		
		// check where enemy is wrt the player
		// dot product returns value between 1 and -1. 1 means enemy is in front of us, -1 he's to the back
		Vector3 dir = (target.transform.position - transform.position).normalized;
		float direction = Vector3.Dot(dir, transform.forward); 
		
		Debug.Log(direction);
		
		
		if (distance < 2.5f) // check if close enough for attack to hit
		{
			if (direction > 0) // check whether enemy is in front of us
			{
				// ref to EnemyHealth script of target and do damage
				EnemyHealth eh = (EnemyHealth) target.GetComponent("EnemyHealth");
				eh.AdjustCurrentHealth(-10);
			}
		}

	}
}
