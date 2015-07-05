using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog.movieschallenge;
using BestHTTP;
using LitJson;

public class LoaderScript : FacadeMonoBehaviour {

	string _gameId;

	void Awake() {
		_gameId = PlayerPrefs.GetString ("gameId");
	}

	void Start () {
		if (!string.IsNullOrEmpty(_gameId)) {
			// load game info
			getGame();
		} 
		else {
			// TODO: show error and redirect to main menu
		}
	}
	
	// get game info from API
	void getGame() {
		// get all the games
		HTTPRequest request = new HTTPRequest(new System.Uri(Properties.API + "/game/" + _gameId), (HTTPRequest req, HTTPResponse res) => {
			// deserialize json
			GameModel games = JsonMapper.ToObject<GameModel> (res.DataAsText);
			// build scene
			buildScene(games);
		});
		request.AddField ("token", PlayerPrefs.GetString ("token"));
		request.Send ();
	}

	void buildScene(GameModel game) {
		// set title
		setTitle (game);
		// set challenger categories progress
		_dispatcher.Dispatch ("update_player_categories", game);
	}

	void setTitle(GameModel game) {
		TitleTextScript titleText;
		// set title
		titleText = FindObjectOfType<TitleTextScript>();
		titleText.setTitle ("Turn " + game.turn);
	}
}
