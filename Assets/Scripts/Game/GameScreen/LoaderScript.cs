using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;
using BestHTTP;
using LitJson;
using GameAnalyticsSDK;

public class LoaderScript : FacadeMonoBehaviour {

	string _gameId;
	GameModel currentGame;

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
		currentGame = game;
		// send analytics
		GameAnalytics.NewProgressionEvent(GA_Progression.GAProgressionStatus.GAProgressionStatusStart, "match", "turn_" + currentGame.turn);
		// set title
		setTitle (game);
		// set the categories and name of the players
		_dispatcher.Dispatch ("disable_play_button", game);
		_dispatcher.Dispatch ("set_player_name", game);
		_dispatcher.Dispatch ("update_categories", game);
		_dispatcher.Dispatch ("update_title", game);
		// check if the game is finished
		if (game.ended) {
			gameFinished (game);
		} 
		else {
			// check if it's de current user turn
			string username = PlayerPrefs.GetString ("username");
			if (game.thisTurn == username) {
				// check if the final round is in progress
				if (finalRound (game)) {
					_dispatcher.Dispatch ("start_final_round", game);
				} 
				else {
					// check if a star question is in progress
					int indexOfStarQuestion = starQuestionPending (game);
					if (indexOfStarQuestion >= 0) {
						_dispatcher.Dispatch ("start_star_question", new PayloadObject (indexOfStarQuestion));
					} 
					else {
						_dispatcher.Dispatch ("enable_play_button", game);
					}
				}
			} 
			else {
				_dispatcher.Dispatch ("enable_play_button", game);
			}
		}
	}

	void setTitle(GameModel game) {
		TitleTextScript titleText;
		// set title
		titleText = FindObjectOfType<TitleTextScript>();
		titleText.setTitle (_i18n.get ("TURN_TEXT") + " " + game.turn);
	}

	void updateGame(UnityEngine.Object data) {
		// re-build scene
		buildScene (((GameModel)data));
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

	bool finalRound(GameModel game) {
		string username = PlayerPrefs.GetString ("username");
		int[] categories;
		bool finalRoundReady = true;
		// get categories
		if (game.players.challenger.username == username) {
			categories = game.players.challenger.categoriesProgress;
		}
		else {
			categories = game.players.challenged.categoriesProgress;
		}
		// check every category is at progress 4
		for (int i = 0; i < categories.Length; i++) {
			finalRoundReady = finalRoundReady && (categories[i] == Properties.completedQuestion);
		}
		return finalRoundReady;
	}
	
	void gameFinished(GameModel game) {
		string username = PlayerPrefs.GetString ("username");
		if (game.winner == username) {
			_dispatcher.Dispatch ("game_won");
		} 
		else {
			_dispatcher.Dispatch("game_lost");
		}
	}
}
