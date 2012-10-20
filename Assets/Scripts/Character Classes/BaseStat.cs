/// <summary>
/// Base stat.
/// This is the base class for all stats in the game
/// </summary>

using UnityEngine;	// to be able to use the Debug.Log

public class BaseStat {
	
	public const int STARTING_EXP_COST = 100;	// publicly accessible value for the cost of all base stats to start at
	
	// underscore before variable is to denote that this variable is private and has a basic setter and getter
	private int _baseValue;						// base value of the stat
	private int _buffValue;						// amount of buff to this stat
	private int _expToLevel;					// total amount of exp needed to raise this skill
	private float _levelModifier;				// modifier applied to the exp needed to raise the skill
	
	/// <summary>
	/// Initializes a new instance of the <see cref="BaseStat"/> class.
	/// i.e. a constructor
	/// </summary>
	public BaseStat () {
		Debug.Log("Base stat created");
		_baseValue = 0;
		_buffValue = 0;
		_levelModifier = 1.1f; 					// 10% needed to raise
		_expToLevel = 100; 						// 100 needed to upgrade to next level, 
												// level after that will be 110 (see _levelModifier)
	}
	
	
	#region Basic setters and getters
	/// <summary>
	/// Gets or sets the _baseValue
	/// </summary>
	/// <value>
	/// The _baseValue.
	/// </value>
	public int BaseValue {
		get { return _baseValue; }
		set { _baseValue = value; }
	}
	
	/// <summary>
	/// Gets or sets the _buffValue
	/// </summary>
	/// <value>
	/// The _buffValue.
	/// </value>
	public int BuffValue {
		get { return _buffValue; }
		set { _buffValue = value; }
	}
	
	/// <summary>
	/// Gets or sets the _expToLevel
	/// </summary>
	/// <value>
	/// The _expToLevel
	/// </value>
	public int ExpToLevel {
		get { return _expToLevel; }
		set { _expToLevel = value; }
	}
	
	/// <summary>
	/// Gets or sets the _levelModifier
	/// </summary>
	/// <value>
	/// The _levelModifier.
	/// </value>
	public float LevelModifier {
		get { return _levelModifier; }
		set { _levelModifier = value; }
	}
	#endregion
	
	
	/// <summary>
	/// Calculates the exp to level.
	/// </summary>
	/// <returns>
	/// The exp to level.
	/// </returns>
	private int CalculateExpToLevel () {
		return (int) (_expToLevel * _levelModifier);
	}
	
	/// <summary>
	/// Assign the new value to the _expToLevel and then increase the _baseValue by 1
	/// </summary>
	public void LevelUp () {
		_expToLevel = CalculateExpToLevel();
		_baseValue++;
	}
	
	/// <summary>
	/// Recalculate the adjusted base value and return it
	/// </summary>
	/// <value>
	/// The adjusted base value.
	/// </value>
	public int AdjustedBaseValue {
		get { return _baseValue + _buffValue; }
	}
	
	/* if we use getter and setter, no braces are needed when calling this method
	 	equivalent (where braces are needed):
	
	public int AdjustedBaseValue () {
		return _baseValue + _buffValue;
	}
	*/
	
	
}
