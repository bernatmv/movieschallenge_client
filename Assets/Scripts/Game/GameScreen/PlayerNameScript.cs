using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog.movieschallenge;

public class PlayerNameScript : FacadeMonoBehaviour {

	public bool challenger = true;

	void Awake () {
		_dispatcher.AddListener ("set_player_name", updateName);
	}
	
	void updateName(Object game) {
		Text playerNameText;
		string playerName;
		if (challenger) {
			playerName = ((GameModel) game).players.challenger.username;
		}
		else {
			playerName = ((GameModel) game).players.challenged.username;
		}
		playerNameText = transform.GetComponent<Text> ();
		playerNameText.text = playerName;
	}
}
