/// <summary>
/// Vital.
/// This class contains all the extra functions for a character's vitals
/// </summary>

public class Vital : ModifiedStat {
	
	private int _curValue;			// current value of this vital
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Vital"/> class.
	/// </summary>
	public Vital() {
		UnityEngine.Debug.Log("Vital created");
		_curValue = 0;
		ExpToLevel = 40;
		LevelModifier = 1.1f;
	}
	
	/// <summary>
	/// Gets or sets the current value.
	/// When getting the _curValue make sure that it is not greater than the AdjustedBaseValue
	/// </summary>
	/// <value>
	/// The current value.
	/// </value>
	public int CurValue {
		set { _curValue = value; }
		get
		{
			// check whether current is not greater than max
			if (_curValue > AdjustedBaseValue) 
				_curValue = AdjustedBaseValue;
			return _curValue;
		}
	}
}

/// <summary>
/// Vital name.
/// A list of vitals our character will have
/// </summary>
public enum VitalName {
	Health,
	Energy,
	Mana
}
