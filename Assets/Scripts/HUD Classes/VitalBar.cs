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
	
	// Use this for initialization
	void Start () {
//		_isPlayerHealthBar = true;
		
		_display = gameObject.GetComponent<GUITexture>();
		
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
			Messenger<int, int>.AddListener("mob health update", OnChangeHealthBarSize);
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
		}
	}
	
	/// <summary>
	/// Changes the size of the health bar in relation to the % of health left
	/// </summary>
	public void OnChangeHealthBarSize (int curHealth, int maxHealth) {
//		Debug.Log("We heard an event:" + curHealth + "/" + maxHealth);
		_curBarLength = (int) (((float) curHealth / (float) maxHealth) * _maxBarLength);	// this calculates the current bar length
		_display.pixelInset = new Rect(_display.pixelInset.x,
									 _display.pixelInset.y, 
									 _curBarLength,
									 _display.pixelInset.height);
	}
	
	/// <summary>
	/// Setting the health bar to the player or mob
	/// </summary>
	public void SetPlayerHealth (bool b) {
		_isPlayerHealthBar = b;
	}
}
