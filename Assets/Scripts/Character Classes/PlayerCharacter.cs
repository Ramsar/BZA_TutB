public class PlayerCharacter : BaseCharacter {

	// Update is called once per frame
	void Update () {
		Messenger<int, int>.Broadcast("player health update", 80, 100);
	}
	
	

}
