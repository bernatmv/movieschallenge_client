using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class SimulateNativeReloadScript : FacadeMonoBehaviour {

	RectTransform content;
	bool loadingGames = true;

	void Awake() {
		content = (RectTransform)transform;
		_dispatcher.AddListener ("loading_games_start", loadingGamesEnable);
		_dispatcher.AddListener ("loading_games_stop", loadingGamesDisable);
	}

	void FixedUpdate () {
		if (content.offsetMax.y < Properties.reloadScrollOffset && !loadingGames) {
			loadingGames = true;
			Debug.Log ("simulate native reload");
			_dispatcher.Dispatch("auth_finished");
			//_utils.reloadScene();
		}
	}

	void loadingGamesEnable(Object data) {
		loadingGames = true;
	}

	void loadingGamesDisable(Object data) {
		loadingGames = false;
	}
}
