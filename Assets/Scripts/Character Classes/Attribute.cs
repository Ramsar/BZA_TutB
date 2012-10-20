/// <summary>
/// Attribute
/// This is the class for all of the character attributes in game
/// </summary>

public class Attribute : BaseStat {
	
	new public const int STARTING_EXP_COST = 50;	// this is the starting cost for all attributes
	
	private string _name;							// this is the name of the attribute
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Attribute"/> class.
	/// </summary>
	public Attribute () {
		UnityEngine.Debug.Log("Attribute created");
		_name = "";
		ExpToLevel = STARTING_EXP_COST;
		LevelModifier = 1.05f;
	}
	
	/// <summary>
	/// Gets or sets _name
	/// </summary>
	/// <value>
	/// _name
	/// </value>
	public string Name 
	{
		get { return _name; }
		set { _name = value; }
	}
}

/// <summary>
/// A list of all the attributes that will be in game for characters
/// </summary>
public enum AttributeName {
	Might,
	Constitution,
	Nimbleness,
	Speed,
	Concentration,
	Willpower,
	Charisma
	
	// putting no numbers: it is then assumed that it is just [... ] = 0, [...] = 1, ...
	
	// if you want a two word attribute use underscore (which is dropped later if you code it appropriately)
}
