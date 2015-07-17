using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using com.lovelydog;
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
			_utils.loadScene("MainMenu");
		}
	}

	// get game info from API
	public void getGame() {
		API request = new API ("/game/" + _gameId, (HTTPRequest req, HTTPResponse res) => {
			// deserialize json
			GameModel game = JsonMapper.ToObject<GameModel> (res.DataAsText);
			// build scene
			buildScene (game);
		});
		request.AddField ("token", PlayerPrefs.GetString ("token"));
		request.Send ();
	}

	void buildScene(GameModel game) {
		// set title
		setTitle (game);
		// set the categories and name of the players
		_dispatcher.Dispatch ("disable_play_button", game);
		_dispatcher.Dispatch ("set_player_name", game);
		_dispatcher.Dispatch ("update_categories", game);
		// check if a star question is in progress
		int indexOfStarQuestion = starQuestionPending (game);
		if (indexOfStarQuestion >= 0) {
			_dispatcher.Dispatch ("start_star_question", new PayloadObject(indexOfStarQuestion));
		} 
		else {
			_dispatcher.Dispatch ("enable_play_button", game);
		}
	}

	void setTitle(GameModel game) {
		TitleTextScript titleText;
		// set title
		titleText = FindObjectOfType<TitleTextScript>();
		titleText.setTitle ("Turn " + game.turn);
	}

	void updateGame(UnityEngine.Object data) {
		// re-build scene
		buildScene (((GameModel)data));
		// reset question-answers
		_dispatcher.Dispatch ("reset_answers");
		_dispatcher.Dispatch("message_wrong_hide");
		_dispatcher.Dispatch("message_correct_hide");
	}

	int starQuestionPending(GameModel game) {
		string username = PlayerPrefs.GetString ("username");
		int[] categories;
		// get categories
		if (game.players.challenger.username == username) {
			categories = game.players.challenger.categoriesProgress;
		}
		else {
			categories = game.players.challenged.categoriesProgress;
		}
		return Array.IndexOf (categories, Properties.starQuestion);
	}
}
