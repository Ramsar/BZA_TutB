using UnityEngine;
using System.Collections;
using System;

public class GameSettings : MonoBehaviour {
	
	// this is the name of the gameobject that the player will spawn at at the start of the level
	public const string PLAYER_SPAWN_POINT = "Player Spawn Point";		
	
	// Awake
	void Awake () {
		// tell game that this object shouldn't be destroyed on load, but passed on
		DontDestroyOnLoad(this);	
	}
	
	
	// Save character data
	public void SaveCharacterData () {
		GameObject pc = GameObject.Find("pc");							// ref to character
		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();	// ref to player character class attached to it
		
		PlayerPrefs.DeleteAll(); // Delete old PlayerPrefs
		
		PlayerPrefs.SetString("Player Name", pcClass.Name);	// save player name		
		
		// Save attribute parameters
		for (int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) 
		{
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + " - Base Value", 
				pcClass.GetPrimaryAttribute(cnt).BaseValue);
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + " - Exp To Level", 
				pcClass.GetPrimaryAttribute(cnt).ExpToLevel);
		}
		
		// Save vital parameters
		for (int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++) 
		{
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " - Base Value", 
				pcClass.GetVital(cnt).BaseValue);
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " - Exp To Level", 
				pcClass.GetVital(cnt).ExpToLevel);
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " - Cur Value", 
				pcClass.GetVital(cnt).CurValue);
			
//			PlayerPrefs.SetString(((VitalName)cnt).ToString() + " - Mods", 
//				pcClass.GetVital(cnt).GetModifyingAttributesString());
		}
		
		// Save skill parameters
		for (int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++) 
		{
			PlayerPrefs.SetInt(((SkillName)cnt).ToString() + " - Base Value", 
				pcClass.GetSkill(cnt).BaseValue);
			PlayerPrefs.SetInt(((SkillName)cnt).ToString() + " - Exp To Level", 
				pcClass.GetSkill(cnt).ExpToLevel);
			
//			PlayerPrefs.SetString(((SkillName)cnt).ToString() + " - Mods", 
//				pcClass.GetSkill(cnt).GetModifyingAttributesString());
		}
	}
	
	// Load character data
	public void LoadCharacterData () {
		GameObject pc = GameObject.Find("pc");							// ref to character
		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();	// ref to player character class attached to it
		
		pcClass.Name = PlayerPrefs.GetString("Player Name", "Name me");	// load player name	(second is default value)
		
		// Load attribute parameters
		for (int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) 
		{
			pcClass.GetPrimaryAttribute(cnt).BaseValue = 
				PlayerPrefs.GetInt(((AttributeName)cnt).ToString() + " - Base Value", 0);
			pcClass.GetPrimaryAttribute(cnt).ExpToLevel = 
				PlayerPrefs.GetInt(((AttributeName)cnt).ToString() + " - Exp To Level", Attribute.STARTING_EXP_COST);
		}
		
		// Load vital parameters
		for (int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++) 
		{
			pcClass.GetVital(cnt).BaseValue = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Base Value", 0);
			pcClass.GetVital(cnt).ExpToLevel = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Exp To Level", 0); 
			
			// make sure you call this so that the AdjustedBaseValue will be updated
			// before you try to call to get the curValue
			pcClass.GetVital(cnt).Update();
			
			// get the stored value for the curValue for each vital
			pcClass.GetVital(cnt).CurValue = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Cur Value", 1);
		}
		
		// Load skill parameters
		for (int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++) 
		{
			pcClass.GetSkill(cnt).BaseValue = PlayerPrefs.GetInt(((SkillName)cnt).ToString() + " - Base Value", 0);
			pcClass.GetSkill(cnt).ExpToLevel = PlayerPrefs.GetInt(((SkillName)cnt).ToString() + " - Exp To Level", 0);
		}
	}
}
