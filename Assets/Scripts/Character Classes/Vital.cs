public class Vital : ModifiedStat {
	// inherits from ModifiedStat
	
	private int _curValue;
	
	
	// Constructor
	public Vital() {
		_curValue = 0;
		ExpToLevel = 40;
		LevelModifier = 1.1f;
	}
	
	// Setter and getter
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

public enum VitalName {
	Health,
	Energy,
	Mana
}
