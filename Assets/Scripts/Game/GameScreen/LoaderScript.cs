using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog.movieschallenge;
using BestHTTP;
using LitJson;

public class LoaderScript : FacadeMonoBehaviour {

	string _gameId;

	void Awake() {
		// get gameId
		_gameId = PlayerPrefs.GetString ("gameId");
		// bind events
		_dispatcher.AddListener ("update_game", updateGame);
	}

	void Start () {
		if (!string.IsNullOrEmpty(_gameId)) {
			// load game info
			getGame();
		} 
		else {
			// TODO: show error and redirect to main menu
			Application.LoadLevel("MainMenu");
		}
	}

	// get game info from API
	public void getGame() {
		// get all the games
		HTTPRequest request = new HTTPRequest(new System.Uri(Properties.API + "/game/" + _gameId), (HTTPRequest req, HTTPResponse res) => {
			// deserialize json
			GameModel game = JsonMapper.ToObject<GameModel> (res.DataAsText);
			// build scene
			buildScene(game);
		});
		request.AddField ("token", PlayerPrefs.GetString ("token"));
		request.Send ();
	}

	void buildScene(GameModel game) {
		// set title
		setTitle (game);
		// set the categories and name of the players
		_dispatcher.Dispatch ("set_player_name", game);
		_dispatcher.Dispatch ("update_categories", game);
		_dispatcher.Dispatch ("enable_play_button", game);
	}

	void setTitle(GameModel game) {
		TitleTextScript titleText;
		// set title
		titleText = FindObjectOfType<TitleTextScript>();
		titleText.setTitle ("Turn " + game.turn);
	}

	void updateGame(Object data) {
		// re-build scene
		buildScene (((GameModel)data));
		// reset question-answers
		_dispatcher.Dispatch ("reset_answers");
	}
}
