/// <summary>
/// Vital bar.
/// This class is responsible for diplaying one of the vitals of a player or a mob
/// </summary>

using UnityEngine;
using System.Collections;

public class VitalBar : MonoBehaviour {
	
	public bool _isPlayerHealthBar;		// this bool tells us if this is the player or the mob vital bar
	
	private int _maxBarLength;				// the max vital bar length
	private int _curBarLength;				// the current vital bar length
	
	private GUITexture _display;
	
	void Awake () {
		_display = gameObject.GetComponent<GUITexture>();
	}
	
	// Use this for initialization
	void Start () {
//		_isPlayerHealthBar = true;
		
		_maxBarLength = (int) _display.pixelInset.width;
		
		OnEnable();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/// <summary>
	/// This method is called when the gameobject is enabled
	/// </summary>
	public void OnEnable () {
		if (_isPlayerHealthBar)
		{
			Messenger<int, int>.AddListener("player health update", OnChangeHealthBarSize);
		}
		else 
		{
			ToggleDisplay(false);
			Messenger<int, int>.AddListener("mob health update", OnChangeHealthBarSize);
			Messenger<bool>.AddListener("show mob vitalbars", ToggleDisplay);
		}
		
	}
	
	/// <summary>
	/// This method is called when the gameobject is disabled
	/// </summary>
	public void OnDisable () {
		if (_isPlayerHealthBar)
		{
			Messenger<int, int>.RemoveListener("player health update", OnChangeHealthBarSize);
		}
		else 
		{
			Messenger<int, int>.RemoveListener("mob health update", OnChangeHealthBarSize);
			Messenger<bool>.AddListener("show mob vitalbars", ToggleDisplay);
		}
	}
	
	/// <summary>
	/// Changes the size of the health bar in relation to the % of health left
	/// </summary>
	public void OnChangeHealthBarSize (int curHealth, int maxHealth) {
//		Debug.Log("We heard an event:" + curHealth + "/" + maxHealth);
		_curBarLength = (int) (((float) curHealth / (float) maxHealth) * _maxBarLength);	// this calculates the current bar length
//		_display.pixelInset = new Rect(_display.pixelInset.x,
//									 _display.pixelInset.y, 
//									 _curBarLength,
//									 _display.pixelInset.height);
		_display.pixelInset = CalculatePosition();
	}
	
	/// <summary>
	/// Setting the health bar to the player or mob
	/// </summary>
	public void SetPlayerHealth (bool b) {
		_isPlayerHealthBar = b;
	}
	
	/// <summary>
	/// Calculates the position of the health bar (depending on whether it concers the player or a mob)
	/// </summary>
	/// <returns>
	/// The position.
	/// </returns>
	private Rect CalculatePosition () {
//		float yPos = _display.pixelInset.height / 2 - 10;
		
		if (!_isPlayerHealthBar)
		{
// Ain't working...
//			float xPos = (_maxBarLength - _curBarLength) - (_maxBarLength / 4 + 10);
//			Debug.Log("Mob: " + xPos + " " + yPos);
//			return new Rect(xPos, yPos, _curBarLength, _display.pixelInset.height);
			
			return new Rect(_display.pixelInset.x,
							_display.pixelInset.y, 
							_curBarLength,
							_display.pixelInset.height);
		}
	
// Ain't working...
//		Debug.Log("Player: " + _display.pixelInset.x + " " + yPos);
//		return new Rect(_display.pixelInset.x, yPos, _curBarLength, _display.pixelInset.height);
		
		return new Rect(_display.pixelInset.x,
						_display.pixelInset.y, 
						_curBarLength,
						_display.pixelInset.height);
	}
	
	/// <summary>
	/// Toggles the display of the vital bar 
	/// </summary>
	/// <param name='show'>
	/// Show.
	/// </param>
	private void ToggleDisplay (bool show) {
		_display.enabled = show;
	}
}
