/// <summary>
/// Skill.
/// This class contains the extra functions that are needed for a skill
/// </summary>

public class Skill : ModifiedStat {
	
	private bool _known;		// tells game whether or not the character knows the skill
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Skill"/> class.
	/// </summary>
	public Skill() {
		UnityEngine.Debug.Log("Skill created");
		_known = false;
		ExpToLevel = 25;
		LevelModifier = 1.1f;
	}
	
	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="Skill"/> is known.
	/// </summary>
	/// <value>
	/// <c>true</c> if known; otherwise, <c>false</c>.
	/// </value>
	public bool Known {
		get { return _known; }
		set { _known = value; }
	}
}

/// <summary>
/// Skill name.
/// List of skills that a player can learn
/// </summary>
public enum SkillName {
	Melee_Offence,
	Melee_Defence,
	Ranged_Offence,
	Ranged_Defence,
	Magic_Offence,
	Magic_Defence
}
