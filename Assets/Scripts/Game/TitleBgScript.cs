using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class TitleBgScript : FacadeMonoBehaviour {

	Image titleBg;

	void Awake() {
		// get components
		titleBg = transform.GetComponent<Image> ();
		// bind events
		_dispatcher.AddListener ("update_title", setGameBackground);
	}

	void setGameBackground(Object data) {
		string username = PlayerPrefs.GetString ("username");
		GameModel game = ((GameModel)data);
		// get categories
		if (game.players.challenger.username == username) {
			setBackgroundChallenger();
		}
		else {
			setBackgroundChallenged();
		}
	}

	void setBackgroundMenu() {
		titleBg.color = Properties.titleBgMenu;
	}

	void setBackgroundChallenger() {
		titleBg.color = Properties.titleBgChallenger;
	}

	void setBackgroundChallenged() {
		titleBg.color = Properties.titleBgChallenged;
	}
}
