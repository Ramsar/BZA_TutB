using UnityEngine;
using System.Collections;
using System; // to access enum class

// if class doesn't derive from MonoBehaviour script cannot be attached to GameObject
public class BaseCharacter : MonoBehaviour {
	
	// Vars
	private string _name;
	private int _level;
	private uint _freeExp; // XP free to spend, unsigned integer (character can't have neg. XP)
	
	private Attribute[] _primaryAttribute;
	private Vital[] _vital;
	private Skill[] _skill;
	
	// Awake
	public void Awake () {
		_name = string.Empty;
		_level = 0;
		_freeExp = 0;
		
		// set sizes of arrays by finding out size of enumeration (that's why we need "using System;"
		_primaryAttribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
		_vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
		_skill = new Skill[Enum.GetValues(typeof(SkillName)).Length];
		
		SetupPrimaryAttributes();
		SetupVitals();
		SetupSkills();
	}

	#region Setters and getters
	public string Name {
		get { return _name; }
		set { _name = value; }
	}
	
	public int Level {
		get { return _level; }
		set { _level = value; }
	}
	
	public uint FreeExp {
		get { return _freeExp; }
		set { _freeExp = value; }
	}
	#endregion
	
	// To add XP
	public void AddExp (uint exp) {
		_freeExp += exp;
		CalculateLevel();
	}
	
	// Calculate level of character: take avg of all the player's skills and assign that as the playerlevel
	public void CalculateLevel () {
		
	}
	
	// Private functions to set up default primary attributes, vitals and skills
	private void SetupPrimaryAttributes () {
		for (int cnt = 0; cnt < _primaryAttribute.Length; cnt++)
		{
			_primaryAttribute[cnt] = new Attribute();
			_primaryAttribute[cnt].Name = ((AttributeName)cnt).ToString();
		}
	}
	
	private void SetupVitals () {
		for (int cnt = 0; cnt < _vital.Length; cnt++)
		{
			_vital[cnt] = new Vital();
		}
		SetupVitalModifiers();
	}
	
	private void SetupSkills () {
		for (int cnt = 0; cnt < _skill.Length; cnt++)
		{
			_skill[cnt] = new Skill();
		}
		SetupSkillModifiers();
	}
	
	// Getters functions
	public Attribute GetPrimaryAttribute (int index) {
		return _primaryAttribute[index];
	}
	
	public Vital GetVital (int index) {
		return _vital[index];
	}
	
	public Skill GetSkill (int index) {
		return _skill[index];
	}
	
	//
	private void SetupVitalModifiers () {
		// health (half the value of constitution is assigned to health)
		GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute
			(GetPrimaryAttribute((int)AttributeName.Constitution), .5f));
		
		// energy (full value of constitution is assigned to health)
		GetVital((int)VitalName.Energy).AddModifier(new ModifyingAttribute
			(GetPrimaryAttribute((int)AttributeName.Constitution), 1));
		
		// mana (full value of willpower is assigned to mana)
		GetVital((int)VitalName.Mana).AddModifier(new ModifyingAttribute
			(GetPrimaryAttribute((int)AttributeName.Willpower), 1));
	}
	
	private void SetupSkillModifiers () {
		// melee offence
		GetSkill((int)SkillName.Melee_Offence).AddModifier(new ModifyingAttribute
			(GetPrimaryAttribute((int)AttributeName.Might), .33f));
		GetSkill((int)SkillName.Melee_Offence).AddModifier(new ModifyingAttribute
			(GetPrimaryAttribute((int)AttributeName.Nimbleness), .33f));
		
		// melee defence
		GetSkill((int)SkillName.Melee_Defence).AddModifier(new ModifyingAttribute
			(GetPrimaryAttribute((int)AttributeName.Speed), .33f));
		GetSkill((int)SkillName.Melee_Defence).AddModifier(new ModifyingAttribute
			(GetPrimaryAttribute((int)AttributeName.Constitution), .33f));
		
		// magic offence
		GetSkill((int)SkillName.Magic_Offence).AddModifier(new ModifyingAttribute
			(GetPrimaryAttribute((int)AttributeName.Concentration), .33f));
		GetSkill((int)SkillName.Magic_Offence).AddModifier(new ModifyingAttribute
			(GetPrimaryAttribute((int)AttributeName.Willpower), .33f));
		
		// magic defence
		GetSkill((int)SkillName.Magic_Defence).AddModifier(new ModifyingAttribute
			(GetPrimaryAttribute((int)AttributeName.Concentration), .33f));
		GetSkill((int)SkillName.Magic_Defence).AddModifier(new ModifyingAttribute
			(GetPrimaryAttribute((int)AttributeName.Willpower), .33f));
		
		// ranged offence
		GetSkill((int)SkillName.Ranged_Offence).AddModifier(new ModifyingAttribute
			(GetPrimaryAttribute((int)AttributeName.Concentration), .33f));
		GetSkill((int)SkillName.Ranged_Offence).AddModifier(new ModifyingAttribute
			(GetPrimaryAttribute((int)AttributeName.Speed), .33f));
		
		// ranged defence
		GetSkill((int)SkillName.Ranged_Defence).AddModifier(new ModifyingAttribute
			(GetPrimaryAttribute((int)AttributeName.Speed), .33f));
		GetSkill((int)SkillName.Ranged_Defence).AddModifier(new ModifyingAttribute
			(GetPrimaryAttribute((int)AttributeName.Nimbleness), .33f));
	}
	
	// an update function for stats
	public void StatUpdate () {
		for (int cnt = 0; cnt < _vital.Length; cnt++)
			_vital[cnt].Update();
		
		for (int cnt = 0; cnt < _skill.Length; cnt++) 
			_skill[cnt].Update();
	}
}
