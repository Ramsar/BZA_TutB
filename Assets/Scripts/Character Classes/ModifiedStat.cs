/// <summary>
/// Modified stat.
/// This is the base class for all stats that will be modifiable by attributes
/// </summary>

using System.Collections.Generic; 			// added so List can be used

public class ModifiedStat : BaseStat {
	
	private List<ModifyingAttribute> _mods;	// a list of attributes that modify this stat
	private int _modValue;					// the amount added to the baseValue from the modifiers
	
	
	/// <summary>
	/// Initializes a new instance of the <see cref="ModifiedStat"/> class.
	/// </summary>
	public ModifiedStat () {
		UnityEngine.Debug.Log("Modified stat created");
		_mods = new List<ModifyingAttribute>();
		_modValue = 0;
	}
	
	/// <summary>
	/// Adds a ModifyingAttribute to our list of mods for this ModifiedStat
	/// </summary>
	/// <param name='mod'>
	/// Mod.
	/// </param>
	public void AddModifier (ModifyingAttribute mod) {
		_mods.Add(mod);
	}
	
	/// <summary>
	/// Reset _modValue to zero
	/// Check to see if we have at least one ModifyingAttribute in our list of mods
	/// If we do, then iterate through the list and add the AdjustedBaseValue * ratio to our modValue
	/// </summary>
	private void CalculateModValue () {
		_modValue = 0; 						// just to make sure that it's zero
		
		if (_mods.Count > 0) 				// have a least one value in list
		{
			foreach (ModifyingAttribute att in _mods)
			{
				_modValue += (int) (att.attribute.AdjustedBaseValue * att.ratio);
			}
		}
	}
	
	
	/// <summary>
	/// Calculate the AdjustedBaseValue from the base, buff and mod value
	/// Polymorphism: this function overwrites the AdjustedBaseValue function of BaseStat class
	/// </summary>
	/// <value>
	/// The adjusted base value.
	/// </value>
	public new int AdjustedBaseValue {
		get { return BaseValue + BuffValue + _modValue; }
		// this uses the getters for _baseValue and _buffValue
	}
	
	/// <summary>
	/// Update this instance.
	/// Not the update from Monobehaviour
	/// </summary>
	public void Update () {
		CalculateModValue();
	}
	

	public string GetModifyingAttributesString () {
		string temp = "";
		
//		UnityEngine.Debug.Log(_mods.Count);
		
		for (int cnt= 0; cnt < _mods.Count; cnt++)
		{
			temp += _mods[cnt].attribute.Name;
			temp += "_";
			temp += _mods[cnt].ratio;
			
			if (cnt < _mods.Count - 1) 		// if not the last modifying attribute
				temp += "|";
//			UnityEngine.Debug.Log(_mods[cnt].attribute.Name);
//			UnityEngine.Debug.Log(_mods[cnt].ratio);
			
		}
//		UnityEngine.Debug.Log(temp);
		return temp;
	}
}

/// <summary>
/// A structure that will hold an Attribute and a ratio that will be added as
/// a modifying attribute to our Modified Stats
/// Structure: like a class, but with no methods, to hold a bunch of vars together
/// </summary>
public struct ModifyingAttribute {
	public Attribute attribute; 			// instance of Attribute class, to be used as modifier
	public float ratio;						// attribute's effect on ModifiedStat (percentage of the attribute's AdjustedBaseValue)
	
	/// <summary>
	/// Initializes a new instance of the <see cref="ModifyingAttribute"/> struct.
	/// </summary>
	/// <param name='att'>
	/// Att. the attribute to be used
	/// </param>
	/// <param name='rat'>
	/// Rat. the ratio to be used
	/// </param>
	public ModifyingAttribute (Attribute att, float rat) {
		UnityEngine.Debug.Log("Modifying Attribute created");
		attribute = att;
		ratio = rat;
	}
}
