public class Skill : ModifiedStat {
	// inherits from ModifiedStat
	
	private bool _known;	// tells game whether or not character knows the skill
	
	// Constructor
	public Skill() {
		_known = false;
		ExpToLevel = 25;
		LevelModifier = 1.1f;
	}
	
	// Setter and getter
	public bool Known {
		get { return _known; }
		set { _known = value; }
	}
}

// Skill enumeration
public enum SkillName {
	Melee_Offence,
	Melee_Defence,
	Ranged_Offence,
	Ranged_Defence,
	Magic_Offence,
	Magic_Defence
}
