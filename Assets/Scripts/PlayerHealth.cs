using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	
	// Vars
	#region Vars
	public int maxHealth = 100;	
	public int curHealth = 100;
	
	public float healthBarLength;
	
	#endregion
	
	// Use this for initialization
	void Start () {
		healthBarLength = Screen.width / 2;
	}
	
	// Update is called once per frame
	void Update () {
		AdjustCurrentHealth(0);
	}
	
	// OnGUI
	void OnGUI () {
		GUI.Box(new Rect(10, 10, healthBarLength, 20), curHealth + "/" + maxHealth);
	}
	
	// Adjust current health (adj: heal = + or damage = -)
	public void AdjustCurrentHealth (int adj) {
		curHealth += adj;
		
		// avoid player to go below 0 health
		if (curHealth < 0)
			curHealth = 0;
		// avoid player to go above max health
		if (curHealth > maxHealth)
			curHealth = maxHealth;
		// avoid division by zero
		if (maxHealth < 1)
			maxHealth = 1;
		
		healthBarLength = (Screen.width / 2) * (curHealth / (float) maxHealth);
	}
}
