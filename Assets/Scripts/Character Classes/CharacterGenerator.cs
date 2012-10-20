using UnityEngine;
using System.Collections;
using System;

public class CharacterGenerator : MonoBehaviour {
	
	// Vars
	#region Vars
	private PlayerCharacter _toon; 							// ref to character
	private const int STARTING_POINTS = 350; 				// const => can't be changed
	private const int MIN_STARTING_ATTRIBUTE_VALUE = 10;	// min attribute value
	private const int STARTING_VALUE = 50;					// starting attribute value
	private int pointsLeft;
	
	// GUI vars
	private const int OFFSET = 5;					// X pixel border
	private const int LINE_HEIGHT = 20;				// line height
	private const int STAT_LABEL_WIDTH = 100;		// stat label width
	private const int BASEVALUE_LABEL_WIDTH = 30;	// stat value label width
	private const int BUTTON_WIDTH = 20;
	private const int BUTTON_HEIGHT = 20;
	private int statStartingPos = 40;				//
	
	//public GUIStyle myStyle; 	// can be used if you want to style items separately
	
	// can be used to style all elements of a type (e.g. buttons or labels) at the same time
	// but can be temporarily put off an on by using GUI.skin = null; (see OnGUI function)
	public GUISkin mySkin;	
	
	//
	public GameObject playerPrefab;
	#endregion
	
	// Use this for initialization
	void Start () {
		GameObject pc = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		pc.name = "pc"; // set name of player prefab instance
		
		//_toon = new PlayerCharacter();
		//_toon.Awake();
		_toon = pc.GetComponent<PlayerCharacter>();
		
		pointsLeft = STARTING_POINTS;
		// set all attributes to min value
		for (int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
		{
			_toon.GetPrimaryAttribute(cnt).BaseValue = STARTING_VALUE;
			pointsLeft -= (STARTING_VALUE - MIN_STARTING_ATTRIBUTE_VALUE);
		}
			
		
		_toon.StatUpdate();
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	// OnGUI
	void OnGUI () {
		GUI.skin = mySkin;

		DisplayName();
		DisplayPointsLeft();
		DisplayAttributes();
		//GUI.skin = null;
		DisplayVitals();
		//GUI.skin = mySkin;
		DisplaySkills();
		
		if (_toon.Name == "" || pointsLeft > 0)	
			DisplayCreateLabel();	// show label instead of button if there are still points left or no name is given
		else
			DisplayCreateButton();
	}
	
	// Display functions called in OnGUI
	#region Display functions
	private void DisplayName () {
		GUI.Label(new Rect(10, 10, 50, 25), "Name:");
		_toon.Name = GUI.TextField(new Rect(65, 10, 100, 25), _toon.Name); 
		// .TextArea takes multiple lines of text, .TextField only one line
	}
	
	private void DisplayAttributes () {
		for (int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
		{
			// attribute name
			GUI.Label(new Rect(	OFFSET, 									// x
								statStartingPos + (cnt * LINE_HEIGHT),		// y
								STAT_LABEL_WIDTH,							// width
								LINE_HEIGHT),								// height
								((AttributeName)cnt).ToString());	// value	
			// attribute value
			GUI.Label(new Rect( STAT_LABEL_WIDTH + OFFSET, 
								statStartingPos + (cnt * LINE_HEIGHT), 
								BASEVALUE_LABEL_WIDTH, 
								LINE_HEIGHT), 
								_toon.GetPrimaryAttribute(cnt).AdjustedBaseValue.ToString());
			
			// + and - buttons	
			if (GUI.Button(new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH, 
									statStartingPos+ (cnt * BUTTON_HEIGHT),
									BUTTON_WIDTH, 
									BUTTON_HEIGHT),
									"-"))
			{
				if (_toon.GetPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE)
				{
					_toon.GetPrimaryAttribute(cnt).BaseValue--;
					pointsLeft++;
					_toon.StatUpdate();
				}
			}
			if (GUI.Button(new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH, 
									statStartingPos+ (cnt * BUTTON_HEIGHT),
									BUTTON_WIDTH, 
									BUTTON_HEIGHT),
									"+"))
			{
				if (pointsLeft > 0)
				{
					_toon.GetPrimaryAttribute(cnt).BaseValue++;
					pointsLeft--;
					_toon.StatUpdate();
				}
			}
		}
	}
	
	private void DisplayVitals () {
		for (int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
		{
			GUI.Label(new Rect( OFFSET, 
								statStartingPos + ((cnt + 7) * LINE_HEIGHT),
								STAT_LABEL_WIDTH, 
								LINE_HEIGHT),
								((VitalName)cnt).ToString());
			GUI.Label(new Rect(	STAT_LABEL_WIDTH + OFFSET,
								40 + ((cnt + 7) * LINE_HEIGHT),
								BASEVALUE_LABEL_WIDTH,
								LINE_HEIGHT), 
								_toon.GetVital(cnt).AdjustedBaseValue.ToString());
		}
	}
	
	private void DisplaySkills () {
		for (int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++)
		{
			GUI.Label(new Rect( OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH * 2 + OFFSET * 2,
								statStartingPos + (cnt * LINE_HEIGHT),
								STAT_LABEL_WIDTH,
								LINE_HEIGHT),
								((SkillName)cnt).ToString());
			GUI.Label(new Rect( OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH * 2 + OFFSET * 2 + STAT_LABEL_WIDTH,
								statStartingPos + (cnt * LINE_HEIGHT), 
								BASEVALUE_LABEL_WIDTH,
								LINE_HEIGHT),
								_toon.GetSkill(cnt).AdjustedBaseValue.ToString());
		}
	}
	
	private void DisplayPointsLeft () {
		GUI.Label(new Rect(250, 10, 100, 25), "Points left: " + pointsLeft.ToString());
	}
	
	private void DisplayCreateButton () {
		if (GUI.Button(new Rect(Screen.width / 2 - 490,
							statStartingPos + 10 * LINE_HEIGHT,
							100, 
							LINE_HEIGHT),
							"Create"))
		{
//			GameObject gs = GameObject.Find("__GameSettings");
//			GameSettings gsScript = gs.GetComponent<GameSettings>();
			GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();
			
			UpdateCurVitalValues(); // change 'cur value' of vitals to max modified value of that vital
			
			gsScript.SaveCharacterData();
			
			Application.LoadLevel("Level1"); // can also use Application.LoadLevel(1)
		}
	}
	
	private void DisplayCreateLabel () {
		GUI.Label(new Rect(Screen.width / 2 - 490,
							statStartingPos + 10 * LINE_HEIGHT,
							100, 
							LINE_HEIGHT),
							"(create)", "Button");
	}
	
	#endregion
	
	// Change 'cur value' of vitals to max modified value of that vital
	private void UpdateCurVitalValues () {
		for (int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
		{
			_toon.GetVital(cnt).CurValue = _toon.GetVital(cnt).AdjustedBaseValue;
		}
	}
}
