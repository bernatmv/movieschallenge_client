using UnityEngine;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class NewGameButtonScript : FacadeMonoBehaviour {
	
	public void newGame() {
		_dispatcher.Dispatch ("open_new_game_panel");
	}
}