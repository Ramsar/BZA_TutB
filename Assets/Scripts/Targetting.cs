using UnityEngine;
using System.Collections;
using System.Collections.Generic; // for the Lists

public class Targetting : MonoBehaviour {
	
	// Vars
	#region Vars
	public List<Transform> targets;
	
	public Transform selectedTarget;
	
	private Transform myTransform; 
	#endregion
	
	// Use this for initialization
	void Start () {
		targets = new List<Transform>();
		selectedTarget = null;
		myTransform = transform; // cache player transform
		
		AddAllEnemies();
	}
	
	// Add all enemies to target list
	public void AddAllEnemies () {
		GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");
		
		foreach(GameObject enemy in go)
		{
			AddTarget(enemy.transform);
		}
	}
	
	// Add to targets list
	public void AddTarget (Transform enemy) {
		targets.Add(enemy);
	}
	
	// Sort targets in targets list by distance 
	/* How does this code actually works
	 .Sort				The sort algorithm must decide which element to put first. 
	 					It uses the delegate to make this decision	
	 delegate			an anonymous function, returns the return value of .CompareTo
	 t1					current item in list
	 t2					item in list to compare t1 to
	 Vector3.Distance	(float) distance between the two positions
	 .CompareTo			compares this instance to a specified float and returns an integer that indicates whether 
	 					the value of this instance is less than, equal to, or greater than the value of 
	 					the specified float => (less than) before, equals and (greater than) after => -1, 0, 1
	*/ 
	private void SortTargetsByDistance () {
		targets.Sort( delegate(Transform t1, Transform t2) 
						{
							return Vector3.Distance(t1.position, myTransform.position)
									.CompareTo(Vector3.Distance(t2.position, myTransform.position)); 
						} 
					);
	}
	
	// Target enemy
	private void TargetEnemy () {
		if (selectedTarget == null) // if no target is selected, select closest target
		{
			SortTargetsByDistance();	
			selectedTarget = targets[0]; 
		}
		else // if a target is already selected, select next closest target
		{
			int index = targets.IndexOf(selectedTarget); // index of currently selected target in targets list
			if (index < targets.Count - 1) // make sure it isn't the last target
			{
				index++;
			}
			else // if the farthest target, select first target in targets list again
			{
				index = 0; 	
			}
			DeselectTarget();
			selectedTarget = targets[index]; 
		}
		SelectTarget();
	}
	
	// Change color of selected target
	private void SelectTarget () {
		selectedTarget.renderer.material.color = Color.red;
		
		// set target of PlayerAttack to the selected target
		PlayerAttack pa = (PlayerAttack) GetComponent("PlayerAttack");
		pa.target = selectedTarget.gameObject;
	}
	
	// Target not selected
	private void DeselectTarget () {
		selectedTarget.renderer.material.color = Color.blue;
		selectedTarget = null; 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			TargetEnemy();
		}
	}
}
