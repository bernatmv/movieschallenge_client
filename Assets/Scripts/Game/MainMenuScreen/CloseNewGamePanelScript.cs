using UnityEngine;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class CloseNewGamePanelScript : FacadeMonoBehaviour {

	public void closePanel() {
		_dispatcher.Dispatch ("close_new_game_panel");
	}
}
